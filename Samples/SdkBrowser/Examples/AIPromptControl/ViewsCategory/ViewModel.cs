using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Maui;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.AIPrompt;

namespace SDKBrowserMaui.Examples.AIPromptControl.ViewsCategory;

// >> aiprompt-views-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private string inputText = string.Empty;
    private IList<AIPromptOutputItem> outputItems = new ObservableCollection<AIPromptOutputItem>();
    private ICommand promptRequestCommand;
    private ICommand copyCommand;
    private ICommand retryCommand;
    private IList<AIPromptCommandBase> commands = new ObservableCollection<AIPromptCommandBase>();
    private ICommand commandTappedCommand;
    private ICommand outputItemRatingChangedCommand;

    public ViewModel()
    {
        this.promptRequestCommand = new Command(this.ExecutePromptRequestCommand, this.CanExecutePromptRequestCommand);

        this.copyCommand = new Command(this.ExecuteCopyCommand);
        this.retryCommand = new Command(this.ExecuteRetryCommand);

        this.commands.Add(new AIPromptCommand { ImageSource = new FontImageSource() { FontFamily = TelerikFont.Name, Size = 12, Glyph = TelerikFont.IconPaste}, Text = "Simplify", });
        this.commands.Add(new AIPromptCommand { ImageSource = new FontImageSource() { FontFamily = TelerikFont.Name, Size = 12, Glyph = TelerikFont.IconReset }, Text = "Check Syntax" });
        this.commandTappedCommand = new Command(this.ExecuteCommandTappedCommand);

        this.outputItemRatingChangedCommand = new Command(this.ExecuteOutputItemRatingChangedCommand);
    }

    public string InputText { get { return this.inputText; } set { this.UpdateValue(ref this.inputText, value); } }
    public IList<AIPromptOutputItem> OutputItems { get { return this.outputItems; } }
    public ICommand PromptRequestCommand { get { return this.promptRequestCommand; } }
    public ICommand CopyCommand { get { return this.copyCommand; } }
    public ICommand RetryCommand { get { return this.retryCommand; } }
    public IList<AIPromptCommandBase> Commands { get { return this.commands; } }
    public ICommand CommandTappedCommand { get { return this.commandTappedCommand; } }
    public ICommand OutputItemRatingChangedCommand { get { return this.outputItemRatingChangedCommand; } }

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

    private async void ExecuteCopyCommand(object arg)
    {
        AIPromptOutputItem outputItem = (AIPromptOutputItem)arg;
        await Clipboard.Default.SetTextAsync(outputItem.ResponseText);
        await Application.Current.MainPage.DisplayAlert("Copied to clipboard:", outputItem.ResponseText, "Ok");
    }

    private void ExecuteRetryCommand(object arg)
    {
        AIPromptOutputItem outputItem = (AIPromptOutputItem)arg;
        this.ExecutePromptRequestCommand(outputItem.InputText);
        this.InputText = string.Empty;
    }

    private void ExecuteOutputItemRatingChangedCommand(object arg)
    {
        AIPromptOutputItem outputItem = (AIPromptOutputItem)arg;
        Application.Current.MainPage.DisplayAlert("Response rating changed:", $"The rating of response {outputItems.IndexOf(outputItem)} has changed.", "Ok");
    }

    private void ExecuteCommandTappedCommand(object arg)
    {
        AIPromptCommand aiPromptCommand = arg as AIPromptCommand;

        if (aiPromptCommand == null)
        {
            return;
        }

        Application.Current.MainPage.DisplayAlert("Command executed:", aiPromptCommand.Text + " command has been executed.", "Ok");
    }
}
// << aiprompt-views-viewmodel