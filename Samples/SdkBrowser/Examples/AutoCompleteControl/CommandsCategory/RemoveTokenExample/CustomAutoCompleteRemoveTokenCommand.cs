using Telerik.Maui.Controls.AutoComplete;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.CommandsCategory.RemoveTokenExample;

// >> autocomplete-custom-removetokencommand
public class CustomAutoCompleteRemoveTokenCommand : AutoCompleteRemoveTokenCommand
{
    public override async void Execute(object parameter)
    {
        bool executeDefault = await App.Current.MainPage.DisplayAlert("Confirm", "Remove token?", "Yes", "No");
        if (executeDefault)
        {
            base.Execute(parameter);
        }
    }
}
// << autocomplete-custom-removetokencommand