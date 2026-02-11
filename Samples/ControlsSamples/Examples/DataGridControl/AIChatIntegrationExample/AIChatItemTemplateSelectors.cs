using Microsoft.Maui.Controls;
using QSF.Examples.ChatControl;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.DataGridControl.AIChatIntegrationExample;

public class AIChatItemTemplateSelectors : DataTemplateSelector
{
    public DataTemplate AIItemTemplate { get; set; }
    public DataTemplate UserItemTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        TextMessage textMessage = item as TextMessage;
        if (textMessage != null)
        {
            if (textMessage.Author.Name == ConversationConstants.aiBotAuthorId)
            {
                return this.AIItemTemplate;
            }

            return this.UserItemTemplate;
        }

        return null;
    }
}