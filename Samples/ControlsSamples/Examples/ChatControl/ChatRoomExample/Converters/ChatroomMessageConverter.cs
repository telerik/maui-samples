using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public class ChatroomMessageConverter : IChatItemConverter
{
    private EmojiConverter emojiConverter = new EmojiConverter();

    public AuthorsMap AuthorsMap
    {
        get;
        set;
    }

    public ChatItem ConvertToChatItem(object dataItem, ChatItemConverterContext context)
    {
        ChatroomTextMessage message = dataItem as ChatroomTextMessage;
        if (message != null)
        {
            return this.CreateTextMessage(message, context.Chat);
        }

        ChatroomTimebreak timebreak = dataItem as ChatroomTimebreak;
        if (timebreak != null)
        {
            return this.CreateTimeBreak(timebreak);
        }

        ChatroomAttachmentsMessage attachmentsMessage = dataItem as ChatroomAttachmentsMessage;
        if (attachmentsMessage != null)
        {
            return this.CreateAttachmentMessage(attachmentsMessage, context.Chat);
        }

        throw new NotSupportedException("Unsupported item type was provided.");
    }

    public object ConvertToDataItem(object message, ChatItemConverterContext context)
    {
        var sender = (ChatroomParticipant)context.Chat.Author.Data;

        bool hasAttachedFiles = context.Chat.AttachedFiles != null && context.Chat.AttachedFiles.Count > 0;

        if (hasAttachedFiles)
        {
            ChatroomAttachmentsMessage attachmentsMessage = new ChatroomAttachmentsMessage();

            attachmentsMessage.Sender = sender;
            attachmentsMessage.Message = message.ToString();
            attachmentsMessage.Attachments = CreateAttachmentDataCollection(context.Chat.AttachedFiles);

            context.Chat.AttachedFiles.Clear();

            return attachmentsMessage;
        }

        return new ChatroomTextMessage
        {
            Sender = sender,
            Message = message.ToString()
        };
    }

    private static ObservableCollection<AttachmentData> CreateAttachmentDataCollection(ICollection<ChatAttachedFile> attachedFiles) =>
        new ObservableCollection<AttachmentData>(attachedFiles.Select(f => new AttachmentData { Name = f.FileName, Size = f.FileSize }));

    private static ChatAttachment ConvertToChatAttachment(AttachmentData attachment) =>
        new ChatAttachment { Data = attachment, FileName = attachment.Name, FileSize = attachment.Size };

    private ChatItem CreateTextMessage(ChatroomTextMessage message, RadChat chat)
    {
        TextMessage textMessage = new TextMessage();
        textMessage.Data = message;
        textMessage.Author = this.AuthorsMap.GetOrCreateAuthor(message.Sender);
        textMessage.SetBinding(TextMessage.TextProperty, new Binding { Path = nameof(message.Message), Source = message, Converter = this.emojiConverter });

        return textMessage;
    }

    private ChatAttachmentsMessage CreateAttachmentMessage(ChatroomAttachmentsMessage message, RadChat chat)
    {
        IEnumerable<ChatAttachment> chatAttachments = message.Attachments
            .Select(ConvertToChatAttachment);

        ChatAttachmentsMessage attachmentsMessage = new ChatAttachmentsMessage();

        attachmentsMessage.Attachments = chatAttachments;
        attachmentsMessage.Data = message;
        attachmentsMessage.Author = this.AuthorsMap.GetOrCreateAuthor(message.Sender);

        attachmentsMessage.SetBinding(ChatAttachmentsMessage.TextProperty,
            new Binding { Path = nameof(message.Message), Source = message, Converter = this.emojiConverter });

        return attachmentsMessage;
    }

    private ChatItem CreateTimeBreak(ChatroomTimebreak timebreak)
    {
        TimeBreak telerikTimeBreak = new TimeBreak();
        telerikTimeBreak.Data = timebreak;
        telerikTimeBreak.SetBinding(TimeBreak.TextProperty, new Binding { Path = nameof(timebreak.Text), Source = timebreak, });

        return telerikTimeBreak;
    }
}
