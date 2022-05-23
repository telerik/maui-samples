using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.DataGridControl.LocalizationCategory;

public class LocalizationViewModel
{
    public LocalizationViewModel()
    {
        var source = new ObservableCollection<City>();
        source.Add(new City("Vratsa", 54150));
        source.Add(new City("Mexico City", 22000000));
        source.Add(new City("Trieste", 204849));
        source.Add(new City("Ottawa", 934243));
        source.Add(new City("Caracas", 1943901));
        source.Add(new City("Amsterdam", 851573));
        source.Add(new City("Pittsburgh", 305704));
        source.Add(new City("Athens", 664046));
        source.Add(new City("Taipei", 2704974));
        source.Add(new City("Marseille", 855393));
        source.Add(new City("Pnom Penh", 1501725));

        this.GridSource = source;
    }

    public ObservableCollection<City> GridSource { get; set; }
    
}