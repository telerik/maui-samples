using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Examples.ChatControl.GettingStartedCategory
{
    // >> chat-gettingstarted-botservice
    public class RepeatBotService
    {
        private Action<string> onReceiveMessage;
        internal void AttachOnReceiveMessage(Action<string> onMessageReceived)
        {
            this.onReceiveMessage = onMessageReceived;
        }
        internal void SendToBot(string text)
        {
            Task.Delay(500).ContinueWith(t => this.onReceiveMessage?.Invoke(text));
        }
    }
    // << chat-gettingstarted-botservice
}
