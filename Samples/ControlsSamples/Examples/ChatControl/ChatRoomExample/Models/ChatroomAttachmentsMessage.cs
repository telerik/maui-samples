using System.Collections.ObjectModel;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public class ChatroomAttachmentsMessage : ChatroomMessage
{
    private ChatroomParticipant sender;
    private string message;
    private ObservableCollection<AttachmentData> attachments;

    public ChatroomAttachmentsMessage()
    {
        this.attachments = new ObservableCollection<AttachmentData>();
    }

    public ChatroomParticipant Sender
    {
        get => this.sender;
        set => this.UpdateValue(ref this.sender, value);
    }

    public string Message
    {
        get => this.message;
        set => this.UpdateValue(ref this.message, value);
    }

    public ObservableCollection<AttachmentData> Attachments
    {
        get => this.attachments;
        set => this.UpdateValue(ref this.attachments, value);
    }

}