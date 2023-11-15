using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;
using QSF.ViewModels;


namespace QSF.Pages
{
    public partial class ExamplePage : ContentPage
    {
        public ExamplePage()
        {
            InitializeComponent();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            ExampleViewModel vm = (ExampleViewModel)this.BindingContext;
            await vm.NavigationService.NavigateBackAsync();
        }
    }
}
