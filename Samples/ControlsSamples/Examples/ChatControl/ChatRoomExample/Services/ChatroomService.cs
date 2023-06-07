using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public class ChatroomService
{
    public const string ParticipantStartedTypingMessage = "ParticipantStartedTypingMessage";
    public const string ParticipantFinishedTypingMessage = "ParticipantFinishedTypingMessage";
    public const string ParticipantTextedMessage = "ParticipantTextedMessage";

    private static readonly List<Tuple<string, string>> participantInfos;

    private Random random;
    private List<ChatroomParticipant> participants;
    private List<ChatroomParticipant> currentlyTyping;
    private int runingSimulations;
    private bool isRunning;

    static ChatroomService()
    {
        participantInfos = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("Maria", "person_1.png"),
            new Tuple<string, string>("Clark", "person_2.png"),
            new Tuple<string, string>("Robert", "person_3.png"),
            new Tuple<string, string>("Sara", "person_4.png"),
            new Tuple<string, string>("Philip", "person_5.png"),
            new Tuple<string, string>("Frankie", "person_6.png"),
            new Tuple<string, string>("Naomi", "person_7.png"),
            new Tuple<string, string>("Zoe", "person_8.png"),
        };
    }

    public ChatroomService()
    {
        this.random = new Random();
        this.InitParticipants();
        this.currentlyTyping = new List<ChatroomParticipant>();
    }

    public event EventHandler<ChatroomSignalEventArgs> Signal;

    public bool IsRunning
    {
        get
        {
            return this.isRunning;
        }
        set
        {
            if (this.isRunning != value)
            {
                this.isRunning = value;

                if (value)
                {
                    this.RunConversation();
                }
            }
        }
    }

    public ChatroomParticipant GetRandomParticipant()
    {
        int index = this.random.Next(0, this.participants.Count);
        return this.participants[index];
    }

    internal ChatroomParticipant GetParticipant(int id)
    {
        return this.participants.First(p => p.Id == id);
    }

    private void InitParticipants()
    {
        this.participants = new List<ChatroomParticipant>();
        int count = this.random.Next(3, 6);
        int r = this.random.Next(0, 10);

        for (int i = 0; i < count; i++)
        {
            Tuple<string, string> info = this.GetParticipantInfo(r + i);
            ChatroomParticipant participant = new ChatroomParticipant { ShortName = info.Item1, Avatar = info.Item2 };
            this.participants.Add(participant);
        }
    }

    private Tuple<string, string> GetParticipantInfo(int index)
    {
        return participantInfos[index % participantInfos.Count];
    }

    private void RunConversation()
    {
        Task.Factory.StartNew(() => 
        {
            if (!this.CanStartSimulation())
            {
                return;
            }

            Task.Delay(this.random.Next(1000, 3000)).Wait();
            ChatroomParticipant participant = GetRandomParticipant();

            if (!this.SimulateStartTyping(participant))
            {
                return;
            }

            this.SimulateTyping();

            this.SimulateEndTyping(participant);

            this.SimulateTexting(participant);

            this.EndSimulation();

            this.RunConversation();
        });
    }

    private bool CanStartSimulation()
    {
        if (!this.isRunning)
        {
            return false;
        }

        if (Interlocked.Increment(ref this.runingSimulations) > 5)
        {
            Interlocked.Decrement(ref this.runingSimulations);
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool SimulateStartTyping(ChatroomParticipant participant)
    {
        lock (this.currentlyTyping)
        {
            if (this.currentlyTyping.Contains(participant))
            {
                return false;
            }

            this.currentlyTyping.Add(participant);
        }

        this.SendStartTypingSignal(participant);

        if (this.random.Next(0, 3) == 0)
        {
            this.RunConversation();
        }

        return true;
    }

    private void SimulateTyping()
    {
        Task.Delay(this.random.Next(1000, 6000)).Wait();
    }

    private void SimulateEndTyping(ChatroomParticipant participant)
    {
        lock (this.currentlyTyping)
        {
            this.currentlyTyping.Remove(participant);
        }

        this.SendFinishedTypingSignal(participant);
    }

    private void SimulateTexting(ChatroomParticipant participant)
    {
        this.SendTextedSignal(participant);
    }

    private void EndSimulation()
    {
        Interlocked.Decrement(ref this.runingSimulations);
    }

    private void SendStartTypingSignal(ChatroomParticipant participant)
    {
        string signal = string.Format("{0} {1}", ParticipantStartedTypingMessage, participant.Id);
        this.SendSignal(signal);
    }

    private void SendFinishedTypingSignal(ChatroomParticipant participant)
    {
        string signal = string.Format("{0} {1}", ParticipantFinishedTypingMessage, participant.Id);
        this.SendSignal(signal);
    }

    private void SendTextedSignal(ChatroomParticipant participant)
    {
        string signal = string.Format("{0} {1} {2}", ParticipantTextedMessage, participant.Id, ParticipantLines.GetRandomLine());
        this.SendSignal(signal);
    }

    private void SendSignal(string signal)
    {
        this.Signal?.Invoke(this, new ChatroomSignalEventArgs(signal));
    }
}
