using System.Collections.ObjectModel;
using QSF.ViewModels;

namespace QSF.Examples.SlideViewControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    public ObservableCollection<string> Images { get; private set; }
    public FirstLookViewModel()
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
}