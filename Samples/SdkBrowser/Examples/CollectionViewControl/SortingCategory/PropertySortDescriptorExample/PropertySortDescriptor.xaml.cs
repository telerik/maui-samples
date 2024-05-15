using Microsoft.Maui.Controls;
using TelerikData = Telerik.Maui.Controls.Data;

namespace SDKBrowserMaui.Examples.CollectionViewControl.SortingCategory.PropertySortDescriptorExample;

public partial class PropertySortDescriptor : ContentView
{
    private TelerikData.PropertySortDescriptor sortDescriptor = new TelerikData.PropertySortDescriptor { PropertyName = "City" };

    public PropertySortDescriptor()
    {
        InitializeComponent();
    }

    private void UpdateSortOrder()
    {
        bool isCurrentSortOrderAscending = this.sortButton.Content.ToString().Contains(TelerikData.SortOrder.Ascending.ToString());

        this.sortDescriptor.SortOrder = isCurrentSortOrderAscending ? TelerikData.SortOrder.Ascending : TelerikData.SortOrder.Descending;
        this.sortButton.Content = isCurrentSortOrderAscending ? $"Sort {TelerikData.SortOrder.Descending}" : $"Sort {TelerikData.SortOrder.Ascending}";
    }

    private void OnSortBtnClicked(object sender, System.EventArgs e)
    {
        this.collectionView.SortDescriptors.Clear();
        this.UpdateSortOrder();
        this.collectionView.SortDescriptors.Add(this.sortDescriptor);
    }

    private void OnClearSortingBtnClicked(object sender, System.EventArgs e)
    {
        this.collectionView.SortDescriptors.Clear();
    }
}