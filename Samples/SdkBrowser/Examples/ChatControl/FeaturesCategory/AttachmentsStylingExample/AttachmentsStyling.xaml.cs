using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsExample;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsStylingExample;

public partial class AttachmentsStyling : ContentView
{
	public AttachmentsStyling()
	{
		InitializeComponent();

		this.BindingContext = new ChatWithAttachmentsViewModel();
	}
}