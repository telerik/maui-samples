using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.SlideViewControl.ThemingExample;

public class ThemingViewModel : ExampleViewModel
{
    public ThemingViewModel()
    {
        this.Images = new ObservableCollection<string>
        {
            "salad.png",
            "salad_1.png",
            "salad_2.png",
            "salad_3.png",
            "salad_4.png"
        };
    }

    public ObservableCollection<string> Images { get; private set; }
}
