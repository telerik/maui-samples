using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.ItemTemplateSelectorExample
{
    // >> chat-features-itemtemplate-templateselector
    public class CustomChatItemTemplateSelector : ChatItemTemplateSelector
    {
        public DataTemplate ImportantMessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ChatItem chatItem = item as ChatItem;
            var myItem = chatItem?.Data as SimpleChatItem;
            if (myItem != null && myItem.Category == MessageCategory.Important)
            {
                return this.ImportantMessageTemplate;
            }
            return base.OnSelectTemplate(item, container);
        }
    }
    // << chat-features-itemtemplate-templateselector
}

