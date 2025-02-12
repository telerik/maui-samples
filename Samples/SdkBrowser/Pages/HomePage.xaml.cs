using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.ViewModels;
using System;
using System.ComponentModel;

namespace SDKBrowserMaui.Pages
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel vm;

        public HomePage()
        {
            InitializeComponent();

            this.vm = new HomeViewModel();
            this.BindingContext = this.vm;
        }
    }
}
