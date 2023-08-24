using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.CustomizationExample;

public partial class Customization : RadContentView
{
    Author bot;
    public Customization()
	{
		InitializeComponent();

        string botAvatar = "samplebot.png";
        this.bot = new Author { Name = "bot", Avatar = botAvatar, };

        this.chat.Items.Add(new TextMessage { Text = "Hello! I am a sophisticated combination of algorithms, AI, machine learning, and large datasets. How may I be of service?", Author = bot, });
        this.chat.Items.Add(new TextMessage { Text = "I wanted to boil an egg in my microwave, but it exploded. Why?", Author = this.chat.Author, });
    }
}