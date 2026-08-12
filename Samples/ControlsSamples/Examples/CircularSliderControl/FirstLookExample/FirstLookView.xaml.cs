using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;

namespace QSF.Examples.CircularSliderControl.FirstLookExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstLookView : RadContentView
{
    public FirstLookView()
    {
        InitializeComponent();
        this.ApplySwitchOffColor();
    }

    private void ApplySwitchOffColor()
    {
#if IOS && NET9_0
        return;
#endif

        var offColorProperty = typeof(Microsoft.Maui.Controls.Switch).GetProperty("OffColor");
        if (offColorProperty == null)
        {
            return;
        }

        var offColor = Microsoft.Maui.Graphics.Color.FromArgb("#4DBABABA");
        offColorProperty.SetValue(this.systemSwitchMobile, offColor);
        offColorProperty.SetValue(this.systemSwitchDesktop, offColor);
    }
}
