using Microsoft.Maui.Controls;
using SDKBrowserMaui.Common;
using SDKBrowserMaui.Services;
using SDKBrowserMaui.ViewModels;
using System;
using System.ComponentModel;

namespace SDKBrowserMaui.Pages
{
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            var navigationService = DependencyService.Get<INavigationService>();
            await navigationService.NavigateBackAsync();
        }

        private void ExampleTapped(object sender, EventArgs e)
        {
            CategoryViewModel vm = (CategoryViewModel)this.BindingContext;
            BindableObject bindableObject = (BindableObject)sender;
            Example example = (Example)bindableObject.BindingContext;
            vm.NavigateTo(example);
        }
    }
}
