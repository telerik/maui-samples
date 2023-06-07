using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.DefaultTemplateSelectorExample;

public partial class DefaultTemplateSelector : RadContentView
{
	public DefaultTemplateSelector()
	{
		InitializeComponent();

        var botAuthor = new Author { Name = "botty" };

        this.chat.Items.Add(new TextMessage() { Text = "Hi.", Author = botAuthor });
        this.chat.Items.Add(new TimeBreak() { Text = "Unread" });
        this.chat.Items.Add(new TextMessage() { Text = "How can I help you?", Author = botAuthor });
    }
}