using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;
using QSF.ViewModels;
using QSF.Common;
using System.Linq;
using QSF.Services;
using System.Globalization;


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

        private async void OnExampleSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string currentText = e.NewTextValue;
            if (currentText.Length > 10 && currentText.FirstOrDefault() == '#' && currentText.LastOrDefault() == '#')
            {
                var examples = ((Application.Current.MainPage as NavigationPage).RootPage.BindingContext as HomeViewModel)?.Examples;

                var splittedText = currentText.Replace("#", string.Empty).Split('.');
                string controlText = splittedText.First();
                string exampleText = splittedText.Last();

                Example example = examples.FirstOrDefault(e => e.ControlName == controlText && e.Name == exampleText);

                var service = DependencyService.Get<INavigationService>();
                service.NavigateToExampleAsync(example);
            }
        }
    }
}
