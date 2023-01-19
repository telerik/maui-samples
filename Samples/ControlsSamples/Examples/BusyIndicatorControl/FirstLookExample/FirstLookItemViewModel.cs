using QSF.ViewModels;
using Telerik.Maui.Controls;

namespace QSF.Examples.BusyIndicatorControl.FirstLookExample
{
    public class FirstLookItemViewModel : GalleryItemViewModelBase
    {
        public FirstLookItemViewModel(string icon, string inactiveIcon, string dataTemplateResourceKey, AnimationType animationType)
            : base(icon, inactiveIcon, dataTemplateResourceKey)
        {
            this.AnimationType = animationType;
        }

        public AnimationType AnimationType { get; }
    }
}
