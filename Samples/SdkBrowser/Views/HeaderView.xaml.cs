using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Services;
using SDKBrowserMaui.ViewModels;
using System;
using System.ComponentModel;

namespace SDKBrowserMaui.Views
{
    public partial class HeaderView : ContentView
    {
        private int tapCounter;

        public HeaderView()
        {
            InitializeComponent();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            var navigationService = DependencyService.Get<INavigationService>();
            await navigationService.NavigateBackAsync();
        }

        private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            this.tapCounter++;
            if (this.tapCounter > 8)
            {
                UIAutomation.IsEnabled = true;
            }
        }
    }
}
