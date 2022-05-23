using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.TemplatesCategory.TokenTemplateExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TokenTemplate : RadContentView
    {
        public TokenTemplate()
        {
            InitializeComponent();
            this.BindingContext = new ViewModel();
        }

        // >> remove-the-selecteditem
        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var closeLabel = sender as Label;
            if (closeLabel != null)
            {
                var item = closeLabel.BindingContext as City;
                if (item != null)
                {
                    this.comboBox.SelectedItems.Remove(item);
                }
            }
        }
        // << remove-the-selecteditem
    }
}