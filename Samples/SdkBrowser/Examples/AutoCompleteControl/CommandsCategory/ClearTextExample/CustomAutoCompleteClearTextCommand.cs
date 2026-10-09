using Telerik.Maui.Controls.AutoComplete;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.CommandsCategory.ClearTextExample;

// >> autocomplete-custom-cleartextcommand
public class CustomAutoCompleteClearTextCommand : AutoCompleteClearTextCommand
{
    public override async void Execute(object parameter)
    {
        int recipientsCount = this.AutoComplete?.Tokens?.Count ?? 0;
        if (recipientsCount >= 1)
        {
            bool executeDefault = await App.Current.Windows[0].Page.DisplayAlert("Clear Recipients", $"Are you sure you want to clear the recipients?", "Yes", "No");
            if (!executeDefault)
            {
                return;
            }
        }

        base.Execute(parameter);
    }
}
// << autocomplete-custom-cleartextcommand
