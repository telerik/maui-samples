using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.BadgeViewControl.FeaturesCategory.BadgeAnimationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BadgeAnimation : RadContentView
    {
        public BadgeAnimation()
        {
            InitializeComponent();

            this.badgeView.BadgeAnimationEasing = Easing.BounceOut;
        }
    }
}