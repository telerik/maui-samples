using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Maui.Controls.PromptInput;

namespace QSF.Examples.PromptInputControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private string lastExecutedCommand;
    private string promptInputText;
    private string lastSelectedKey = string.Empty;
    private bool isSemanticSearchToggled = false;
    private bool isSummarizationToggled = false;
    private bool isWritingToggled = false;
    private ObservableCollection<PromptInputAttachedFile> attachedFiles;

    private readonly Dictionary<string, string> promptTexts = new()
    {
        { "Semantic Search", "Find inconsistencies with our design system." },
        { "Summarization", "Summarize UX issues and identify risks." },
        { "Writing", "Rewrite microcopy for greater clarity and tone." },
        { "Analyze Style Consistency", "Analyze this content for UX and visual style consistency. Highlight inconsistencies and suggest improvements." },
        { "Convert to Table", "Convert the key points and insights from this content into a clear, structured table." },
        { "Optimize suggestions", "Optimize the suggestions to be clearer, more actionable, and suitable for sharing with stakeholders." },
    };

    public FirstLookViewModel()
    {
        this.SemanticSearchCommand = new Command(() =>
        {
            if (this.IsSemanticSearchToggled)
            {
                this.OnButtonExecuted(this.SemanticSearchText);
            }
        });

        this.SummarizationCommand = new Command(() =>
        {
            if (this.IsSummarizationToggled)
            {
                this.OnButtonExecuted(this.SummarizationText);
            }
        });

        this.WritingCommand = new Command(() =>
        {
            if (this.IsWritingToggled)
            {
                this.OnButtonExecuted(this.WritingText);
            }
        });

        this.AnalyzeStyleCommand = new Command(() =>
        {
            this.UntoggleAll();
            this.OnButtonExecuted(this.AnalyzeStyleText);
        });

        this.ConvertToTableCommand = new Command(() =>
        {
            this.UntoggleAll();
            this.OnButtonExecuted(this.ConvertToTableText);
        });

        this.OptimizeSuggestionsCommand = new Command(() =>
        {
            this.UntoggleAll();
            this.OnButtonExecuted(this.OptimizeSuggestionsText);
        });

        this.SendMessageCommand = new SendMessageCommand(this.OnSendMessage);

        this.LastExecutedCommand = string.Empty;
        this.PromptInputText = string.Empty;
        this.AttachedFiles = new ObservableCollection<PromptInputAttachedFile>();
    }

    public string SemanticSearchText => "Semantic Search";

    public string SummarizationText => "Summarization";

    public string WritingText => "Writing";

    public string AnalyzeStyleText => "Analyze Style Consistency";

    public string ConvertToTableText => "Convert to Table";

    public string OptimizeSuggestionsText => "Optimize suggestions";

    public bool IsSemanticSearchToggled
    {
        get => this.isSemanticSearchToggled;
        set
        {
            if (this.UpdateValue(ref this.isSemanticSearchToggled, value) && value)
            {
                this.isSummarizationToggled = false;
                this.isWritingToggled = false;
                this.OnPropertyChanged(nameof(this.IsSummarizationToggled));
                this.OnPropertyChanged(nameof(this.IsWritingToggled));
            }
        }
    }

    public bool IsSummarizationToggled
    {
        get => this.isSummarizationToggled;
        set
        {
            if (this.UpdateValue(ref this.isSummarizationToggled, value) && value)
            {
                this.isSemanticSearchToggled = false;
                this.isWritingToggled = false;
                this.OnPropertyChanged(nameof(this.IsSemanticSearchToggled));
                this.OnPropertyChanged(nameof(this.IsWritingToggled));
            }
        }
    }

    public bool IsWritingToggled
    {
        get => this.isWritingToggled;
        set
        {
            if (this.UpdateValue(ref this.isWritingToggled, value) && value)
            {
                this.isSemanticSearchToggled = false;
                this.isSummarizationToggled = false;
                this.OnPropertyChanged(nameof(this.IsSemanticSearchToggled));
                this.OnPropertyChanged(nameof(this.IsSummarizationToggled));
            }
        }
    }

    public string PromptInputText
    {
        get => this.promptInputText;
        set => this.UpdateValue(ref this.promptInputText, value);
    }

    public string LastExecutedCommand
    {
        get => this.lastExecutedCommand;
        set => this.UpdateValue(ref this.lastExecutedCommand, value);
    }

    public ObservableCollection<PromptInputAttachedFile> AttachedFiles
    {
        get => this.attachedFiles;
        set => this.UpdateValue(ref this.attachedFiles, value);
    }

    public ICommand SemanticSearchCommand { get; }

    public ICommand SummarizationCommand { get; }

    public ICommand WritingCommand { get; }

    public ICommand AnalyzeStyleCommand { get; }

    public ICommand ConvertToTableCommand { get; }

    public ICommand OptimizeSuggestionsCommand { get; }

    public ICommand SendMessageCommand { get; }

    private void OnSendMessage()
    {
        this.LastExecutedCommand = string.IsNullOrEmpty(this.lastSelectedKey)
            ? this.PromptInputText
            : this.lastSelectedKey;
        this.UntoggleAll();
        this.PromptInputText = string.Empty;
        
        if (this.AttachedFiles != null & this.AttachedFiles.Count >= 0)
        {
            this.AttachedFiles.Clear();
        }
    }

    private void UntoggleAll()
    {
        this.IsSemanticSearchToggled = false;
        this.IsSummarizationToggled = false;
        this.IsWritingToggled = false;
        this.lastSelectedKey = string.Empty;
    }

    private void OnButtonExecuted(string label)
    {
        if (this.promptTexts.TryGetValue(label, out string promptText))
        {
            this.PromptInputText = promptText;
            this.lastSelectedKey = label;
        }
        else
        {
            this.PromptInputText = string.Empty;
            this.lastSelectedKey = string.Empty;
        }
    }

}

internal class SendMessageCommand : PromptInputSendCommand
{
    private readonly Action sendMessageAction;

    public SendMessageCommand(Action sendMessageAction)
    {
        this.sendMessageAction = sendMessageAction;
    }

    public override void Execute(object parameter)
    {
        base.Execute(parameter);
        this.sendMessageAction?.Invoke();
    }
}
