using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.FeaturesCategory.DropDownConfigurationExample;

public partial class DropDownConfiguration : RadContentView
{
	public DropDownConfiguration()
	{
		InitializeComponent();
        this.BindingContext = new ViewModel();
    }
}