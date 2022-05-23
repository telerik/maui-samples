using Microsoft.Maui.Controls.Xaml;
using System;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.BadgeViewControl.FeaturesCategory.BadgeViewContentExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // >> badgeview-content-code-behind
    public partial class BadgeViewContent : RadContentView
    {
        private int badgeCounter = 0;

        public BadgeViewContent()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            badgeCounter++;
            badgeView.BadgeText = badgeCounter.ToString();
        }
    }
    // << badgeview-content-code-behind
}