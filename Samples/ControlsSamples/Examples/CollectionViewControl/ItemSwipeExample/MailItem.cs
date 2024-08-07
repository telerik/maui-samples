using Telerik.Maui.Controls;

namespace QSF.Examples.CollectionViewControl.ItemSwipeExample;

public class MailItem : NotifyPropertyChangedBase
{
    private bool isFavourite;

    public string Sender { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Date { get; set; }
    public bool IsFavourite
    {
        get => this.isFavourite;
        set => UpdateValue(ref this.isFavourite, value);
    }
    public string Image { get; set; }
}