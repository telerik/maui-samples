using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.GroupingCategory.GroupHeaderStylingExample;

public partial class GroupHeaderStyling : ContentView
{
	public GroupHeaderStyling()
	{
		InitializeComponent();
		
		this.BindingContext = new ViewModel();
	}
}