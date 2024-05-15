using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls.Data;

namespace SDKBrowserMaui.Examples.CollectionViewControl.FilteringCategory.FilterDescriptorExample;

// >> collectionview-filter-function
public partial class FilterDescriptor : ContentView
{
    private TextFilterDescriptor descriptor;

    public FilterDescriptor()
	{
		InitializeComponent();
	}

    private void OnFilterChanged(object sender, TextChangedEventArgs e)
    {
        var filterText = e.NewTextValue;
        if (this.descriptor != null)
        {
            this.collectionView.FilterDescriptors.Remove(this.descriptor);
        }

        this.descriptor = new TextFilterDescriptor
        {
            PropertyName = nameof(DataModel.City),
            IsCaseSensitive = false,
            Operator = TextOperator.Contains,
            Value = filterText,
        };

        this.collectionView.FilterDescriptors.Add(this.descriptor);
    }
}
// << collectionview-filter-function
