using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace QSF.Examples.DataGridControl.AIChatIntegrationExample;

public partial class AIChatMessageView : ContentView
{
    public static readonly BindableProperty MessageTextProperty =
        BindableProperty.Create(nameof(MessageText), typeof(string), typeof(AIChatMessageView), null);

    public static readonly BindableProperty TimeStampProperty =
        BindableProperty.Create(nameof(TimeStamp), typeof(DateTime?), typeof(AIChatMessageView), null);

    private RadToggleButton thumbsUpButton;
    private RadToggleButton thumbsDownButton;
    private RadTemplatedButton copyButton;
    private bool isInternalChange;

    public AIChatMessageView()
    {
        this.InitializeComponent();
    }

    public string MessageText
    {
        get => (string)this.GetValue(MessageTextProperty);
        set => this.SetValue(MessageTextProperty, value);
    }

    public DateTime? TimeStamp
    {
        get => (DateTime?)this.GetValue(TimeStampProperty);
        set => this.SetValue(TimeStampProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        if (this.thumbsUpButton != null)
        {
            this.thumbsUpButton.IsToggledChanged -= this.OnThumbsUpIsToggledChanged;
        }

        if (this.thumbsDownButton != null)
        {
            this.thumbsDownButton.IsToggledChanged -= this.OnThumbsDownIsToggledChanged;
        }

        if (this.copyButton != null)
        {
            this.copyButton.Clicked -= this.OnCopyButtonClicked;
        }

        base.OnApplyTemplate();

        this.thumbsUpButton = this.GetTemplateChild("PART_ThumbsUp") as RadToggleButton;
        this.thumbsDownButton = this.GetTemplateChild("PART_ThumbsDown") as RadToggleButton;
        this.copyButton = this.GetTemplateChild("PART_CopyButton") as RadTemplatedButton;

        if (this.thumbsUpButton != null)
        {
            this.thumbsUpButton.IsToggledChanged += this.OnThumbsUpIsToggledChanged;
        }

        if (this.thumbsDownButton != null)
        {
            this.thumbsDownButton.IsToggledChanged += this.OnThumbsDownIsToggledChanged;
        }

        if (this.copyButton != null)
        {
            this.copyButton.Clicked += this.OnCopyButtonClicked;
        }
    }

    private void OnThumbsUpIsToggledChanged(object sender, ValueChangedEventArgs<bool?> e)
        => this.UpdateRatingAndIsToggled((RadToggleButton)sender);

    private void OnThumbsDownIsToggledChanged(object sender, ValueChangedEventArgs<bool?> e)
        => this.UpdateRatingAndIsToggled((RadToggleButton)sender);

    private void UpdateRatingAndIsToggled(RadToggleButton toggleButton)
    {
        if (this.isInternalChange)
        {
            return;
        }

        this.isInternalChange = true;

        bool isThumbsUp = toggleButton == this.thumbsUpButton;
        RadToggleButton otherButton = isThumbsUp ? this.thumbsDownButton : this.thumbsUpButton;

        if (toggleButton.IsToggled == true && otherButton?.IsToggled == true)
        {
            otherButton.IsToggled = false;
        }

        this.isInternalChange = false;
    }

    private async void OnCopyButtonClicked(object sender, EventArgs e)
    {
        var messageText = this.MessageText;
        if (messageText == null)
        {
            return;
        }

        await Clipboard.SetTextAsync(messageText);
    }
}