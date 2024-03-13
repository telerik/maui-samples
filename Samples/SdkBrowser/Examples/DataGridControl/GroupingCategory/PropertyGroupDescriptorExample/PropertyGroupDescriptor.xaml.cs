using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.GroupingCategory.PropertyGroupDescriptorExample;

public partial class PropertyGroupDescriptor : ContentView
{
    public PropertyGroupDescriptor()
    {
        InitializeComponent();

        // >> datagrid-grouping-propertygroupdescriptor
        this.dataGrid.GroupDescriptors.Add(new Telerik.Maui.Controls.Compatibility.Common.Data.PropertyGroupDescriptor()
        {
            PropertyName="Department"
        });
        // << datagrid-grouping-propertygroupdescriptor

        // >> datagrid-grouping-propertygroupdescriptor-setvm
        this.BindingContext = new ViewModel();
        // << datagrid-grouping-propertygroupdescriptor-setvm

    }
}