using Microsoft.Maui.Controls;
using SDKBrowserMaui.ViewModels;

namespace SDKBrowserMaui.Pages
{
    public partial class UITestsHomePage : ContentPage
    {
        public UITestsHomePage()
        {
            InitializeComponent();
            this.BindingContext = new UITestsHomeViewModel();
        }

        private void button_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}
