using System.Collections.Generic;
using Microsoft.Maui.Controls;
using System.Windows.Input;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.PromptInputControl.FeaturesCategory.AffixExample;

// >> promptinput-viewmodel-affix
public class AffixViewModel : NotifyPropertyChangedBase
{
    private string promptText = string.Empty;
    private bool isPopupOpen;
    private string selectedModel;

    public AffixViewModel()
    {
        this.OpenPopup = new Command(() => this.IsPopupOpen = true);
        this.ClosePopup = new Command(() => this.IsPopupOpen = false);

        this.ChatModels = new List<string>
        {
            "GPT-4o",
            "GPT-4o mini",
            "GPT-4.1",
            "Claude 3.5 Sonnet",
            "Claude 4 Opus",
            "Gemini 2.5 Pro",
            "Gemini 2.5 Flash",
            "LLaMA 4"
        };

        this.SelectedModel = this.ChatModels[0];
    }

    public ICommand OpenPopup { get; }

    public ICommand ClosePopup { get; }

    public List<string> ChatModels { get; }

    public string SelectedModel
    {
        get => this.selectedModel;
        set => this.UpdateValue(ref this.selectedModel, value);
    }

    public bool IsPopupOpen
    {
        get => this.isPopupOpen;
        set => this.UpdateValue(ref this.isPopupOpen, value);
    }

    public string ActiveTopic => "Customer Support Assistant";

    public string PromptText
    {
        get => this.promptText;
        set
        {
            if (this.promptText == value)
            {
                return;
            }

            this.promptText = value;
            this.OnPropertyChanged();
            this.OnPropertyChanged(nameof(this.CharacterCount));
        }
    }

    public string CharacterCount
    {
        get
        {
            if (this.PromptText == null)
            {
                return "0";
            }

            return this.PromptText.Length.ToString();
        }
    }
}
// << promptinput-viewmodel-affix
