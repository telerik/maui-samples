using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Examples.DataPagerControl.FeaturesCategory;

// >> datapager-features-viewmodel
public class ViewModel
{
    public ViewModel()
    {
        Data = CreateData(200);
    }

    public List<int> Data { get; set; }

    internal static List<int> CreateData(int count)
    {
        List<int> source = new List<int>();

        for (int i = 0; i < count; i++)
        {
            source.Add(i);
        }

        return source;
    }
}
// << datapager-features-viewmodel