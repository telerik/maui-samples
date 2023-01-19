using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;

namespace QSF.Layouts;

public class UniformLayoutHorizontalManager : ILayoutManager
{
    private readonly IUniformLayout layout;

    public UniformLayoutHorizontalManager(IUniformLayout layout)
    {
        this.layout = layout;
    }

    public Size Measure(double widthConstraint, double heightConstraint)
    {
        int count = UniformLayoutManager.GetVisibleChildrenCount(this.layout);
        if (count == 0)
        {
            return Size.Zero;
        }

        double itemSpacing = this.layout.Spacing;
        double totalSpacing = (count - 1) * itemSpacing;
        double availableWidth = widthConstraint - totalSpacing;
        double w = this.layout.LayoutMode == UniformLayoutMode.HorizontalStretch ? (availableWidth / count) : double.PositiveInfinity;
        double h = heightConstraint;
        double maxWidth = 0;
        double maxHeight = 0;

        for (int i = 0; i < this.layout.Count; i++)
        {
            IView child = this.layout[i];

            if (child.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            Size desiredSize = child.Measure(w, h);

            if (maxWidth < desiredSize.Width)
            {
                maxWidth = desiredSize.Width;
            }

            if (maxHeight < desiredSize.Height)
            {
                maxHeight = desiredSize.Height;
            }
        }

        double desiredWidth = (maxWidth * count) + totalSpacing;
        return new Size(desiredWidth, maxHeight);
    }

    public Size ArrangeChildren(Rect bounds)
    {
        int count = UniformLayoutManager.GetVisibleChildrenCount(this.layout);
        if (count == 0)
        {
            return Size.Zero;
        }

        Size desiredSize = this.layout.DesiredSize;
        double arrangeWidth = this.layout.LayoutMode == UniformLayoutMode.HorizontalStretch ? Math.Max(desiredSize.Width, bounds.Width) : desiredSize.Width;
        double spacing = this.layout.Spacing;
        double totalSpacing = (count - 1) * spacing;
        double childWidth = (arrangeWidth - totalSpacing) / count;
        double childWidthWithSpacing = childWidth + spacing;
        double maxHeight = 0;
        double arrangedCount = 0;

        for (int i = 0; i < this.layout.Count; i++)
        {
            IView child = this.layout[i];

            if (child.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            double childX = bounds.X + (arrangedCount * childWidthWithSpacing);
            Rect rect = new Rect(childX, bounds.Y, childWidth, bounds.Height);
            Size childArrangeSize = child.Arrange(rect);

            if (maxHeight < childArrangeSize.Height)
            {
                maxHeight = childArrangeSize.Height;
            }

            arrangedCount++;
        }

        Size arrangeSize = new Size(arrangeWidth, maxHeight);
        return arrangeSize;
    }
}
