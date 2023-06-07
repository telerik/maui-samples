using Microsoft.Maui.Controls;
using System;
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

        throw new NotSupportedException("Unsupported item type was provided.");
    }

    public object ConvertToDataItem(object message, ChatItemConverterContext context)
    {
        return new ChatroomTextMessage { Message = message?.ToString(), Sender = (ChatroomParticipant)context.Chat.Author.Data, };
    }

    private ChatItem CreateTextMessage(ChatroomTextMessage message, RadChat chat)
    {
        TextMessage textMessage = new TextMessage();
        textMessage.Data = message;
        textMessage.Author = this.AuthorsMap.GetOrCreateAuthor(message.Sender);
        textMessage.SetBinding(TextMessage.TextProperty, new Binding { Path = nameof(message.Message), Source = message, Converter = this.emojiConverter });

        return textMessage;
    }

    private ChatItem CreateTimeBreak(ChatroomTimebreak timebreak)
    {
        TimeBreak telerikTimeBreak = new TimeBreak();
        telerikTimeBreak.Data = timebreak;
        telerikTimeBreak.SetBinding(TimeBreak.TextProperty, new Binding { Path = nameof(timebreak.Text), Source = timebreak, });

        return telerikTimeBreak;
    }
}
