using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;

namespace QSF.Layouts;

public class UniformLayoutVerticalManager : ILayoutManager
{
    private readonly IUniformLayout layout;

    public UniformLayoutVerticalManager(IUniformLayout layout)
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
        double availableHeight = heightConstraint - totalSpacing;
        double w = widthConstraint;
        double h = this.layout.LayoutMode == UniformLayoutMode.VerticalStretch ? (availableHeight / count) : double.PositiveInfinity;
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

        double desiredHeight = (maxHeight * count) + totalSpacing;
        return new Size(maxWidth, desiredHeight);
    }

    public Size ArrangeChildren(Rect bounds)
    {
        int count = UniformLayoutManager.GetVisibleChildrenCount(this.layout);
        if (count == 0)
        {
            return Size.Zero;
        }

        Size desiredSize = this.Measure(bounds.Width, bounds.Height);
        double arrangeHeight = this.layout.LayoutMode == UniformLayoutMode.VerticalStretch ? Math.Max(desiredSize.Height, bounds.Height) : desiredSize.Height;
        double spacing = this.layout.Spacing;
        double totalSpacing = (count - 1) * spacing;
        double childHeight = (arrangeHeight - totalSpacing) / count;
        double childHeightWithSpacing = childHeight + spacing;
        double maxWidth = 0;
        double arrangedCount = 0;

        for (int i = 0; i < this.layout.Count; i++)
        {
            IView child = this.layout[i];

            if (child.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            double childY = bounds.Y + (arrangedCount * childHeightWithSpacing);
            Rect rect = new Rect(bounds.X, childY, bounds.Width, childHeight);
            Size childArrangeSize = child.Arrange(rect);

            if (maxWidth < childArrangeSize.Width)
            {
                maxWidth = childArrangeSize.Width;
            }

            arrangedCount++;
        }

        Size arrangeSize = new Size(maxWidth, arrangeHeight);
        return arrangeSize;
    }
}
