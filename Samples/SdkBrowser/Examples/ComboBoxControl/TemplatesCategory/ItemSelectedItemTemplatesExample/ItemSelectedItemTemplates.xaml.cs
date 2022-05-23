using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.TemplatesCategory.ItemSelectedItemTemplatesExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemSelectedItemTemplates : RadContentView
    {        
        public ItemSelectedItemTemplates()
        {
            InitializeComponent();
            this.BindingContext = new ViewModel();
        }
    }
}