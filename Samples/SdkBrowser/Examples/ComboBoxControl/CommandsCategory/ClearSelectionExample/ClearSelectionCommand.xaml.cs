using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls.ComboBox;

namespace SDKBrowserMaui.Examples.ComboBoxControl.CommandsCategory.ClearSelectionExample;

public partial class ClearSelectionCommand : ContentView
{
    public ClearSelectionCommand()
    {
        InitializeComponent();

        this.BindingContext = new ViewModel();
    }

    // >> combobox-commands-csharp
    class CustomClearSelectionCommand : ComboBoxClearSelectionCommand
    {
        public override async void Execute(object parameter)
        {
            bool executeDefault = await App.Current.MainPage.DisplayAlert("Command", "Execute command?", "Yes", "No");
            if (executeDefault)
            {
                base.Execute(parameter);
            }
        }
    }

    private void ExecuteCustomCommanClicked(object sender, EventArgs e)
    {
        this.comboBox.ClearSelectionCommand = new CustomClearSelectionCommand();
        this.comboBox.ClearSelectionCommand.Execute(true);
    }
    // << combobox-commands-csharp
}