using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.ChatControl.FirstLookExample;

public class FirstLookItemConverter : IChatItemConverter
{
    private readonly Dictionary<object, Author> authorCache = new Dictionary<object, Author>();

    public ChatItem ConvertToChatItem(object dataItem, ChatItemConverterContext context)
    {
        MessageItem message = (MessageItem)dataItem;
        ChatMessage chatMessage;

        if (message is AttachmentsItem attachmentsItem)
        {
            List<ChatAttachment> chatAttachments = attachmentsItem.Attachments.Select(CreateChatAttachment).ToList();
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
        // We add a new message into the messages in the view model when the SendMessageCommand is executed, so no need to create a new data item here.
        return null;
    }

    private Author GetOrCreateAuthor(object authorData, ChatItemConverterContext context)
    {
        if (authorData == null)
        {
            return null;
        }

        if (!this.authorCache.TryGetValue(authorData, out var author))
        {
            var vm = context.Chat.BindingContext as FirstLookViewModel;

            if (vm != null && Equals(vm.Me, authorData))
            {
                author = context.Chat.Author;
            }
            else
            {
                author = new Author { Data = authorData, Name = authorData.ToString(), Avatar = $"{authorData}.png", };
            }

            this.authorCache[authorData] = author;
        }

        return author;
    }

    private static ChatAttachment CreateChatAttachment(AttachmentData attachment)
    {
        ChatAttachment chatAttachment = new ChatAttachment { Data = attachment, FileName = attachment.Name, FileSize = attachment.Size, };
        chatAttachment.GetFileStream = () => GetFileStreamTask(attachment);
        return chatAttachment;
    }

    private static async Task<Stream> GetFileStreamTask(AttachmentData attachment)
    {
        if (attachment.Guid != System.Guid.Empty)
        {
            return await DataFileService.OpenFileStream(attachment.Guid);
        }
        else
        {
            return null;
        }
    }
}