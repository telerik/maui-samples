using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.ChatItemsExample;

public partial class ChatItems : RadContentView
{
	public ChatItems()
	{
		InitializeComponent();

        var bot = new Author() { Name = "botty" };
        // >> chat-chatitems-timebreak
        chat.Items.Add(new TextMessage { Author = bot, Text = "Hello there" });
        chat.Items.Add(new TimeBreak() { Text = "Unread" });
        chat.Items.Add(new TextMessage() { Author = bot, Text = "How are you today?" });
        // << chat-chatitems-timebreak
    }
}