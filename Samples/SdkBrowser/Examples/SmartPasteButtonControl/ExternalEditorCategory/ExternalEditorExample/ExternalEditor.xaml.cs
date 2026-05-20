using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SmartPasteButtonControl.ExternalEditorCategory.ExternalEditorExample;

public partial class ExternalEditor : ContentView
{
    public ExternalEditor()
    {
        this.InitializeComponent();

        this.BindingContext = new ViewModel();
    }
}
