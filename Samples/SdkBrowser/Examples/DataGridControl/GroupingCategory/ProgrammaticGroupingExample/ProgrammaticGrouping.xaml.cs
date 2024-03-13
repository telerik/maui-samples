using Microsoft.Maui.Controls.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Common.Data;

namespace SDKBrowserMaui.Examples.DataGridControl.GroupingCategory.ProgrammaticGroupingExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ProgrammaticGrouping : RadContentView
{
    public ProgrammaticGrouping()
    {
        this.InitializeComponent();

        this.BindingContext = new ViewModel();
        this.columnChooser.ItemsSource = new List<string>() { "Name", "Age", "Department" };
        this.columnChooser.SelectedItem = "Department";
    }

    private void GroupClicked(object sender, System.EventArgs e)
    {
        var selectedColumnName = this.columnChooser.SelectedItem.ToString();

        if (!this.grid.GroupDescriptors.Where(d => (d as PropertyGroupDescriptor).PropertyName.Equals(selectedColumnName)).Any())
        {
            this.grid.GroupDescriptors.Add(new PropertyGroupDescriptor() { PropertyName = selectedColumnName });
        }
    }

    private void UngroupClicked(object sender, System.EventArgs e)
    {
        var selectedColumnName = this.columnChooser.SelectedItem.ToString();
        var descriptor = this.grid.GroupDescriptors.Where(d => (d as PropertyGroupDescriptor).PropertyName.Equals(selectedColumnName)).FirstOrDefault();
        if (descriptor != null)
        {
            this.grid.GroupDescriptors.Remove(descriptor);
        }
    }
}