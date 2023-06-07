using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.TypingIndicatorExample;

public partial class TypingIndicatorItemsSource : RadContentView
{
	public TypingIndicatorItemsSource()
	{
		InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}