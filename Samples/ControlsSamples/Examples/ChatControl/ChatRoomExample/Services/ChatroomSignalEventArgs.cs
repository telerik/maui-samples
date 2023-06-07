using System;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public class ChatroomSignalEventArgs : EventArgs
{
    public ChatroomSignalEventArgs(string signal)
    {
        this.Signal = signal;
    }

    public string Signal
    {
        get;
        set;
    }
}
