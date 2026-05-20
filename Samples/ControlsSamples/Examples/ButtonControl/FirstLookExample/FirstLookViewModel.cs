using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.ButtonControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    public FirstLookViewModel()
    {
        this.WiFiNetworks = new ObservableCollection<MyData>
        {
            new MyData { Name = "Home Wi-Fi", Icon = "\ue85b" },
            new MyData { Name = "Mobile Hotspot", Icon = "\ue85d" },
            new MyData { Name = "Office Wi-Fi", Icon = "\ue8a3" },
            new MyData { Name = "Public Wi-Fi", Icon = "\ue804" },
        };

        this.SaveOperations = new ObservableCollection<MyData>
        {
            new MyData { Name = ".pdf", Icon = "\ue899" },
            new MyData { Name = ".docx", Icon = "\ue898" },
            new MyData { Name = ".xlsx", Icon = "\ue896" },
            new MyData { Name = ".txt", Icon = "\ue853" },
        };
    }

    public ObservableCollection<MyData> WiFiNetworks { get; }

    public ObservableCollection<MyData> SaveOperations { get; }
}
