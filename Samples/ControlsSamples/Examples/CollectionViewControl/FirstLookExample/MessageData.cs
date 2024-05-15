using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;

namespace QSF.Examples.CollectionViewControl.FirstLookExample;

public class MessageData : NotifyPropertyChangedBase
{
    private string sender;

    public string Sender 
    {
        get => this.sender;
        set => this.UpdateValue(ref this.sender, value);
    }

    public string Message { get; set; }

    public string MessageDelivered { get; set; }

    public Color BackgroundColor { get; set; }

    public Color TextColor { get; set; }

    public Color StatusColor { get; set; }

    public string SenderAbbreviation
    {
        get => this.Sender.Substring(0, 2);
    }

    public string GroupSender
    {
        get => this.Sender.Substring(0, 1);
    }
}
