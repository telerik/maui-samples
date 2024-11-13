using System.Linq;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace QSF.Examples.DataPagerControl.ThemingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ThemingView : RadContentView
{
    public ThemingView()
    {
        InitializeComponent();
        this.dataPager.Source = Enumerable.Range(1, 100).ToList();
    }
}