using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.FeaturesCategory.EditModeAndSearchExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditModeAndSearch : RadContentView
    {
        public EditModeAndSearch()
        {
            InitializeComponent();
            this.BindingContext = new ViewModel();
        }
    }
}