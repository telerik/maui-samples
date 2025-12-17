using QSF.Examples.ChatControl.AIIntegrationExample;
using System.Collections.Generic;

namespace QSF.Examples.ChatControl.FirstLookExample;

public class ChatResponseEventArgs : EventArgs
{
    public readonly string Text;
    public readonly IList<AttachmentData> Attachments;

    public ChatResponseEventArgs(string text, IList<AttachmentData> attachments = null)
    {
        this.Text = text;
        this.Attachments = attachments;
    }
}