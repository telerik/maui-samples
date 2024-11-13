using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace QSF.Examples.DataPagerControl.CustomizationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomizationView : ContentView
{
    public CustomizationView()
    {
        InitializeComponent();
        this.dataPager.Source = Enumerable.Range(1, 100).ToList();
    }
}