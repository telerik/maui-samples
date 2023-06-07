using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;

namespace QSF.Examples.SlideViewControl.FirstLookExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstLookView : RadContentView
{
    public FirstLookView()
    {
        InitializeComponent();

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.slideView.MaximumWidthRequest = 650;
        }
    }
}