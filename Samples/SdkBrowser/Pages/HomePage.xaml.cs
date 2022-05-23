using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.ViewModels;
using System;
using System.ComponentModel;

namespace SDKBrowserMaui.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = new HomeViewModel();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
#if __ANDROID__
            //TODO: Remove this when NavigationPage starts using the Maui Handler instead of the old compat Renderer.
            if (e.PropertyName == nameof(this.Width) || e.PropertyName == nameof(this.Height))
            {
                this.Frame = this.Content.Frame;
            }
#endif
        }
    }
}
