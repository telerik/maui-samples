using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.TypingIndicatorExample;

public partial class TypingIndicatorAuthors : RadContentView
{
    public TypingIndicatorAuthors()
    {
        InitializeComponent();

        // >> chat-typingindicator-authors-code
        var author1 = new Author { Name = "Sandra" };
        var author2 = new Author { Name = "John" };
        chat.Author = author2;

        chat.Items.Add(new TextMessage { Author = author2, Text = "Hello there" });
        chat.Items.Add(new TextMessage { Author = author1, Text = "Hi, John" });

        typingIndicator.Authors.Add(author1);
        typingIndicator.Authors.Add(author2);

        Task.Delay(3000).ContinueWith(t =>
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.typingIndicator.Authors.Clear();
                this.chat.Items.Add(new TextMessage { Author = author2, Text = "What are you doing today?" });
                this.chat.Items.Add(new TextMessage { Author = author1, Text = "Do you have any plans for the afternoon?" });
            });
        });
        // << chat-typingindicator-authors-code
    }
}
