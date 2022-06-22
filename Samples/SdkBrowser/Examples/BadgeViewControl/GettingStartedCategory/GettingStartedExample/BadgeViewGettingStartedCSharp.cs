using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Primitives;

namespace SDKBrowserMaui.Examples.BadgeViewControl.GettingStartedCategory.GettingStartedExample
{
    public class BadgeViewGettingStartedCSharp : RadContentView
    {
        public BadgeViewGettingStartedCSharp()
        {
            // >> badgeview-getting-started-csharp
            var badgeView = new RadBadgeView();
            badgeView.BadgeText = "1";
            badgeView.Content = new RadBorder
            {
                WidthRequest = 80,
                HeightRequest = 80,
                BorderThickness = 1,
                BorderColor = Colors.LightGray,
                Content = new Label
                {
                    Text = "Telerik Badge View for MAUI",
                    FontSize = 14,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                },
            };
            // << badgeview-getting-started-csharp

            var panel = new VerticalStackLayout();
            panel.HorizontalOptions = LayoutOptions.Center;
            panel.Children.Add(badgeView);
            this.Content = panel;
        }
    }
}