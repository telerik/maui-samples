using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Data;

namespace SDKBrowserMaui.Examples.CollectionViewControl.GroupingCategory.DelegateGroupExample;

public partial class DelegateGroup : ContentView
{
    public DelegateGroup()
    {
        InitializeComponent();
        // >> collectionview-delegate-grouping
        this.collectionView.GroupDescriptors.Add(new DelegateGroupDescriptor
        {
            DisplayContent = "Country",
            KeyLookup = new CustomIKeyLookup()
        });
        // << collectionview-delegate-grouping
    }
}

// >> collectionview-delegate-grouping-keyextractor-function
class CustomIKeyLookup : Telerik.Maui.Controls.Data.IKeyLookup
{
    public object GetKey(object arg)
    {
        var item = arg as DataModel;
        return item?.Country;
    }
}
// << collectionview-delegate-grouping-keyextractor-function