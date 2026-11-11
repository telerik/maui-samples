using Telerik.Documents.Fixed.Model;
using Telerik.Documents.Fixed.Selection;
using Telerik.Documents.Fixed.Text;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.PdfViewer;
using FixedRect = Telerik.Documents.Primitives.Rect;

namespace Telerik.AppUtils.Services;

/// <summary>
/// Supplies PdfViewer selection geometry and state from the public fixed-document APIs.
/// Generic word/line queries replace the previous hardcoded selection scenarios so tests
/// can compose any selection range without app-side changes.
/// </summary>
public static class PdfViewerSelectionTesting
{
    public static PdfViewerTestingState GetState(RadPdfViewer viewer, string documentFileName,
        bool includeSelectionState = false)
    {
        RadFixedDocument? document = viewer.Document;
        PdfViewerTestingState state = new()
        {
            DocumentFileName = documentFileName,
        };

        if (document is null ||
            document.Pages.Count == 0 ||
            !HasViewport(viewer))
        {
            return state;
        }

        if (!includeSelectionState)
        {
            return state;
        }

        TextSelection? selection = document.Selection;
        if (selection is null || selection.IsEmpty || selection.StartPosition is null || selection.EndPosition is null)
        {
            return state;
        }

        TextPosition selectionStart = selection.StartPosition <= selection.EndPosition ? selection.StartPosition : selection.EndPosition;
        TextPosition selectionEnd = selection.StartPosition <= selection.EndPosition ? selection.EndPosition : selection.StartPosition;

        state = state with
        {
            HasSelection = true,
            SelectedText = selection.GetSelectedText() ?? string.Empty,
            SelectionStartPositionIndex = selectionStart.Index,
            SelectionEndPositionIndex = selectionEnd.Index,
            SelectionLineCount = GetSelectedLineCount(selectionStart, selectionEnd),
        };

        return state;
    }

    /// <summary>
    /// Locates the given word (nth occurrence across the document) and returns its bounding
    /// rect in viewport coordinates, along with its text-position range and text.
    /// </summary>
    public static PdfViewerTextRangeResult? GetWordRect(RadPdfViewer viewer, string word, int occurrence)
    {
        RadFixedDocument? document = viewer.Document;
        if (document is null || document.Pages.Count == 0 || !HasViewport(viewer))
        {
            return null;
        }

        TextPosition? position = FindWordPosition(document, word, occurrence);
        if (position is null)
        {
            return null;
        }

        FixedRect wordBounds = position.GetWordBoundingRect();
        if (!HasPositiveBounds(wordBounds))
        {
            return null;
        }

        int pageIndex = document.Pages.IndexOf(position.Page);
        (double x, double y, double width, double height) = GetBoundsInViewport(wordBounds, viewer, document, pageIndex);
        if (!IsWithinViewerBounds(viewer, x + (width / 2), y + (height / 2)))
        {
            return null;
        }

        TextPosition startPosition = new(position);
        startPosition.MoveToCurrentWordStart();
        TextPosition endPosition = new(position);
        endPosition.MoveToCurrentWordEnd();
        string text = GetSelectedText(document, startPosition, endPosition);

        return new PdfViewerTextRangeResult(
            x, y, width, height,
            x, y, width, height,
            viewer.Width, viewer.Height,
            startPosition.Index, endPosition.Index,
            text, 1);
    }

    /// <summary>
    /// Resolves the line range [startLineIndex, endLineIndex] (0-based, relative to the start
    /// of the given page) and returns the bounding rects of its first and last words in
    /// viewport coordinates, along with its text-position range and text.
    /// </summary>
    public static PdfViewerTextRangeResult? GetLineRange(RadPdfViewer viewer, int pageIndex, int startLineIndex, int endLineIndex)
    {
        RadFixedDocument? document = viewer.Document;
        if (document is null ||
            pageIndex < 0 || pageIndex >= document.Pages.Count ||
            startLineIndex < 0 || endLineIndex < startLineIndex ||
            !HasViewport(viewer))
        {
            return null;
        }

        TextPosition startLine = new(document.Pages[pageIndex]);
        startLine.MoveToLineStart();
        for (int line = 0; line < startLineIndex; line++)
        {
            if (!startLine.MoveLineDown())
            {
                return null;
            }
        }

        // MoveLineDown preserves the column from the original line, which can drift into the
        // whitespace before the first word on justified lines; re-anchor to the true line start.
        startLine.MoveToLineStart();

        TextPosition endLine = new(startLine);
        for (int line = startLineIndex; line < endLineIndex; line++)
        {
            if (!endLine.MoveLineDown())
            {
                return null;
            }
        }

        endLine.MoveToLineEnd();
        if (!endLine.MoveToPreviousWord())
        {
            return null;
        }

        FixedRect startWordBounds = startLine.GetWordBoundingRect();
        FixedRect endWordBounds = endLine.GetWordBoundingRect();
        if (!HasPositiveBounds(startWordBounds) || !HasPositiveBounds(endWordBounds))
        {
            return null;
        }

        (double startX, double startY, double startWidth, double startHeight) = GetBoundsInViewport(startWordBounds, viewer, document, pageIndex);
        (double endX, double endY, double endWidth, double endHeight) = GetBoundsInViewport(endWordBounds, viewer, document, pageIndex);
        if (!IsWithinViewerBounds(viewer, startX + (startWidth / 2), startY + (startHeight / 2)) ||
            !IsWithinViewerBounds(viewer, endX + (endWidth / 2), endY + (endHeight / 2)))
        {
            return null;
        }

        TextPosition selectionStart = new(startLine);
        selectionStart.MoveToCurrentWordStart();
        TextPosition selectionEnd = new(endLine);
        selectionEnd.MoveToCurrentWordEnd();
        string text = GetSelectedText(document, selectionStart, selectionEnd);

        return new PdfViewerTextRangeResult(
            startX, startY, startWidth, startHeight,
            endX, endY, endWidth, endHeight,
            viewer.Width, viewer.Height,
            selectionStart.Index, selectionEnd.Index,
            text, endLineIndex - startLineIndex + 1);
    }

    private static TextPosition? FindWordPosition(RadFixedDocument document, string word, int occurrence)
    {
        int remaining = occurrence;
        foreach (RadFixedPage page in document.Pages)
        {
            TextPosition position = new(page);
            while (true)
            {
                string currentWord = position.ToString().Replace("@", string.Empty, StringComparison.Ordinal);
                if (string.Equals(currentWord, word, StringComparison.OrdinalIgnoreCase))
                {
                    if (remaining == 0)
                    {
                        return position;
                    }

                    remaining--;
                }

                if (!position.MoveToNextWord())
                {
                    break;
                }
            }
        }

        return null;
    }

    /// <summary>Selects [start, end] temporarily to read its text, then restores any previous selection.</summary>
    private static string GetSelectedText(RadFixedDocument document, TextPosition start, TextPosition end)
    {
        TextSelection selection = document.Selection;
        TextPosition? previousStart;
        TextPosition? previousEnd;
        string text;
        lock (selection)
        {
            previousStart = selection.StartPosition is null ? null : new TextPosition(selection.StartPosition);
            previousEnd = selection.EndPosition is null ? null : new TextPosition(selection.EndPosition);
            selection.SetSelection(start, end);
            text = selection.GetSelectedText() ?? string.Empty;
            if (previousStart is null || previousEnd is null)
            {
                selection.Clear();
            }
            else
            {
                selection.SetSelection(previousStart, previousEnd);
            }
        }

        return text;
    }

    private static bool HasViewport(RadPdfViewer viewer)
    {
        Rect viewport = viewer.Viewport;
        return double.IsFinite(viewer.Width) &&
            double.IsFinite(viewer.Height) &&
            double.IsFinite(viewport.Width) &&
            double.IsFinite(viewport.Height) &&
            viewer.Width > 0 &&
            viewer.Height > 0 &&
            viewport.Width > 0 &&
            viewport.Height > 0;
    }

    private static bool HasPositiveBounds(FixedRect bounds)
    {
        return double.IsFinite(bounds.Left) &&
            double.IsFinite(bounds.Top) &&
            double.IsFinite(bounds.Right) &&
            double.IsFinite(bounds.Bottom) &&
            bounds.Right > bounds.Left &&
            bounds.Bottom > bounds.Top;
    }

    private static bool IsWithinViewerBounds(RadPdfViewer viewer, double x, double y)
    {
        return double.IsFinite(x) &&
            double.IsFinite(y) &&
            x > 0 &&
            x < viewer.Width &&
            y > 0 &&
            y < viewer.Height;
    }

    private static (double X, double Y) ToViewportPoint(
        RadPdfViewer viewer,
        RadFixedDocument document,
        int pageIndex,
        double x,
        double y)
    {
        (double pageX, double pageY) = GetPageOrigin(document, viewer, pageIndex);
        Rect viewport = viewer.Viewport;
        return (
            (pageX + x - viewport.X) * viewer.Width / viewport.Width,
            (pageY + y - viewport.Y) * viewer.Height / viewport.Height);
    }

    private static (double X, double Y, double Width, double Height) GetBoundsInViewport(
        FixedRect bounds,
        RadPdfViewer viewer,
        RadFixedDocument document,
        int pageIndex)
    {
        (double left, double top) = ToViewportPoint(viewer, document, pageIndex, bounds.Left, bounds.Top);
        (double right, double bottom) = ToViewportPoint(viewer, document, pageIndex, bounds.Right, bounds.Bottom);
        return (left, top, right - left, bottom - top);
    }

    private static (double X, double Y) GetPageOrigin(RadFixedDocument document, RadPdfViewer viewer, int pageIndex)
    {
        if (viewer.LayoutMode == LayoutMode.SinglePage)
        {
            return (0, 0);
        }

        double widestPage = 0;
        for (int index = 0; index < document.Pages.Count; index++)
        {
            widestPage = Math.Max(widestPage, document.Pages[index].Size.Width);
        }

        double top = 0;
        for (int index = 0; index < pageIndex; index++)
        {
            top += document.Pages[index].Size.Height + viewer.PageSpacing;
        }

        return ((widestPage - document.Pages[pageIndex].Size.Width) / 2, top);
    }

    private static int GetSelectedLineCount(TextPosition selectionStart, TextPosition selectionEnd)
    {
        TextPosition lineStart = new(selectionStart);
        lineStart.MoveToLineStart();
        int lineCount = 1;

        while (lineStart < selectionEnd)
        {
            TextPosition nextLineStart = new(lineStart);
            if (!nextLineStart.MoveLineDown() || nextLineStart > selectionEnd)
            {
                break;
            }

            lineStart = nextLineStart;
            lineCount++;
        }

        return lineCount;
    }

}

/// <summary>Geometry, text-position range, and text for a word or line-range query, in viewport coordinates.</summary>
public sealed record PdfViewerTextRangeResult(
    double StartX,
    double StartY,
    double StartWidth,
    double StartHeight,
    double EndX,
    double EndY,
    double EndWidth,
    double EndHeight,
    double ViewportWidth,
    double ViewportHeight,
    int StartPositionIndex,
    int EndPositionIndex,
    string Text,
    int LineCount);
