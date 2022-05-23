using Microsoft.Maui.Controls.Xaml;
using System;
using Telerik.Maui.Controls;
using Telerik.XamarinForms.Primitives;

namespace SDKBrowserMaui.Examples.BadgeViewControl.FeaturesCategory.BadgeTypesExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BadgeTypes : RadContentView
    {
        public BadgeTypes()
        {
            InitializeComponent();
            // >> badgeview-badge-types-code-behind
            this.listView.ItemsSource = Enum.GetValues(typeof(BadgeType));
            // << badgeview-badge-types-code-behind
        }
    }
}