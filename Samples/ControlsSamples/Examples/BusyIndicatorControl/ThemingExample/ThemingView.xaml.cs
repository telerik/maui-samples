using QSF.Views;
using Microsoft.Maui.Controls.Xaml;
using QSF.Examples.BusyIndicatorControl.FirstLookExample;

namespace QSF.Examples.BusyIndicatorControl.ThemingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ThemingView : GalleryExampleViewBase
{
    public ThemingView()
    {
        InitializeComponent();
        this.BindingContext = new FirstLookViewModel();
    }
}