using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.TemplatesCategory.HeaderFooterExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderFooter : RadContentView
    {
        ViewModel vm;

        public HeaderFooter()
        {
            InitializeComponent();
            this.vm = new ViewModel();
            this.BindingContext = vm;
        }

        // >> combobox-footer-template-button-clicked
        private void Button_Clicked(object sender, System.EventArgs e)
        {
            this.vm.Items.Add(new City { Name = "Sofia", Population = 1243000 });
        }
        // << combobox-footer-template-button-clicked
    }
}