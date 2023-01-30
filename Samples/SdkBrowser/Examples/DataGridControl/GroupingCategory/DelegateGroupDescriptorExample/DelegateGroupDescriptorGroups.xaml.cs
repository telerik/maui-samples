using Telerik.Maui.Controls.Compatibility.Common.Data;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.GroupingCategory.DelegateGroupDescriptorExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DelegateGroupDescriptorGroups : RadContentView
{
    public DelegateGroupDescriptorGroups()
    {
        InitializeComponent();
        
        this.BindingContext = new ViewModel();

        // >> datagrid-grouping-delegategroupdescriptor
        this.dataGrid.GroupDescriptors.Add(new DelegateGroupDescriptor() { DisplayContent = "Name", KeyLookup = new CustomIKeyLookup() });
        // << datagrid-grouping-delegategroupdescriptor
    }
}

// >> datagrid-grouping-delegategroupdescriptor-lookup
class CustomIKeyLookup : Telerik.Maui.Controls.Compatibility.Common.Data.IKeyLookup
{
    public object GetKey(object instance)
    {
        var item = instance as Person;
        return item?.Name[0];
    }
}
// << datagrid-grouping-delegategroupdescriptor-lookup