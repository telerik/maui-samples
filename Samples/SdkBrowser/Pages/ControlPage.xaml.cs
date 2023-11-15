using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Services;
using SDKBrowserMaui.ViewModels;
using System;
using System.ComponentModel;

namespace SDKBrowserMaui.Pages
{
    public partial class ControlPage : ContentPage
    {
        public ControlPage()
        {
            InitializeComponent();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            var navigationService = DependencyService.Get<INavigationService>();
            await navigationService.NavigateBackAsync();
        }

        private void CategoryTapped(object sender, EventArgs e)
        {
            ControlViewModel vm = (ControlViewModel)this.BindingContext;
            BindableObject bindableObject = (BindableObject)sender;
            Category category = (Category)bindableObject.BindingContext;
            vm.NavigateTo(category);
        }
    }
}
