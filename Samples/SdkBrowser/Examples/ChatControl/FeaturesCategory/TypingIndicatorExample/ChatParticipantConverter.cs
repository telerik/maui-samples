using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.TypingIndicatorExample
{
    // >> chat-typingindicator-authorsconverter
    public class ChatParticipantConverter : IAuthorConverter
    {
        public Author ConvertToAuthor(object dataItem, AuthorConverterContext context)
        {
            Participant participant = (Participant)dataItem;
            return new Author()
            {
                Name = participant.ShortName,
                Avatar = participant.Avatar,
                Data = participant
            };
        }
    }
    // << chat-typingindicator-authorsconverter
}