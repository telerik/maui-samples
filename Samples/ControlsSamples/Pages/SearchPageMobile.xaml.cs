using Microsoft.Maui.Controls;

namespace QSF.Pages;

public partial class SearchPageMobile : ContentPage
{
    public SearchPageMobile()
    {
#if ANDROID && NET10_0_OR_GREATER
        this.SafeAreaEdges = new Microsoft.Maui.SafeAreaEdges(Microsoft.Maui.SafeAreaRegions.Container);
#endif
        this.InitializeComponent();
    }
}
