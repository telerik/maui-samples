using Telerik.Maui.Controls.ComboBox;

namespace SDKBrowserMaui.Examples.ComboBoxControl.CommandsCategory.RemoveTokenExample;

// >> combobox-custom-removetokencommand
public class CustomComboBoxRemoveTokenCommand : ComboBoxRemoveTokenCommand
{
    public override async void Execute(object parameter)
    {
        bool executeDefault = await App.Current.Windows[0].Page.DisplayAlert("Confirm", "Remove token?", "Yes", "No");
        if (executeDefault)
        {
            base.Execute(parameter);
        }
    }
}
// << combobox-custom-removetokencommand
