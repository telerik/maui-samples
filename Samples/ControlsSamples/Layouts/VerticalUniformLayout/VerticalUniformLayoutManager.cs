using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;

namespace QSF.Layouts;

public class VerticalUniformLayoutManager : ILayoutManager
{
    private readonly ILayout layout;

    public VerticalUniformLayoutManager(ILayout layout)
    {
        this.layout = layout;
    }

    public Size Measure(double widthConstraint, double heightConstraint)
    {
        UniformInfo uniformInfo = this.GetUniformInfo(widthConstraint);
        return new Size(uniformInfo.cols * uniformInfo.childMaxSize.Width, uniformInfo.rows * uniformInfo.childMaxSize.Height);
    }

    public Size ArrangeChildren(Rect bounds)
    {
        UniformInfo uniformInfo = this.GetUniformInfo(bounds.Width);
        if (uniformInfo.visibleChildrenCount == 0)
        {
            return Size.Zero;
        }

        double childWidth = bounds.Width / uniformInfo.cols;
        double childHeight = uniformInfo.childMaxSize.Height;
        int index = 0;
        double x;
        double y;

        foreach (IView view in this.layout)
        {
            if (view.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            int col = index % uniformInfo.cols;
            x = bounds.X + (col * childWidth);
            int row = index / uniformInfo.cols;
            y = bounds.Y + (row * childHeight);

            view.Arrange(new Rect(x, y, childWidth, childHeight));
            index++;
        }

        return new Size(uniformInfo.cols * childWidth, uniformInfo.rows * childHeight);
    }

    private UniformInfo GetUniformInfo(double widthConstraint)
    {
        int visibleChildrenCount;
        Size maxSize = this.GetMaxSize(double.PositiveInfinity, out visibleChildrenCount);

        if (maxSize.Width == 0)
        {
            return new UniformInfo(visibleChildrenCount, maxSize, visibleChildrenCount, 1);
        }

        double colsCountApprox = widthConstraint / maxSize.Width;
        int cols = colsCountApprox > 0 ? (int)Math.Floor(colsCountApprox) : 1;
        cols = cols > 0 ? cols : 1;
        int rows = (int)Math.Ceiling(visibleChildrenCount / (double)cols);

        double actualAvailableWidth = widthConstraint / cols;
        if (0 < actualAvailableWidth && actualAvailableWidth < double.MaxValue)
        {
            maxSize = this.GetMaxSize(actualAvailableWidth, out visibleChildrenCount);
        }

        return new UniformInfo(visibleChildrenCount, maxSize, cols, rows);
    }

    private Size GetMaxSize(double measureWidth, out int visibleChildrenCount)
    {
        visibleChildrenCount = 0;
        Size maxSize = new Size();

        foreach (IView view in this.layout)
        {
            if (view.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            Size desiredSize = view.Measure(measureWidth, double.PositiveInfinity);
            maxSize.Width = Math.Max(maxSize.Width, desiredSize.Width);
            maxSize.Height = Math.Max(maxSize.Height, desiredSize.Height);
            visibleChildrenCount++;
        }

        return maxSize;
    }

    struct UniformInfo
    {
        internal int visibleChildrenCount;
        internal Size childMaxSize;
        internal int cols;
        internal int rows;

        internal UniformInfo(int visibleChildrenCount, Size childMaxSize, int cols, int rows) : this()
        {
            this.visibleChildrenCount = visibleChildrenCount;
            this.childMaxSize = childMaxSize;
            this.cols = cols;
            this.rows = rows;
        }
    }
}
