using Telerik.Maui.Controls;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.SlideViewControl.CommandsCategory.CommandsExample;

public partial class Commands : RadContentView
{
    public Commands()
    {
        InitializeComponent();

        slideView.BindingContext = new ViewModel();
    }
}

public class MyItem
{
    public string Content { get; set; }
}

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