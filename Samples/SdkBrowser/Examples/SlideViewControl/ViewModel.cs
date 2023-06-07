using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.SlideViewControl
{
    // >> slideview-events-viewmodel
    public class ViewModel
    {
        public ObservableCollection<MyItem> Views { get; set; }

        public ViewModel()
        {
            this.Views = new ObservableCollection<MyItem>()
            {
                new MyItem() { Content = "View 1" },
                new MyItem() { Content = "View 2" },
                new MyItem() { Content = "View 3" },
            };
        }
    }
    // << slideview-events-viewmodel
}