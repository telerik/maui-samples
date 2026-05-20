using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace QSF.Examples.SegmentedControlControl.FirstLookExample;

public partial class FirstLookView : ContentView
{
    public FirstLookView()
    {
        this.InitializeComponent();

        this.profileTypeSegmented.SetSegmentEnabled(2, false);
    }
}