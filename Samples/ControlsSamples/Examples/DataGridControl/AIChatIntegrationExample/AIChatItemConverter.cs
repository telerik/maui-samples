using Microsoft.Maui.Controls;
using QSF.Examples.ChatControl;
using System.Collections.Generic;
using Telerik.AppUtils.Services;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.DataGridControl.AIChatIntegrationExample;

public class AIChatItemConverter : IChatItemConverter
{
    private readonly Dictionary<object, Author> authorDict = new();

    public ChatItem ConvertToChatItem(object dataItem, ChatItemConverterContext context)
    {
        if (dataItem is ChatItem existing)
        {
            return existing;
        }

        if (dataItem is not MessageItem message)
        {
            return null;
        }

        TextMessage chatMessage = new TextMessage();
        chatMessage.Text = message.Text;
        chatMessage.Data = DependencyService.Get<ITestingService>().DateTimeNow(new DateTime(2026, 02, 03, 0, 0, 0, DateTimeKind.Utc));
        chatMessage.Author = this.GetOrCreateAuthor(message.Author, context);

        return chatMessage;
    }

    public object ConvertToDataItem(object message, ChatItemConverterContext context) => null;

    private Author GetOrCreateAuthor(object authorData, ChatItemConverterContext context)
    {
        if (!this.authorDict.TryGetValue(authorData, out var author))
        {
            var vm = (AIChatIntegrationViewModel)context.Chat.BindingContext;
            if (Equals(vm.Me, authorData))
            {
                author = context.Chat.Author;
            }
            else
            {
                author = new Author
                {
                    Data = authorData,
                    Name = authorData.ToString()
                };
            }

            this.authorDict[authorData] = author;
        }

        return author;
    }
}