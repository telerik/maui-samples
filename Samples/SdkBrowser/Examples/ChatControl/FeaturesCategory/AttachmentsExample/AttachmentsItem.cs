using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsExample;

// >> chat-attachments-item
public class AttachmentsItem : MessageItem
{
	private ObservableCollection<AttachmentData> attachments;

	public ObservableCollection<AttachmentData> Attachments
	{
		get => this.attachments;
		set => this.UpdateValue(ref this.attachments, value);
	}
}
// << chat-attachments-item