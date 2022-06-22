using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.GroupingCategory.PropertyGroupDescriptorExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PropertyGroupDescriptor : RadContentView
{
    public PropertyGroupDescriptor()
    {
        InitializeComponent();

        // >> datagrid-grouping-propertygroupdescriptor-setvm
        this.BindingContext = new ViewModel();
        // << datagrid-grouping-propertygroupdescriptor-setvm

        this.dataGrid.GroupDescriptors.Add(new Telerik.Maui.Controls.Compatibility.Common.Data.PropertyGroupDescriptor()
        {
            PropertyName="Department"
        });
    }
}