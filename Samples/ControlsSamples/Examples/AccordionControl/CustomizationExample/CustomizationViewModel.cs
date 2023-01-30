using System.Collections.ObjectModel;
using QSF.Examples.ListViewControl.FirstLookExample;
using QSF.Examples.ChartControl;
using QSF.ViewModels;

namespace QSF.Examples.AccordionControl.CustomizationExample;

public class CustomizationViewModel : ExampleViewModel
{
    private ObservableCollection<DataItem> seriesData;
    private ObservableCollection<Person> people;

    public CustomizationViewModel()
    {
        this.SeriesData = new ObservableCollection<DataItem>
        {
            new DataItem() { Value = 12.5, Category = "Direct" },
            new DataItem() { Value = 62.5, Category = "Ecommerce" },
            new DataItem() { Value = 25, Category = "Personal" }
        };
        
        this.People = new ObservableCollection<Person>
        {
            new Person() { Name="Kosta Kushlev", Information="KostaKushlev@Company.com" },
            new Person() { Name="Svetlin Nikolaev", Information="SNikolaev@Company.com" },
            new Person() { Name="Diana Koleva", Information="DianaK@Company.com" },
        };
    }

    public ObservableCollection<DataItem> SeriesData
    {
        get => this.seriesData;
        set => this.UpdateValue(ref this.seriesData, value);
    }

    public ObservableCollection<Person> People
    {
        get => this.people;
        set => this.UpdateValue(ref this.people, value);
    }
}
