using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SlideViewControl.FeaturesCategory.InteractionModeExample;

public partial class InteractionMode : RadContentView
{
    public InteractionMode()
    {
        InitializeComponent();
        
        slideView.BindingContext = new ViewModel();
    }
}