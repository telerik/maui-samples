using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.FeaturesCategory.ConfigurationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationSettings : RadContentView
    {
        public ConfigurationSettings()
        {
            InitializeComponent();
            this.BindingContext = new ViewModel();
        }
    }
}