using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.GettingStartedCategory.GettingStartedExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComboBoxGettingStartedXaml : RadContentView
    {
        public ComboBoxGettingStartedXaml()
        {
            InitializeComponent();
            this.BindingContext = new ViewModel();
        }
    }
}