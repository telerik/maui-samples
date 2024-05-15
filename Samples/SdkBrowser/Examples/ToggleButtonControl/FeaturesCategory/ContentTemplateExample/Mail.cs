using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ToggleButtonControl.FeaturesCategory.ContentTemplateExample;

public class Mail : NotifyPropertyChangedBase
{
    bool isImportant;

    public string Sender { get; set; }

    public string Subject { get; set; }

    public bool IsImportant
    {
        get { return isImportant; }
        set { this.UpdateValue(ref this.isImportant, value); }
    }
}
