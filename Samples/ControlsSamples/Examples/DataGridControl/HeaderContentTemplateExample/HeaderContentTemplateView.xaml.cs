using System;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;
using Telerik.Maui.Controls.Compatibility.DataGrid.Commands;

namespace QSF.Examples.DataGridControl.HeaderContentTemplateExample;

public partial class HeaderContentTemplateView : ContentView
{
    public HeaderContentTemplateView()
    {
        this.InitializeComponent();
    }

    private void FreezeButtonClicked(object sender, EventArgs e)
    {
        var column = ((View)sender).BindingContext as DataGridColumn;
        column.IsFrozen = !column.IsFrozen;
    }

    private void OnFilterIconTapped(object sender, EventArgs e)
    {
        var column = ((View)sender).BindingContext as DataGridColumn;
        var context = this.dataGrid.CreateFilterTapContext(column);
        this.dataGrid.CommandService.ExecuteDefaultCommand(DataGridCommandId.FilterTap, context);
    }
}