using System.Text.Json;
using System.Globalization;
using Telerik.Documents.Fixed.Model;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.PdfViewer;
using FixedSize = Telerik.Documents.Primitives.Size;

namespace Telerik.AppUtils.Services;

/// <summary>
/// Provides the narrow command surface required by PdfViewer Appium tests. Commands execute on
/// the viewer dispatcher so test setup never depends on platform picker implementations.
/// </summary>
public static class PdfViewerTestingService
{
    private const string CommandPrefix = "PDFVIEWER_";
    private const string ReadyCommand = "PDFVIEWER_READY";
    private const string SetCommand = "PDFVIEWER_SET";
    private const string SourceCommand = "PDFVIEWER_SOURCE:";
    private const string PreparePageCommand = "PDFVIEWER_PREPARE_FULL_PAGE";
    private const string NavigateToPageCommand = "PDFVIEWER_NAVIGATE_TO_PAGE:";
    private const string ChangeViewportCommand = "PDFVIEWER_CHANGE_VIEWPORT:";
    private const string InspectCommand = "PDFVIEWER_INSPECT";
    private const string WordRectCommand = "PDFVIEWER_WORD_RECT:";
    private const string LineRangeCommand = "PDFVIEWER_LINE_RANGE:";
    private static Func<string, bool>? chooseSource;
    private static string? exampleId;
    private static Func<PdfViewerTestingSetting, string, string>? setSetting;
    private static Func<PdfViewerTestingState>? inspectionSelector;
    private static Func<RadPdfViewer, long>? completedPaintCountSelector;
    private static WeakReference<RadPdfViewer>? viewerReference;
    private static ITestingService? testingService;
    private static RadFixedDocument? documentBeforeSourceChange;
    private static bool isSourceLoadPending;
    private static PdfViewerTextSearchResult? lastSearchResult;
    private static string lastCompletedSearchQuery = string.Empty;

    public static bool TryStart(
        RadPdfViewer viewer,
        string pageId,
        Func<string, bool>? sourceSelector = null,
        Func<PdfViewerTestingSetting, string, string>? settingSelector = null,
        Func<PdfViewerTestingState>? testingInspectionSelector = null,
        Func<RadPdfViewer, long>? completedPaintCountProvider = null)
    {
        var service = DependencyService.Get<ITestingService>();
        if (service == null || !service.IsAppUnderTest)
        {
            return false;
        }

        if (testingService != null)
        {
            testingService.OnCommand -= HandleCommand;
        }

        testingService = service;
        chooseSource = sourceSelector;
        exampleId = pageId;
        setSetting = settingSelector;
        inspectionSelector = testingInspectionSelector;
        completedPaintCountSelector = completedPaintCountProvider;
        viewerReference = new WeakReference<RadPdfViewer>(viewer);
        lastSearchResult = null;
        lastCompletedSearchQuery = string.Empty;
        documentBeforeSourceChange = null;
        isSourceLoadPending = false;
        testingService.OnCommand += HandleCommand;
        return true;
    }

    private static void HandleCommand(object? sender, TestCommandEventArgs e)
    {
        if (!e.Command.StartsWith(CommandPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var completion = new TaskCompletionSource<string?>();
        e.Result = completion.Task;

        if (viewerReference == null || !viewerReference.TryGetTarget(out var viewer))
        {
            completion.SetResult("ERROR: PdfViewer is not available.");
            return;
        }

        viewer.Dispatcher.Dispatch(() =>
        {
            try
            {
                completion.SetResult(ExecuteCommand(viewer, e.Command));
            }
            catch (Exception exception)
            {
                completion.SetResult($"ERROR: {exception.Message}");
            }
        });
    }

    private static string ExecuteCommand(RadPdfViewer viewer, string command)
    {
        if (command.StartsWith(ReadyCommand + ":", StringComparison.OrdinalIgnoreCase))
        {
            string requestedPageId = command[(ReadyCommand.Length + 1)..];
            if (string.IsNullOrWhiteSpace(requestedPageId))
            {
                return "ERROR: A PdfViewer page id is required.";
            }

            return string.Equals(requestedPageId, exampleId, StringComparison.Ordinal)
                ? $"READY:{exampleId}"
                : $"ERROR: PdfViewer page '{requestedPageId}' is not active.";
        }

        if (command.StartsWith(SourceCommand, StringComparison.OrdinalIgnoreCase))
        {
            string fileName = command[SourceCommand.Length..];
            lastSearchResult = null;
            lastCompletedSearchQuery = string.Empty;
            documentBeforeSourceChange = viewer.Document;
            isSourceLoadPending = true;
            if (chooseSource?.Invoke(fileName) == true)
            {
                return "OK";
            }

            isSourceLoadPending = false;
            return $"ERROR: Unknown source '{fileName}'.";
        }

        if (command.StartsWith(PreparePageCommand, StringComparison.OrdinalIgnoreCase))
        {
            if (!TryGetRequestedPageIndex(command, viewer, out int pageIndex))
            {
                return "ERROR: The PdfViewer full-page command requires a valid one-based page number.";
            }

            bool isPrepared = TryPreparePageFullyVisible(viewer, pageIndex);
            if (!isPrepared)
            {
                return "ERROR: The requested PdfViewer page cannot be fitted.";
            }

            return "OK";
        }

        if (command.StartsWith(NavigateToPageCommand, StringComparison.OrdinalIgnoreCase))
        {
            string value = command[NavigateToPageCommand.Length..];
            if (!int.TryParse(value, out int pageNumber) ||
                viewer.Document is null ||
                pageNumber < 1 ||
                pageNumber > viewer.Document.Pages.Count)
            {
                return $"ERROR: '{value}' is not a valid PdfViewer page number.";
            }

            viewer.NavigateToPage(pageNumber - 1);
            return "OK";
        }

        if (command.StartsWith(ChangeViewportCommand, StringComparison.OrdinalIgnoreCase))
        {
            string value = command[ChangeViewportCommand.Length..];
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double viewportY) ||
                !double.IsFinite(viewportY) ||
                viewportY < 0)
            {
                return $"ERROR: '{value}' is not a valid PdfViewer viewport Y value.";
            }

            Rect viewport = viewer.Viewport;
            if (!IsFinitePositive(viewport.Width) || !IsFinitePositive(viewport.Height))
            {
                return "ERROR: The PdfViewer viewport is not ready.";
            }

            viewer.ChangeViewport(new Rect(viewport.X, viewportY, viewport.Width, viewport.Height));
            return "OK";
        }

        if (command.StartsWith(SetCommand + ":", StringComparison.OrdinalIgnoreCase))
        {
            return ExecuteSettingCommand(viewer, command[(SetCommand.Length + 1)..]);
        }

        if (command.StartsWith(WordRectCommand, StringComparison.OrdinalIgnoreCase))
        {
            return ExecuteWordRectCommand(viewer, command[WordRectCommand.Length..]);
        }

        if (command.StartsWith(LineRangeCommand, StringComparison.OrdinalIgnoreCase))
        {
            return ExecuteLineRangeCommand(viewer, command[LineRangeCommand.Length..]);
        }

        if (command.Equals(InspectCommand, StringComparison.OrdinalIgnoreCase))
        {
            bool isSourceLoaded = !isSourceLoadPending ||
                (viewer.Document is not null && !ReferenceEquals(viewer.Document, documentBeforeSourceChange));
            bool isRendered = isSourceLoaded &&
                viewer.VisiblePagesCount > 0 &&
                IsFinitePositive(viewer.Width) &&
                IsFinitePositive(viewer.Height) &&
                IsFinitePositive(viewer.Viewport.Width) &&
                IsFinitePositive(viewer.Viewport.Height);
            if (isRendered)
            {
                isSourceLoadPending = false;
            }

            var searchSettings = viewer.SearchSettings;
            var searchResult = searchSettings?.TextSearchResult;
            if (!Equals(searchResult, lastSearchResult))
            {
                lastSearchResult = searchResult;
                lastCompletedSearchQuery = searchResult is null ? string.Empty : searchSettings?.Text ?? string.Empty;
            }

            int activeSearchResultIndex = GetActiveSearchResultIndex(searchResult);
            int searchResultCount = searchResult?.SearchResults?.Count ?? 0;
            string searchResultsStringFormat = searchSettings?.SearchResultsStringFormat ?? string.Empty;
            var testingState = inspectionSelector?.Invoke() ?? new PdfViewerTestingState();
            FullPageInspection fullPage = GetFullPageInspection(viewer);
            string completedSearchQuery = string.IsNullOrEmpty(testingState.CompletedSearchQuery)
                ? lastCompletedSearchQuery
                : testingState.CompletedSearchQuery;

            return JsonSerializer.Serialize(new
            {
                IsLoaded = isSourceLoaded && viewer.Document != null,
                IsRendered = isRendered,
                ZoomLevel = GetFiniteValue(viewer.ZoomLevel),
                LayoutMode = viewer.LayoutMode.ToString(),
                viewer.VisiblePagesStartIndex,
                viewer.VisiblePagesCount,
                SearchBusy = searchSettings?.IsBusy ?? false,
                SearchCompleted = searchResult != null
                    && !(searchSettings?.IsBusy ?? false)
                    && string.Equals(searchSettings?.Text, completedSearchQuery, StringComparison.Ordinal),
                SearchQuery = searchSettings?.Text ?? string.Empty,
                SearchResultQuery = completedSearchQuery,
                SearchResultCount = searchResultCount,
                ActiveSearchResultIndex = activeSearchResultIndex,
                SearchTrigger = searchSettings?.TextSearchTrigger.ToString() ?? string.Empty,
                SearchResultsStringFormat = searchResultsStringFormat,
                testingState.SearchSettingsIdentity,
                ViewportWidth = GetFiniteValue(viewer.Width),
                ViewportHeight = GetFiniteValue(viewer.Height),
                ViewportY = GetFiniteValue(viewer.Viewport.Y),
                fullPage.CurrentPageIndex,
                fullPage.PageFitsViewportWidth,
                fullPage.PageFitsViewportHeight,
                fullPage.IsFullPageFitApplied,
                FitToWidthCanExecute = viewer.FitToWidthCommand.CanExecute(null),
                CompletedPaintCount = completedPaintCountSelector?.Invoke(viewer) ?? 0,
                testingState.DocumentFileName,
                testingState.HasSelection,
                testingState.SelectedText,
                testingState.SelectionStartPositionIndex,
                testingState.SelectionEndPositionIndex,
                testingState.SelectionLineCount,
            });
        }

        return "ERROR: Unsupported PdfViewer command.";
    }

    private static string ExecuteWordRectCommand(RadPdfViewer viewer, string arguments)
    {
        string[] parts = arguments.Split('|');
        if (parts.Length == 0 || string.IsNullOrEmpty(parts[0]))
        {
            return "ERROR: PdfViewer word rect requires a word.";
        }

        string word = parts[0];
        int occurrence = 0;
        if (parts.Length > 1 && !int.TryParse(parts[1], out occurrence))
        {
            return $"ERROR: '{parts[1]}' is not a valid PdfViewer word occurrence index.";
        }

        PdfViewerTextRangeResult? result = PdfViewerSelectionTesting.GetWordRect(viewer, word, occurrence);
        if (result is null)
        {
            return $"ERROR: PdfViewer could not locate word '{word}' (occurrence {occurrence}).";
        }

        return JsonSerializer.Serialize(result);
    }

    private static string ExecuteLineRangeCommand(RadPdfViewer viewer, string arguments)
    {
        string[] parts = arguments.Split('|');
        if (parts.Length < 2 ||
            !int.TryParse(parts[0], out int pageIndex) ||
            !int.TryParse(parts[1], out int startLineIndex))
        {
            return "ERROR: PdfViewer line range requires a page index and a start line index.";
        }

        int endLineIndex = startLineIndex;
        if (parts.Length > 2 && !int.TryParse(parts[2], out endLineIndex))
        {
            return $"ERROR: '{parts[2]}' is not a valid PdfViewer end line index.";
        }

        PdfViewerTextRangeResult? result = PdfViewerSelectionTesting.GetLineRange(viewer, pageIndex, startLineIndex, endLineIndex);
        if (result is null)
        {
            return $"ERROR: PdfViewer could not resolve lines {startLineIndex}-{endLineIndex} on page {pageIndex}.";
        }

        return JsonSerializer.Serialize(result);
    }

    private static bool TryPreparePageFullyVisible(RadPdfViewer viewer, int pageIndex)
    {
        if (viewer.Document is null ||
            pageIndex < 0 ||
            pageIndex >= viewer.Document.Pages.Count ||
            viewer.Width <= 0 ||
            viewer.Height <= 0)
        {
            return false;
        }

        FixedSize pageSize = viewer.Document.Pages[pageIndex].Size;
        Rect viewport = viewer.Viewport;
        double zoomLevel = GetFiniteValue(viewer.ZoomLevel);
        double viewportDisplayWidth = viewport.Width * zoomLevel;
        double viewportDisplayHeight = viewport.Height * zoomLevel;
        if (!IsFinitePositive(viewportDisplayWidth) ||
            !IsFinitePositive(viewportDisplayHeight) ||
            !IsFinitePositive(pageSize.Width) ||
            !IsFinitePositive(pageSize.Height))
        {
            return false;
        }

        viewer.LayoutMode = LayoutMode.SinglePage;
        viewer.NavigateToPage(pageIndex);

        double widthFitZoom = viewportDisplayWidth / pageSize.Width;
        double heightFitZoom = viewportDisplayHeight / pageSize.Height;
        double fullPageZoom = Math.Min(widthFitZoom, heightFitZoom);
        if (fullPageZoom < viewer.MinZoomLevel)
        {
            viewer.MinZoomLevel = fullPageZoom;
        }

        double targetZoom = Math.Clamp(
            fullPageZoom,
            viewer.MinZoomLevel,
            viewer.MaxZoomLevel);
        viewer.ZoomToLevel(targetZoom);
        viewer.NavigateToPage(pageIndex);
        return true;
    }

    private static bool TryGetRequestedPageIndex(string command, RadPdfViewer viewer, out int pageIndex)
    {
        pageIndex = viewer.VisiblePagesStartIndex;
        if (command.Length == PreparePageCommand.Length)
        {
            return viewer.Document is not null &&
                pageIndex >= 0 &&
                pageIndex < viewer.Document.Pages.Count;
        }

        if (!command.StartsWith(PreparePageCommand + ":", StringComparison.OrdinalIgnoreCase) ||
            !int.TryParse(command[(PreparePageCommand.Length + 1)..], out int pageNumber))
        {
            return false;
        }

        pageIndex = pageNumber - 1;
        return viewer.Document is not null &&
            pageIndex >= 0 &&
            pageIndex < viewer.Document.Pages.Count;
    }

    private static FullPageInspection GetFullPageInspection(RadPdfViewer viewer)
    {
        int pageIndex = viewer.VisiblePagesStartIndex;
        if (viewer.Document is null ||
            pageIndex < 0 ||
            pageIndex >= viewer.Document.Pages.Count)
        {
            return FullPageInspection.Empty;
        }

        FixedSize pageSize = viewer.Document.Pages[pageIndex].Size;
        Rect viewport = viewer.Viewport;
        double zoomLevel = GetFiniteValue(viewer.ZoomLevel);
        double viewportDisplayWidth = viewport.Width * zoomLevel;
        double viewportDisplayHeight = viewport.Height * zoomLevel;
        if (!IsFinitePositive(pageSize.Width) ||
            !IsFinitePositive(pageSize.Height) ||
            !IsFinitePositive(viewport.Width) ||
            !IsFinitePositive(viewport.Height) ||
            !IsFinitePositive(viewportDisplayWidth) ||
            !IsFinitePositive(viewportDisplayHeight))
        {
            return FullPageInspection.Empty;
        }

        double expectedZoom = Math.Clamp(
            Math.Min(viewportDisplayWidth / pageSize.Width, viewportDisplayHeight / pageSize.Height),
            viewer.MinZoomLevel,
            viewer.MaxZoomLevel);
        bool fitsWidth = pageSize.Width <= viewport.Width + 0.01;
        bool fitsHeight = pageSize.Height <= viewport.Height + 0.01;
        return new FullPageInspection(
            pageIndex,
            fitsWidth,
            fitsHeight,
            viewer.LayoutMode == LayoutMode.SinglePage &&
                viewer.VisiblePagesCount == 1 &&
                fitsWidth &&
                fitsHeight &&
                Math.Abs(zoomLevel - expectedZoom) <= 0.01);
    }

    private static int GetActiveSearchResultIndex(PdfViewerTextSearchResult? searchResult)
    {
        if (searchResult?.MainSearchResult is null || searchResult.SearchResults is null)
        {
            return -1;
        }

        int index = 0;
        foreach (var result in searchResult.SearchResults)
        {
            if (Equals(result, searchResult.MainSearchResult))
            {
                return index;
            }

            index++;
        }

        return -1;
    }

    private static bool IsFinitePositive(double value) => double.IsFinite(value) && value > 0;

    private static double GetFiniteValue(double value) => double.IsFinite(value) ? value : 0;

    private readonly record struct FullPageInspection(
        int CurrentPageIndex,
        bool PageFitsViewportWidth,
        bool PageFitsViewportHeight,
        bool IsFullPageFitApplied)
    {
        public static FullPageInspection Empty => new(
            -1,
            false,
            false,
            false);
    }

    private static string ExecuteSettingCommand(RadPdfViewer viewer, string command)
    {
        int separatorIndex = command.IndexOf('|');
        if (separatorIndex < 0)
        {
            return "ERROR: PdfViewer settings require a value.";
        }

        string settingName = command[..separatorIndex];
        string value = command[(separatorIndex + 1)..];
        if (!Enum.TryParse(settingName, ignoreCase: true, out PdfViewerTestingSetting setting))
        {
            return $"ERROR: Unsupported PdfViewer setting '{settingName}'.";
        }

        if (setting == PdfViewerTestingSetting.ZoomLevel)
        {
            return SetZoomLevel(viewer, value);
        }

        return setSetting?.Invoke(setting, value) ?? $"ERROR: PdfViewer page '{exampleId ?? "unknown"}' does not support settings.";
    }

    private static string SetZoomLevel(RadPdfViewer viewer, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "ERROR: PdfViewer ZoomLevel requires an invariant-culture value.";
        }

        if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double zoomLevel))
        {
            return $"ERROR: '{value}' is not a valid invariant-culture PdfViewer ZoomLevel.";
        }

        if (!double.IsFinite(zoomLevel))
        {
            return $"ERROR: PdfViewer ZoomLevel '{value}' must be finite.";
        }

        double minimumZoomLevel = viewer.MinZoomLevel;
        double maximumZoomLevel = viewer.MaxZoomLevel;
        if (!IsFinitePositive(minimumZoomLevel) ||
            !IsFinitePositive(maximumZoomLevel) ||
            minimumZoomLevel > maximumZoomLevel)
        {
            return "ERROR: PdfViewer ZoomLevel cannot be set because the supported zoom range is invalid.";
        }

        if (zoomLevel < minimumZoomLevel || zoomLevel > maximumZoomLevel)
        {
            return $"ERROR: PdfViewer ZoomLevel '{value}' is outside the supported range " +
                $"[{minimumZoomLevel.ToString("G", CultureInfo.InvariantCulture)}, " +
                $"{maximumZoomLevel.ToString("G", CultureInfo.InvariantCulture)}].";
        }

        viewer.ZoomToLevel(zoomLevel);
        return "OK";
    }
}

public sealed record PdfViewerTestingState
{
    public string SearchSettingsIdentity { get; init; } = string.Empty;

    public string CompletedSearchQuery { get; init; } = string.Empty;

    public string DocumentFileName { get; init; } = string.Empty;

    public bool HasSelection { get; init; }

    public string SelectedText { get; init; } = string.Empty;

    public int SelectionStartPositionIndex { get; init; } = -1;

    public int SelectionEndPositionIndex { get; init; } = -1;

    public int SelectionLineCount { get; init; }
}

public enum PdfViewerTestingSetting
{
    ZoomLevel,
    SearchResultsFormat,
    SearchTrigger,
    SearchSettings,
    SearchText,
    ToastStyle,
    VisibleToolbarItem,
    SelectionEnabled,
    SelectionFill,
    SelectionIndicatorColor,
    SelectionMenuTemplate,
    SelectionMenuFill,
    SelectionMenuTextColor,
    SelectionHandles,
}
