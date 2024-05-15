using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using Telerik.Maui.Data;

namespace SDKBrowserMaui.Examples.CollectionViewControl.GroupingCategory.ExpandCollapseExample;

public partial class ExpandCollapse : ContentView
{
    IDataViewCollection dataView;
    public ExpandCollapse()
    {
        InitializeComponent();
        // >> collectionview-getdataview
        this.dataView = this.collectionView.GetDataView();
        // << collectionview-getdataview
    }

    private void OnCollapseAllClicked(object sender, System.EventArgs e)
    {
        // >> collectionview-collapseall
        this.dataView.CollapseAll();
        // << collectionview-collapseall
    }

    private void OnExpandAllClicked(object sender, System.EventArgs e)
    {
        // >> collectionview-expandall
        this.dataView.ExpandAll();
        // << collectionview-expandall
    }

    private void OnCollapseFirstClicked(object sender, System.EventArgs e)
    {
        // >> collectionview-collapsegroup
        var groups = this.dataView.GetGroups();
        var firstGroup = groups.First();
        this.dataView.CollapseGroup(firstGroup);
        // << collectionview-collapsegroup

        this.collectionView.ScrollItemIntoView(firstGroup);
    }

    private void OnExpandFirstClicked(object sender, System.EventArgs e)
    {
        // >> collectionview-expandgroup
        var groups = this.dataView.GetGroups();
        var firstGroup = groups.First();
        this.dataView.ExpandGroup(firstGroup);
        // << collectionview-expandgroup

        this.collectionView.ScrollItemIntoView(firstGroup);
    }

    private void OnCollapseLastItemClicked(object sender, System.EventArgs e)
    {
        // >> collectionview-collapseitem
        var lastGroup = this.dataView.GetGroups().Last();
        var lastItem = lastGroup.ChildItems.Last();
        this.dataView.CollapseItem(lastItem);
        // << collectionview-collapseitem

        this.collectionView.ScrollItemIntoView(lastGroup);
    }

    private void OnExpandLastItemClicked(object sender, System.EventArgs e)
    {
        // >> collectionview-expanditem
        var lastGroup = this.dataView.GetGroups().Last();
        var lastItem = lastGroup.ChildItems.Last();
        this.dataView.ExpandItem(lastItem);
        // << collectionview-expanditem

        this.collectionView.ScrollItemIntoView(lastItem);
    }
}