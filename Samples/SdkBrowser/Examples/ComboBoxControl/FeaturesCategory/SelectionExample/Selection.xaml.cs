using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.FeaturesCategory.SelectionExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Selection : RadContentView
    {
        public Selection()
        {
            InitializeComponent();
        }

        private void RadComboBox_SelectionChanged(object sender, ComboBoxSelectionChangedEventArgs e)
        {
            // implement your logic here
        }
    }
}