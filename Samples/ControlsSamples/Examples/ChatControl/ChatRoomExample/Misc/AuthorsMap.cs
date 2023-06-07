using Microsoft.Maui.Controls;
using System.Collections.Generic;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.ChatControl.ChatRoomExample;

public class AuthorsMap : Dictionary<ChatroomParticipant, Author>
{
    public Author GetOrCreateAuthor(ChatroomParticipant participant)
    {
        Author author;
        if (!this.TryGetValue(participant, out author))
        {
            author = Convert(participant);
            this[participant] = author;
        }

        return author;
    }

    private static Author Convert(ChatroomParticipant participant)
    {
        Author author = new Author();
        author.Data = participant;
        author.SetBinding(Author.NameProperty, new Binding { Path = nameof(participant.ShortName), Source = participant, });
        author.SetBinding(Author.AvatarProperty, new Binding { Path = nameof(participant.Avatar), Source = participant, });
        return author;
    }
}