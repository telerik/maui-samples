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
    }
}
