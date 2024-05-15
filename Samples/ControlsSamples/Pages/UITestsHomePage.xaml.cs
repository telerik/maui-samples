using Microsoft.Maui.Controls;
using QSF.ViewModels;
using QSF.Common;
using System;
using System.Linq;

namespace QSF.Pages;

public partial class UITestsHomePage : ContentPage
{
    private HomeViewModel viewModel = new HomeViewModel();

    public UITestsHomePage()
    {
        InitializeComponent();
        this.BindingContext = this.viewModel;
    }

    private void OnSearchTestTextChanged(object sender, TextChangedEventArgs e)
    {
        string currentText = e.NewTextValue;
        if (currentText.Length > 10 && currentText.FirstOrDefault() == '#' && currentText.LastOrDefault() == '#')
        {
            var splittedText = currentText.Replace("#", string.Empty).Split('.');
            string controlText = splittedText.First();
            string exampleText = splittedText.Last();

            Control control = this.viewModel.Controls.First(c => c.Name == controlText);
            control.StartupExample = control.Examples.FirstOrDefault(e => e.Name == exampleText);
            this.viewModel.SelectedControl = null;
            this.viewModel.SelectedControl = control;
        }
    }
}