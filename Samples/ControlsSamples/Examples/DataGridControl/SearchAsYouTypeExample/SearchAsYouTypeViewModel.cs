using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace QSF.Examples.DataGridControl.SearchAsYouTypeExample;

public class SearchAsYouTypeViewModel : ConfigurationExampleViewModel
{
    private DataGridSearchPanelVisibilityMode searchPanelVisibilityMode = DataGridSearchPanelVisibilityMode.AlwaysVisible;

    private DataGridSearchTrigger searchTrigger;

    public SearchAsYouTypeViewModel()
    {
        this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
    }

    public ObservableCollection<Order> Orders { get; private set; }

    public IEnumerable<DataGridSearchPanelVisibilityMode> SearchPanelVisibilityModes { get; } = Enum.GetValues(typeof(DataGridSearchPanelVisibilityMode)).Cast<DataGridSearchPanelVisibilityMode>();

    public IEnumerable<DataGridSearchTrigger> SearchTriggers { get; } = Enum.GetValues(typeof(DataGridSearchTrigger)).Cast<DataGridSearchTrigger>();

    public DataGridColumnCollection Columns { get; set; }

    public DataGridSearchPanelVisibilityMode SearchPanelVisibilityMode
    {
        get => this.searchPanelVisibilityMode;
        set => this.UpdateValue(ref this.searchPanelVisibilityMode, value);
    }

    public DataGridSearchTrigger SearchTrigger
    {
        get => this.searchTrigger;
        set => this.UpdateValue(ref this.searchTrigger, value);
    }
}
