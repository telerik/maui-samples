using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public static class ChatroomUtils
{
    public static readonly BindableProperty AuthorsMapProperty = BindableProperty.CreateAttached(
        "AuthorsMap", typeof(AuthorsMap), typeof(ChatroomUtils), null);

    public static readonly BindableProperty AuthorProperty = BindableProperty.CreateAttached(
        "Author", typeof(ChatroomParticipant), typeof(ChatroomUtils), null, propertyChanged: AuthorChanged);

    public static AuthorsMap GetAuthorsMap(BindableObject bindable)
    {
        return (AuthorsMap)bindable.GetValue(AuthorsMapProperty);
    }

    public static void SetAuthorsMap(BindableObject bindable, AuthorsMap value)
    {
        bindable.SetValue(AuthorsMapProperty, value);
    }

    public static ChatroomParticipant GetAuthor(BindableObject bindable)
    {
        return (ChatroomParticipant)bindable.GetValue(AuthorProperty);
    }

    public static void SetAuthor(BindableObject bindable, ChatroomParticipant value)
    {
        bindable.SetValue(AuthorProperty, value);
    }

    private static void AuthorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        RadChat chat = (RadChat)bindable;
        AuthorsMap authors = GetAuthorsMap(chat);
        chat.Author = authors.GetOrCreateAuthor((ChatroomParticipant)newValue);
    }
}
