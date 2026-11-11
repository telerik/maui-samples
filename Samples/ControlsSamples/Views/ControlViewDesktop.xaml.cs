using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ControlViewDesktop : ContentView
{
    public ControlViewDesktop()
    {
        this.InitializeComponent();

        // Workaround for issue with TypedBindings in .NET 11 RC1 generated XAML.
        this.configurationAreaContainer.SetBinding(
            BindingContextProperty,
            new Binding("Content.BindingContext", source: this.exampleContainer));
    }
}
