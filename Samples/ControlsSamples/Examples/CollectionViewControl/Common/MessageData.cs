using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;

namespace QSF.Examples.CollectionViewControl.Common;

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

    public string BackgroundColorKey { get; set; }

    public string TextColorKey { get; set; }

    public string StatusColorKey { get; set; }

    public string SenderAbbreviation
    {
        get => this.Sender.Substring(0, 2);
    }

    public string GroupSender
    {
        get => this.Sender.Substring(0, 1);
    }
}
