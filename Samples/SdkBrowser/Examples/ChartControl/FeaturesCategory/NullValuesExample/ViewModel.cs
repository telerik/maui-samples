using System.Collections.ObjectModel;

namespace SDKBrowser.Examples.ChartControl.FeaturesCategory.NullValuesExample
{
    // >> chart-nullvalues-viewmodel
    public class ViewModel
    {
        public ViewModel()
        {
            this.Data = new ObservableCollection<CategoryItem>()
            {
                new CategoryItem { Category = "A" }, 
                new CategoryItem { Category = "B", Value = 70 },
                new CategoryItem { Category = "C", Value = 80 },
                new CategoryItem { Category = "D", Value = 50 },
                new CategoryItem { Category = "E", Value = 40 },
                new CategoryItem { Category = "F" },
                new CategoryItem { Category = "G" },
                new CategoryItem { Category = "H", Value = 30 },
                new CategoryItem { Category = "I", Value = 60 },
                new CategoryItem { Category = "J", Value = 80 },
                new CategoryItem { Category = "K", Value = 85 },
                new CategoryItem { Category = "L", Value = 90 }
            };
        }

        public ObservableCollection<CategoryItem> Data { get; private set; }
    }

    public class CategoryItem
    {
        public string Category { get; set; }
        public double? Value { get; set; }
    }
    // << chart-nullvalues-viewmodel
}
