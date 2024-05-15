using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Telerik.Maui.Controls.AIPrompt;

namespace QSF.Examples.AIPromptControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private const string AiItemTitle = "Generated with AI:";
    private const string AiItemLoadingText = "AI is processing your request...";
    private double iconFontSize = DeviceInfo.Platform == DevicePlatform.MacCatalyst ? 12 : 16;
    private string inputText;
    private IList<string> suggestions;
    private IList<AIPromptOutputItem> outputItems = new ObservableCollection<AIPromptOutputItem>();
    private IList<AIPromptCommandBase> commands = new ObservableCollection<AIPromptCommandBase>();
    private ICommand promptRequestCommand;
    private ICommand outputItemCopyCommand;
    private ICommand outputItemRetryCommand;
    private AiService aiService = new AiService();

    public FirstLookViewModel()
    {
        this.suggestions = new List<string>
        {
            "Generate key points of a well-written CV",
            "Generate a CV template"
        };

        this.commands.Add(new AIPromptCommand
        {
            Text = "Simplify",
            Command = new Command(this.ExecuteSimplifyCommand),
            ImageSource = new FontImageSource { FontFamily = "TelerikFontExamples", Size = this.iconFontSize, Glyph = "\ue8dd" }
        });

        this.commands.Add(new AIPromptCommand
        {
            Text = "Expand",
            Command = new Command(this.ExecuteExpandCommand),
            ImageSource = new FontImageSource { FontFamily = "TelerikFontExamples", Size = this.iconFontSize, Glyph = "\ue8de" }
        });

        this.commands.Add(new AIPromptCommandGroup
        {
            Text = "Translate",
            Commands = new List<AIPromptCommandBase>
            {
                new AIPromptCommand
                {
                    Text = "German",
                    Command = new Command(this.ExecuteTranslateToGermanCommand),
                    ImageSource = new FontImageSource { FontFamily = "TelerikFontExamples", Size = this.iconFontSize, Glyph = "\ue8df" }
                },
                new AIPromptCommand
                {
                    Text = "Spanish",
                    Command = new Command(this.ExecuteTranslateToSpanishCommand),
                    ImageSource = new FontImageSource { FontFamily = "TelerikFontExamples", Size = this.iconFontSize, Glyph = "\ue8df" }
                }
            }
        });

        this.promptRequestCommand = new Command(this.ExecutePromptRequestCommand, this.CanExecutePromptRequestCommand);
        this.outputItemCopyCommand = new Command(this.ExecuteOutputItemCopyCommand);
        this.outputItemRetryCommand = new Command(this.ExecuteOutputItemRetryCommand);
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

    public IList<AIPromptCommandBase> Commands => this.commands;

    public ICommand PromptRequestCommand => this.promptRequestCommand;

    public ICommand OutputItemCopyCommand { get { return this.outputItemCopyCommand; } }

    public ICommand OutputItemRetryCommand { get { return this.outputItemRetryCommand; } }

    private async void AddOutputItem(string instruction, string inputText)
    {
        AIPromptOutputItem outputItem = new AIPromptOutputItem();
        outputItem.Title = AiItemTitle;
        outputItem.InputText = inputText;
        outputItem.ResponseText = AiItemLoadingText;
        this.OutputItems.Insert(0, outputItem);

        AiItem aiItem = await this.aiService.GetLLMResponseAsync(instruction, inputText);
        outputItem.Data = aiItem;
        outputItem.ResponseText = aiItem.response;
    }

    private async void RetryAddingOutputItem(AiItem oldAiItem)
    {
        AIPromptOutputItem outputItem = new AIPromptOutputItem();
        outputItem.Title = AiItemTitle;
        outputItem.InputText = oldAiItem.inputText;
        outputItem.ResponseText = AiItemLoadingText;
        this.OutputItems.Insert(0, outputItem);

        AiItem aiItem = await this.aiService.RetryGettingLLMResponseAsync(oldAiItem);
        outputItem.Data = aiItem;
        outputItem.ResponseText = aiItem.response;
    }

    private bool CanExecutePromptRequestCommand(object arg)
    {
        string text = (string)arg;
        return !string.IsNullOrEmpty(text?.Trim());
    }

    private void ExecutePromptRequestCommand(object arg)
    {
        this.AddOutputItem(this.inputText, this.inputText);
        this.InputText = string.Empty;
    }

    private void ExecuteOutputItemCopyCommand(object arg)
    {
        AIPromptOutputItem outputItem = (AIPromptOutputItem)arg;
        AiItem aiItem = (AiItem)outputItem.Data;

#if ANDROID || IOS
        Clipboard.SetTextAsync(aiItem.responseHtml);
#else
        Clipboard.SetTextAsync(aiItem.response);
#endif
    }

    private void ExecuteOutputItemRetryCommand(object arg)
    {
        AIPromptOutputItem outputItem = (AIPromptOutputItem)arg;
        AiItem aiItem = (AiItem)outputItem.Data;
        this.RetryAddingOutputItem(aiItem);
        this.InputText = string.Empty;
    }

    private string GetLatestOutputItemInputText()
    {
        if (this.OutputItems.Count >= 1)
        {
            return this.OutputItems.First().InputText;
        }
        else
        {
            return string.Empty;
        }
    }

    private void ExecuteSimplifyCommand(object arg)
    {
        this.AddOutputItem("Simplify this: ", this.GetLatestOutputItemInputText());
    }

    private void ExecuteExpandCommand(object arg)
    {
        this.AddOutputItem("Expand this: ", this.GetLatestOutputItemInputText());
    }

    private void ExecuteTranslateToGermanCommand(object arg)
    {
        this.AddOutputItem("Translate to German: ", this.GetLatestOutputItemInputText());
    }

    private void ExecuteTranslateToSpanishCommand(object arg)
    {
        this.AddOutputItem("Translate to Spanish: ", this.GetLatestOutputItemInputText());
    }
}
