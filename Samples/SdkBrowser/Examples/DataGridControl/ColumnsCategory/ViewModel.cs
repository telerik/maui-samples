using System;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory;

// >> datagrid-column-view-model
public class ViewModel
{
    private ObservableCollection<Club> clubs;

    public ObservableCollection<Club> Clubs => clubs ?? (clubs = CreateClubs());

    private ObservableCollection<Club> CreateClubs()
    {
        return new ObservableCollection<Club>
        {
            new Club("UK Liverpool ", new DateTime(1892, 1, 1), new TimeSpan(3, 28, 33), 45362, "England"),
            new Club("Manchester Utd.", new DateTime(1878, 1, 1), new TimeSpan(2, 56, 44), 76212, "England") { IsChampion = true },
            new Club("Chelsea", new DateTime(1905, 1, 1), new TimeSpan(6, 19, 59), 42055, "England"),
            new Club("Barcelona", new DateTime(1899, 1, 1), new TimeSpan(12, 25, 31), 99354, "Spain")
        };
    }
}

// << datagrid-column-view-model
