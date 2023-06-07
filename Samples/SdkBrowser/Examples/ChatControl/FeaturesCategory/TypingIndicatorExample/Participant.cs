using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.TypingIndicatorExample
{
    // >> chat-typingindicator-participant
    public class Participant
    {
        public int ID { get; set; }
        public string ShortName { get; set; }
        public string Avatar { get; set; }
        public bool IsAdmin { get; set; }
    }
    // << chat-typingindicator-participant
}
