using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.SegmentedControlControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        public FirstLookViewModel()
        {
            this.Categories = new ObservableCollection<string>
            {
                "Dinner",
                "Drinks",
                "Desserts"
            };

            this.SmallImages = new ObservableCollection<ImageSource>
            {
                CreateImage("busyindicator.png"),
                CreateImage("path.png"),
                CreateImage("rating.png")
            };
        }

        public ObservableCollection<string> Categories { get; private set; }
        public ObservableCollection<ImageSource> SmallImages { get; private set; }

        private static ImageSource CreateImage(string name)
        {
            return ImageSource.FromFile(name);
        }
    }
}
