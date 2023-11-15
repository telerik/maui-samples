using Microsoft.Maui.Controls;
using SDKBrowserMaui.Services;
using System;
using System.ComponentModel;

namespace SDKBrowserMaui.Pages
{
    public partial class ExamplePage : ContentPage
    {
        public ExamplePage()
        {
            InitializeComponent();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            var navigationService = DependencyService.Get<INavigationService>();
            await navigationService.NavigateBackAsync();
        }
    }
}
