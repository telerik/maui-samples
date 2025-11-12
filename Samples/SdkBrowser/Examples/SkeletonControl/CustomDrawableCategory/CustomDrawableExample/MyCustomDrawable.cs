using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SDKBrowserMaui.Examples.SkeletonControl.CustomDrawableCategory.CustomDrawableExample;

// >> skeleton-custom-drawable-implementation
public class MyCustomDrawable : BindableObject, IDrawable
{
    public static readonly BindableProperty FillColorProperty =
        BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(MyCustomDrawable));

    public Color FillColor
    {
        get => (Color)this.GetValue(FillColorProperty);
        set => this.SetValue(FillColorProperty, value);
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (dirtyRect.Width <= 0 || dirtyRect.Height <= 0)
            return;

        canvas.SaveState();
        canvas.Antialias = true;
        canvas.FillColor = this.FillColor;

        var padding = 10f;
        var availableWidth = dirtyRect.Width - 2 * padding;
        var availableHeight = dirtyRect.Height - 2 * padding;
        var spacing = 10f;

        // First row - long rectangle for text (takes most of the space)
        var textHeight = 20f; // Height for the text rectangle
        var firstRowY = dirtyRect.Y + padding;
        var textRect = new RectF(dirtyRect.X + padding, firstRowY, availableWidth, textHeight);
        canvas.FillRectangle(textRect);

        // Second row - two button rectangles (smaller, positioned at bottom)
        var buttonHeight = 40f; // Height for button rectangles
        var buttonWidth = 80f; // Width for each button rectangle
        var secondRowY = firstRowY + textHeight + spacing;

        // Left button (Like)
        var leftButtonRect = new RectF(dirtyRect.X + padding, secondRowY, buttonWidth, buttonHeight);
        canvas.FillRectangle(leftButtonRect);

        // Right button (Dislike) - positioned next to the left button
        var rightButtonRect = new RectF(dirtyRect.X + padding + buttonWidth + spacing, secondRowY, buttonWidth, buttonHeight);
        canvas.FillRectangle(rightButtonRect);

        canvas.RestoreState();
    }
}
// << skeleton-custom-drawable-implementation