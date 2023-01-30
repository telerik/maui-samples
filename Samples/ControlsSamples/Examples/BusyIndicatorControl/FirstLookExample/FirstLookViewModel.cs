using QSF.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace QSF.Examples.BusyIndicatorControl.FirstLookExample
{
    public class FirstLookViewModel : GalleryExampleViewModelBase
    {
        protected override IEnumerable<GalleryItemViewModelBase> GetGalleryItems()
        {
            IEnumerable<AnimationType> enumValues = Enum.GetValues(typeof(AnimationType)).Cast<AnimationType>();
            IEnumerable<AnimationType> animationTypesExceptCustom = enumValues.Except(new AnimationType[] { AnimationType.Custom });

            int i = 1;
            foreach (var animationType in animationTypesExceptCustom)
            {
                var activeIcon = string.Format("busyindicator_theming_gallery{0}_header_active.png", i.ToString());
                var inactiveIcon = string.Format("busyindicator_theming_gallery{0}_header_inactive.png", i.ToString());
                i++;

                yield return new FirstLookItemViewModel(activeIcon, inactiveIcon, "busyIndicator", animationType);
            }
        }
    }
}
