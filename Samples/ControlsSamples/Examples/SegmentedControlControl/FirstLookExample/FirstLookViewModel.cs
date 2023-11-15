using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.Common;
using QSF.Converters;
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

            this.SmallImages = new ObservableCollection<FontImageSource>
            {
                CreateFontImage(TelerikControlsIcons.GetIcon("BusyIndicator")),
                CreateFontImage(TelerikControlsIcons.GetIcon("Path")),
                CreateFontImage(TelerikControlsIcons.GetIcon("Rating"))
            };
        }

        public ObservableCollection<string> Categories { get; private set; }
        public ObservableCollection<FontImageSource> SmallImages { get; private set; }

        private static FontImageSource CreateFontImage(string glyph)
        {
            return new FontImageSource
            {
                Glyph = glyph,
                FontFamily = "TelerikControlsIcons",
                Size = 16,
                Color = App.Current.Resources["AccentColor8"] as Color
            };
        }
    }
}
