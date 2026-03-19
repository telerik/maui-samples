using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsExample;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.ButtonsVisibilityExample;

public partial class ButtonsVisibility : ContentView
{
    public ButtonsVisibility()
    {
        InitializeComponent();

        this.BindingContext = new ChatWithAttachmentsViewModel();
    }
}