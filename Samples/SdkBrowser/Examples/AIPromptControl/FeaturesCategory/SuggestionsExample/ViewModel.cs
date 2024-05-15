using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.AIPrompt;


namespace SDKBrowserMaui.Examples.AIPromptControl.FeaturesCategory.SuggestionsExample;

// >> aiprompt-suggestions-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private string inputText = string.Empty;
    private IList<AIPromptOutputItem> outputItems = new ObservableCollection<AIPromptOutputItem>();
    private ICommand promptRequestCommand;
    private IList<string> suggestions;

    public ViewModel()
    {
        this.promptRequestCommand = new Command(this.ExecutePromptRequestCommand, this.CanExecutePromptRequestCommand);
        this.suggestions = new List<string>
        {
            "What is the current temperature in NYC",
            "Create a generic birthday invitation for my birthday",
            "Tell me the Taurus daily horoscope for today",
        };
    }

    public string InputText { get { return this.inputText; } set { this.UpdateValue(ref this.inputText, value); } }
    public IList<AIPromptOutputItem> OutputItems { get { return this.outputItems; } }
    public ICommand PromptRequestCommand { get { return this.promptRequestCommand; } }
    public IList<string> Suggestions { get { return this.suggestions; } }

    private bool CanExecutePromptRequestCommand(object arg)
    {
        string text = (string)arg;
        return !string.IsNullOrEmpty(text?.Trim());
    }

    private void ExecutePromptRequestCommand(object arg)
    {
        AIPromptOutputItem outputItem = new AIPromptOutputItem
        {
            Title = "Generated with AI:",
            InputText = arg?.ToString(),
            ResponseText = "This is the response from the AI in relation to your request. For real prompt processing, please connect the component to a preferred AI service."
        };

        this.OutputItems.Insert(0, outputItem);
        this.InputText = string.Empty;
    }
}
// << aiprompt-suggestions-viewmodel