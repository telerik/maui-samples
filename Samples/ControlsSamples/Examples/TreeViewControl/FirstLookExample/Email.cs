using System;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.FirstLookExample;

public class Email : ViewModelBase
{
    private string sender;
    private string subject;
    private DateTime received;
    private string message;

    [XmlAttribute("Sender")]
    public string Sender
    {
        get => this.sender;
        set => this.UpdateValue(ref this.sender, value);
    }

    [XmlAttribute("Subject")]
    public string Subject
    {
        get => this.subject;
        set => this.UpdateValue(ref this.subject, value);
    }

    [XmlAttribute("Received")]
    public DateTime Received
    {
        get => this.received;
        set => this.UpdateValue(ref this.received, value);
    }

    [XmlAttribute("Message")]
    public string Message
    {
        get => this.message;
        set => this.UpdateValue(ref this.message, value);
    }
}
