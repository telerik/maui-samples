using System.Threading;
using Telerik.Maui.Controls;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public class ChatroomParticipant : NotifyPropertyChangedBase
{
    private static int count;

    private int id;
    private string shortName;
    private string avatar;

    public ChatroomParticipant()
    {
        this.id = Interlocked.Increment(ref count);
    }

    public int Id
    {
        get
        {
            return this.id;
        }
    }

    public string ShortName
    {
        get
        {
            return this.shortName;
        }
        set
        {
            this.UpdateValue(ref this.shortName, value);
        }
    }

    public string Avatar
    {
        get
        {
            return this.avatar;
        }
        set
        {
            this.UpdateValue(ref this.avatar, value);
        }
    }
}
