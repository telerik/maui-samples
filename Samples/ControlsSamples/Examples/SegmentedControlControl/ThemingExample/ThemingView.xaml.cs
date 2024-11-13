using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using QSF.Examples.SegmentedControlControl.FirstLookExample;

namespace QSF.Examples.SegmentedControlControl.ThemingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ThemingView : RadContentView
{
    public ThemingView()
    {
        InitializeComponent();

        this.BindingContext = new FirstLookViewModel();
    }
}