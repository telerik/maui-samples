using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace QSF.Examples.DataGridControl.BillionCellsExample;

public partial class BillionCellsView : ContentView
{
    public BillionCellsView()
    {
        InitializeComponent();
        GenerateColumns();
    }

    public void GenerateColumns()
    {
        for (int i = 0; i < 100; i++)
        {
            var textColumn = new DataGridTextColumn();
            textColumn.HeaderText = $"column {i}";
            textColumn.PropertyName = nameof(DataItem.ID);
            radDataGrid.Columns.Add(textColumn);
        }
    }
}