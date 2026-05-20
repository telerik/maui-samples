using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.SmartPasteButtonControl.ExternalEditorCategory.ExternalEditorExample;

namespace SDKBrowserMaui.Examples.SmartPasteButtonControl.StylingCategory.StylingExample;

public partial class Styling : ContentView
{
    public Styling()
    {
        this.InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}
