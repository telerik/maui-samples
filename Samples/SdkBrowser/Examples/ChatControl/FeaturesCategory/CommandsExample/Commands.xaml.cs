using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.CommandsExample;

public partial class Commands : RadContentView
{
	public Commands()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();
    }
}