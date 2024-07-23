using Microsoft.Maui.Graphics;
using SkiaSharp;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.CellRendererExample;

// >> datagrid-column-datagridcellrenderer
public class CustomColumnRenderer : DataGridCellRenderer
{
    protected override void RenderContainer(DataGridCellRendererRenderContext renderContext)
    {
        Club club = (Club)renderContext.Item;

        if (renderContext is DataGridSkiaSharpCellRendererRenderContext skRenderContext)
        {
            this.DrawBulletGraph(club.Revenue, skRenderContext, skRenderContext.Bounds);
        }
    }

    private void DrawBulletGraph(double? revenue, DataGridSkiaSharpCellRendererRenderContext skRenderContext, Rect bounds)
    {
        double badRange = 300;
        double satisfactoryRange = 500;
        double goodRange = 700;
        double horizontalPadding = 8;
        double bulletGraphWidth = bounds.Width - 2 * horizontalPadding;
        double x = bounds.X + horizontalPadding;
        double h = 20;
        double y = bounds.Y + (bounds.Height - h) / 2;
        double featuredHeight = 8;
        double displayScale = skRenderContext.DisplayScale;

        // bad range
        using (SKPaint paint = new SKPaint())
        {
            paint.Color = new SKColor(150, 150, 150);
            Rect rect = new Rect(x, y, bulletGraphWidth * (badRange / goodRange), h);
            skRenderContext.Canvas.DrawRect(SkiaUtils.ToSKRect(rect, displayScale), paint);
            x += rect.Width;
        }

        // satisfactory range
        using (SKPaint paint = new SKPaint())
        {
            paint.Color = new SKColor(180, 180, 180);

            Rect rect = new Rect(x, y, bulletGraphWidth * ((satisfactoryRange - badRange) / goodRange), h);
            skRenderContext.Canvas.DrawRect(SkiaUtils.ToSKRect(rect, displayScale), paint);
            x += rect.Width;
        }

        // good range
        using (SKPaint paint = new SKPaint())
        {

            paint.Color = new SKColor(230, 230, 230);
            Rect rect = new Rect(x, y, bulletGraphWidth * ((goodRange - satisfactoryRange) / goodRange), h);
            skRenderContext.Canvas.DrawRect(SkiaUtils.ToSKRect(rect, displayScale), paint);
        }

        // featured measure
        if (revenue != null)
        {
            using (SKPaint paint = new SKPaint())
            {
                paint.Color = new SKColor(48, 48, 48);
                Rect rect = new Rect(bounds.X + horizontalPadding, y + ((h - featuredHeight) / 2), bulletGraphWidth * (revenue.Value / goodRange), featuredHeight);
                skRenderContext.Canvas.DrawRect(SkiaUtils.ToSKRect(rect, displayScale), paint);
            }
        }
    }

    internal static class SkiaUtils
    {
        public static SKRect ToSKRect(Rect rect, double scale = 1)
        {
            return new SKRect((float)(scale * rect.Left), (float)(scale * rect.Top), (float)(scale * rect.Right), (float)(scale * rect.Bottom));
        }

        public static Rect ToRect(SKRect skRect, double scale = 1)
        {
            return new Rect(scale * skRect.Left, scale * skRect.Top, scale * skRect.Right, scale * skRect.Bottom);
        }
    }
}
// << datagrid-column-datagridcellrenderer
