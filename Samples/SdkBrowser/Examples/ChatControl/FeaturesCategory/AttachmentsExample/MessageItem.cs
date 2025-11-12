using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsExample;

// >> chat-message-item
public class MessageItem : NotifyPropertyChangedBase
{
	private object author;
	private string text;

	public object Author
	{
		get => this.author;
		set => this.UpdateValue(ref this.author, value);
	}

	public string Text
	{
		get => this.text;
		set => this.UpdateValue(ref this.text, value);
	}
}

// << chat-message-item