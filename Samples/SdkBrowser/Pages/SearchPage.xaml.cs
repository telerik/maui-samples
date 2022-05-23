using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.ViewModels;
using System;
using System.ComponentModel;

namespace SDKBrowserMaui.Pages
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}
