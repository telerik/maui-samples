namespace QSF.Examples.ChatControl.FirstLookExample;

public class ChatRecipientTypingEventArgs : EventArgs
{
    public readonly bool IsTyping;

    public ChatRecipientTypingEventArgs(bool isTyping)
    {
        this.IsTyping = isTyping;
    }
}