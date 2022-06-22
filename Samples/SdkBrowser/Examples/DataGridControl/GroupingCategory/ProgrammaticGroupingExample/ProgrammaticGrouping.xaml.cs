using Microsoft.Maui.Controls.Xaml;
using SDKBrowser.Examples.DataGridControl;
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

        var source = new ObservableCollection<Person>();
        source.Add(new Person() { Name = "Kiko", Age = 23, Gender = Gender.Other });
        source.Add(new Person() { Name = "Jerry", Age = 23, Gender = Gender.Male });
        source.Add(new Person() { Name = "Ethan", Age = 51, Gender = Gender.Male });
        source.Add(new Person() { Name = "Isabella", Age = 23, Gender = Gender.Female });
        source.Add(new Person() { Name = "Joshua", Age = 51, Gender = Gender.Male });
        source.Add(new Person() { Name = "Logan", Age = 51, Gender = Gender.Female });
        source.Add(new Person() { Name = "Aaron", Age = 23, Gender = Gender.Other });

        this.BindingContext = source;
        this.columnPicker.ItemsSource = new List<string>() { "Name", "Age", "Gender" };
        this.columnPicker.SelectedItem = "Gender";
    }

    private void GroupClicked(object sender, System.EventArgs e)
    {
        var selectedColumnName = this.columnPicker.SelectedItem.ToString();

        if (!this.grid.GroupDescriptors.Where(d => (d as PropertyGroupDescriptor).PropertyName.Equals(selectedColumnName)).Any())
        {
            this.grid.GroupDescriptors.Add(new PropertyGroupDescriptor() { PropertyName = selectedColumnName });
        }
    }

    private void UngroupClicked(object sender, System.EventArgs e)
    {
        var selectedColumnName = this.columnPicker.SelectedItem.ToString();
        var descriptor = this.grid.GroupDescriptors.Where(d => (d as PropertyGroupDescriptor).PropertyName.Equals(selectedColumnName)).FirstOrDefault();
        if (descriptor != null)
        {
            this.grid.GroupDescriptors.Remove(descriptor);
        }
    }
}