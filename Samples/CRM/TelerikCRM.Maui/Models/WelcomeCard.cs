using Telerik.Maui.Controls;

namespace TelerikCRM.Maui.Models;

public class WelcomeCard : NotifyPropertyChangedBase
{
    private string title;
    private string subtitle;
    private bool isFinalItem;
    private ImageSource iconSource;

    public string Title
    {
        get => this.title;
        set => this.UpdateValue(ref this.title, value);
    }

    public string Subtitle
    {
        get => this.subtitle;
        set => this.UpdateValue(ref this.subtitle, value);
    }

    public ImageSource IconSource
    {
        get => this.iconSource;
        set => this.UpdateValue(ref this.iconSource, value);
    }

    public bool IsFinalItem
    {
        get => this.isFinalItem;
        set => this.UpdateValue(ref this.isFinalItem, value);
    }
}