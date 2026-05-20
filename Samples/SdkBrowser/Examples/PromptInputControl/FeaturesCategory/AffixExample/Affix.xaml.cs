using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.PromptInputControl.FeaturesCategory.AffixExample;

public partial class Affix : ContentView
{
    public Affix()
    {
        this.InitializeComponent();
        this.BindingContext = new AffixViewModel();
    }
}