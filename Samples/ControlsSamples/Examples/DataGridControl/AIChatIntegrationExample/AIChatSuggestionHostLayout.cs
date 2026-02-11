using System.Linq;
using Microsoft.Maui.Platform;
using Telerik.Maui.Controls;

namespace QSF.Examples.DataGridControl.AIChatIntegrationExample;

public class AIChatSuggestionHostLayout : RadLayout
{
}

#if MACCATALYST
public class AIChatSuggestionHostLayoutHandler : Microsoft.Maui.Handlers.LayoutHandler
{
    protected override AIChatSuggestionHostLayoutView CreatePlatformView()
        => new AIChatSuggestionHostLayoutView();
}

public class AIChatSuggestionHostLayoutView : LayoutView
{
    public override UIKit.UIView HitTest(CoreGraphics.CGPoint point, UIKit.UIEvent uievent)
    {
        if (point.X < 0 || point.Y < 0 || point.X > this.Bounds.Width || point.Y > this.Bounds.Height)
        {
            return base.HitTest(point, uievent);
        }

        var child = this.Subviews.FirstOrDefault();
        if (child == null)
        {
            return base.HitTest(point, uievent);
        }

        var childPoint = child.ConvertPointFromView(point, this);

        foreach (var subview in child.Subviews)
        {
            if (subview.Frame.Contains(childPoint))
            {
                var subviewPoint = subview.ConvertPointFromView(point, this);
                var hitResult = subview.HitTest(subviewPoint, uievent);
                if (hitResult != null)
                {
                    return hitResult;
                }
            }
        }

        return base.HitTest(point, uievent);
    }
}
#endif