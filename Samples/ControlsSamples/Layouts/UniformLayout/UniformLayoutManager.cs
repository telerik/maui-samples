using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System.Linq;

namespace QSF.Layouts;

public class UniformLayoutManager : ILayoutManager
{
    private readonly IUniformLayout layout;

    private ILayoutManager horizontalManager;
    private ILayoutManager verticalManager;

    public UniformLayoutManager(IUniformLayout layout)
    {
        this.layout = layout;
    }

    public Size Measure(double widthConstraint, double heightConstraint)
    {
        ILayoutManager manager = this.GetOrCreateManager();
        return manager.Measure(widthConstraint, heightConstraint);
    }

    public Size ArrangeChildren(Rect bounds)
    {
        ILayoutManager manager = this.GetOrCreateManager();
        return manager.ArrangeChildren(bounds);
    }

    internal static int GetVisibleChildrenCount(ILayout layout)
    {
        int count = layout.Count(c => c.Visibility != Visibility.Collapsed);
        return count;
    }

    private ILayoutManager GetOrCreateManager()
    {
        if (this.layout.LayoutMode == UniformLayoutMode.HorizontalStretch ||
            this.layout.LayoutMode == UniformLayoutMode.HorizontalStack)
        {
            if (this.horizontalManager == null)
            {
                this.horizontalManager = new UniformLayoutHorizontalManager(this.layout);
            }

            return this.horizontalManager;
        }
        else
        {
            if (this.verticalManager == null)
            {
                this.verticalManager = new UniformLayoutVerticalManager(this.layout);
            }

            return this.verticalManager;
        }
    }
}
