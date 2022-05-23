using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace QSF.Examples.SegmentedControlControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : RadContentView
    {
        public FirstLookView()
        {
            InitializeComponent();
            this.segmented.SetSegmentEnabled(2, false);
        }
    }
}