using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace QSF.Examples.SchedulerControl.CustomizationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SharedSchedulingResources : ResourceDictionary
{
    public SharedSchedulingResources()
    {
        InitializeComponent();
    }
}