using Telerik.Maui.Controls;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public partial class ChatRoomView : RadContentView
{
    public ChatRoomView()
    {
        InitializeComponent();
    }

    private void chat_Loaded(object sender, System.EventArgs e)
    {
        ((ChatroomViewModel)this.BindingContext).StartService();
    }

    private void chat_Unloaded(object sender, System.EventArgs e)
    {
        ((ChatroomViewModel)this.BindingContext).StopService();
    }
}