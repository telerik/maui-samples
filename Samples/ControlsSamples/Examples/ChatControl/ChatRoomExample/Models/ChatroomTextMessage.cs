namespace QSF.Examples.ChatControl.ChatRoomExample;

public class ChatroomTextMessage : ChatroomMessage
{
    private ChatroomParticipant sender;
    private string message;

    public ChatroomParticipant Sender
    {
        get
        {
            return this.sender;
        }
        set
        {
            this.UpdateValue(ref this.sender, value);
        }
    }

    public string Message
    {
        get
        {
            return this.message;
        }
        set
        {
            this.UpdateValue(ref this.message, value);
        }
    }
}
