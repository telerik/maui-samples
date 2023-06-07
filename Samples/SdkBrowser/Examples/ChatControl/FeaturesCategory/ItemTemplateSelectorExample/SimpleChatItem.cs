using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.ItemTemplateSelectorExample
{
    // >> chat-features-itemtemplate-chatitem
    public enum MessageCategory
    {
        Important,
        Normal
    }
    public class SimpleChatItem
    {
        public Author Author { get; set; }
        public string Text { get; set; }
        public MessageCategory Category { get; set; }
    }
    // << chat-features-itemtemplate-chatitem
}
