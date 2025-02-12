using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.DataPagerControl.IntegrationCategory;

// >> datapager-viewmodel
public class ViewModel
{
    public ViewModel()
    {
        var data = new ObservableCollection<Data>();
        for (int i = 0; i < 200; i++)
        {
            data.Add(new Data { Id = i, Number = i * 10 / 2, Information = "Information " + i });
        }

        MyData = data;
    }

    public ObservableCollection<Data> MyData { get; set; }
}
// << datapager-viewmodel