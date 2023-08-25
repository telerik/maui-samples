using Telerik.Maui.Controls;
using Telerik.Maui.Controls.ComboBox;
using System;

namespace SDKBrowserMaui.Examples.ComboBoxControl.FeaturesCategory.CommandsExample;

public partial class Commands : RadContentView
{
    public Commands()
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