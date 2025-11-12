using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsExample;

public partial class Attachments : ContentView
{
	public Attachments()
	{
		InitializeComponent();

		this.BindingContext = new ChatWithAttachmentsViewModel();
	}
}