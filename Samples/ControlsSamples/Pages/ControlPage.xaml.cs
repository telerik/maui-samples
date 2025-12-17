using Microsoft.Maui.Controls;

namespace QSF.Pages;

public partial class ControlPage : ContentPage
{
    public ControlPage()
    {
#if ANDROID && NET10_0_OR_GREATER
        this.SafeAreaEdges = new Microsoft.Maui.SafeAreaEdges(Microsoft.Maui.SafeAreaRegions.Container);
#endif
        this.InitializeComponent();
    }
}
