using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.ChatControl.AIIntegrationExample;

public class ItemConverter : IChatItemConverter
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

        ChatMessage chatMessage;
        if (message is AttachmentsItem attachmentsItem)
        {
            var chatAttachments = attachmentsItem.Attachments.Select(CreateChatAttachment).ToList();
            chatMessage = new ChatAttachmentsMessage { Attachments = chatAttachments };
            chatMessage.SetBinding(ChatAttachmentsMessage.TextProperty, new Binding(nameof(MessageItem.Text)) { Source = message });
        }
        else
        {
            chatMessage = new TextMessage();
            chatMessage.SetBinding(TextMessage.TextProperty, new Binding(nameof(MessageItem.Text)) { Source = message });
        }

        chatMessage.Data = message;
        chatMessage.Author = GetOrCreateAuthor(message.Author, context);
        return chatMessage;
    }

    public object ConvertToDataItem(object message, ChatItemConverterContext context)
    {
        // SendMessageCommand manually adds items; no automatic reverse conversion needed.
        return null;
    }

    private static ChatAttachment CreateChatAttachment(AttachmentData attachment)
    {
        var chatAttachment = new ChatAttachment { Data = attachment, FileName = attachment.Name, FileSize = attachment.Size };
        chatAttachment.GetFileStream = () => GetFileStreamTask(attachment);
        return chatAttachment;
    }

    private static async Task<Stream> GetFileStreamTask(AttachmentData attachment)
    {
        if (attachment.Guid != Guid.Empty)
        {
            return await DataFileService.OpenFileStream(attachment.Guid);
        }
        
        return await Task.FromResult<Stream>(null);
    }

    private Author GetOrCreateAuthor(object authorData, ChatItemConverterContext context)
    {
        if (!this.authorDict.TryGetValue(authorData, out var author))
        {
            var vm = (AIIntegrationViewModel)context.Chat.BindingContext;
            if (Equals(vm.Me, authorData))
            {
                author = context.Chat.Author;
            }
            else
            {
                author = new Author
                {
                    Data = authorData,
                    Name = authorData.ToString(),
                    Avatar = $"{authorData}.png"
                };
            }
            this.authorDict[authorData] = author;
        }
        return author;
    }
}