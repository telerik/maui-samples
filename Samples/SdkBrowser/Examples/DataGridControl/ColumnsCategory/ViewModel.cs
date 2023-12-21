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
            new Club("UK Liverpool ", new DateTime(1892, 1, 1), 54074, "England", "Liverpool", "Premier League", null),
            new Club("Manchester Utd.", new DateTime(1878, 1, 1), 74310, "England", "Manchester", "Premier League", 594.3) { IsChampion = true },
            new Club("Chelsea", new DateTime(1905, 1, 1), 42055, "England", "London","UEFA Champions League", 481.3),
            new Club("Barcelona", new DateTime(1899, 1, 1), 99354, "Spain", "Barcelona", "La Liga", 540.5)
        };
    }
}
// << datagrid-column-view-model