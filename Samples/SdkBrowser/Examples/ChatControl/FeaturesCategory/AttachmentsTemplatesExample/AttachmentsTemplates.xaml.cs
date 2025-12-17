using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsExample;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsTemplatesExample;

public partial class AttachmentsTemplates : ContentView
{
	public AttachmentsTemplates()
	{
		InitializeComponent();

		this.BindingContext = new ChatWithAttachmentsViewModel();
	}
}