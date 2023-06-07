using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Maui.Controls;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public class ChatroomViewModel : NotifyPropertyChangedBase
{
    private ChatroomParticipant user;
    private ObservableCollection<ChatroomMessage> items;
    private ObservableCollection<ChatroomParticipant> typingParticipants;
    private ChatroomService chatroomService;
    private ICommand sendMessageCommand;

    public ChatroomViewModel()
    {
        string avatar = "person_9.png";
        this.user = new ChatroomParticipant { ShortName = "User", Avatar = avatar, };
        this.items = new ObservableCollection<ChatroomMessage>();
        this.typingParticipants = new ObservableCollection<ChatroomParticipant>();
        this.chatroomService = new ChatroomService();
        this.AddHistory();
        this.chatroomService.Signal += this.ChatroomService_Signal;
        this.sendMessageCommand = new Command(this.OnSendMessage, this.CanSendMessage);
    }

    public ChatroomParticipant User
    {
        get
        {
            return this.user;
        }
    }

    public ObservableCollection<ChatroomMessage> Items
    {
        get
        {
            return this.items;
        }
    }

    public ObservableCollection<ChatroomParticipant> TypingParticipants
    {
        get
        {
            return this.typingParticipants;
        }
    }

    public ICommand SendMessageCommand
    {
        get
        {
            return this.sendMessageCommand;
        }
    }

    public void StartService()
    {
        this.chatroomService.IsRunning = true;
    }

    public void StopService()
    {
        this.chatroomService.IsRunning = false;
    }

    private void AddHistory()
    {
        for (int i = 0; i < 20; i++)
        {
            ChatroomTextMessage message = new ChatroomTextMessage();
            message.Sender = i % 9 == 0 ? this.user : this.chatroomService.GetRandomParticipant();
            message.Message = ParticipantLines.GetRandomLine();
            this.items.Add(message);
        }

        this.items.Insert(8, new ChatroomTimebreak { Text = "2 hours ago" });
        this.items.Insert(18, new ChatroomTimebreak { Text = "1 hour ago" });
    }

    private void ChatroomService_Signal(object sender, ChatroomSignalEventArgs e)
    {
        Device.BeginInvokeOnMainThread(() => this.OnSignalReceived(e));
    }

    private void OnSignalReceived(ChatroomSignalEventArgs e)
    {
        if (e.Signal.StartsWith(ChatroomService.ParticipantStartedTypingMessage))
        {
            string stringId = e.Signal.Substring(ChatroomService.ParticipantStartedTypingMessage.Length + 1);
            int id = int.Parse(stringId);
            ChatroomParticipant participant = this.chatroomService.GetParticipant(id);
            this.typingParticipants.Add(participant);
        }
        else if (e.Signal.StartsWith(ChatroomService.ParticipantFinishedTypingMessage))
        {
            string stringId = e.Signal.Substring(ChatroomService.ParticipantFinishedTypingMessage.Length + 1);
            int id = int.Parse(stringId);
            ChatroomParticipant participant = this.chatroomService.GetParticipant(id);
            this.typingParticipants.Remove(participant);
        }
        else if (e.Signal.StartsWith(ChatroomService.ParticipantTextedMessage))
        {
            string stringIdAndMessage = e.Signal.Substring(ChatroomService.ParticipantTextedMessage.Length + 1);
            string stringId = stringIdAndMessage.Substring(0, stringIdAndMessage.IndexOf(" "));
            int id = int.Parse(stringId);
            ChatroomParticipant participant = this.chatroomService.GetParticipant(id);
            string message = e.Signal.Substring(ChatroomService.ParticipantTextedMessage.Length + stringId.Length + 2);
            this.Items.Add(new ChatroomTextMessage { Sender = participant, Message = message, });
        }
    }

    private void OnSendMessage(object arg)
    {
        // Send the message to the chat service.
        string message = arg?.ToString();
    }

    private bool CanSendMessage(object arg)
    {
        string message = arg?.ToString();
        return !string.IsNullOrWhiteSpace(message);
    }
}
