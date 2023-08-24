using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace QSF.Examples.ExpanderControl.FirstLookExample;

public partial class FirstLookView : ContentView
{
    public FirstLookView()
    {
        this.InitializeComponent();

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.expander.MaximumWidthRequest = 640;
            this.expander.HorizontalOptions = LayoutOptions.Start;
        }
    }
}