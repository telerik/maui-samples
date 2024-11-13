using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;

namespace QSF.Examples.SlideViewControl.ThemingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ThemingView : RadContentView
{
    public ThemingView()
    {
        InitializeComponent();

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.slideView.MaximumWidthRequest = 650;
        }
    }
}