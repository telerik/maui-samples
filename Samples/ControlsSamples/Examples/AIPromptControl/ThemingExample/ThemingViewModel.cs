using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Maui.Controls.AIPrompt;

namespace QSF.Examples.AIPromptControl.ThemingExample;

public class ThemingViewModel : ExampleViewModel
{
    private string inputText;
    private IList<string> suggestions;
    private IList<AIPromptOutputItem> outputItems = new ObservableCollection<AIPromptOutputItem>();

    private ICommand promptRequestCommand;

    public ThemingViewModel()
    {
        this.promptRequestCommand = new Command(this.ExecutePromptRequestCommand, this.CanExecutePromptRequestCommand);
        this.suggestions = new List<string>
        {
            "What is the current temperature in NYC",
            "Create a generic birthday invitation for my birthday",
            "Tell me the Taurus daily horoscope for today",
        };
    }

    public string InputText
    {
        get => this.inputText;
        set
        {
            if (this.UpdateValue(ref this.inputText, value))
            {
                ((Command)this.PromptRequestCommand).ChangeCanExecute();
            }
        }
    }

    public IList<string> Suggestions => this.suggestions;
    public IList<AIPromptOutputItem> OutputItems => this.outputItems;

    public ICommand PromptRequestCommand => this.promptRequestCommand;

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
            ResponseText = "This is the response from the AI in relation to your request."
        };

        this.OutputItems.Insert(0, outputItem);
        this.InputText = string.Empty;
    }
}
