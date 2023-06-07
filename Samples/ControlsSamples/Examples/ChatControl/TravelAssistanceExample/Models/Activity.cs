using System;

namespace QSF.Examples.ChatControl.TravelAssistanceExample.Models;

public class Activity
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string AttachmentLayout { get; set; }
    public ConversationAccount Conversation { get; set; }
    public DateTime Timestamp { get; set; }
    public Sender From { get; set; }
    public string Text { get; set; }
    public object ChannelData { get; set; }
    public Attachment[] Attachments { get; set; }
    public SuggestedActions SuggestedActions { get; set; }
}