using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.MVVMSupportExample
{
    // >> chat-features-mvvm-converter
    public class SimpleChatItemConverter : IChatItemConverter
    {
        public ChatItem ConvertToChatItem(object dataItem, ChatItemConverterContext context)
        {
            SimpleChatItem item = (SimpleChatItem)dataItem;
            TextMessage textMessage = new TextMessage()
            {
                Data = dataItem,
                Author = item.Author,
                Text = item.Text
            };
            return textMessage;
        }
        public object ConvertToDataItem(object message, ChatItemConverterContext context)
        {
            ViewModel vm = (ViewModel)context.Chat.BindingContext;
            return new SimpleChatItem { Author = vm.Me, Text = (string)message, };
        }
    }
    // << chat-features-mvvm-converter
}
