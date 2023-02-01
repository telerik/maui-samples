using System;
using System.Collections;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace QSF.Behaviors;

public static class BindableTabs
{
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.CreateAttached("ItemsSource", typeof(IEnumerable), typeof(BindableTabs),
            null, propertyChanged: OnItemsSourceChanged);

    public static readonly BindableProperty ItemTemplateProperty =
        BindableProperty.CreateAttached("ItemTemplate", typeof(DataTemplate), typeof(BindableTabs),
            null, propertyChanged: OnItemTemplateChanged);

    public static IEnumerable GetItemsSource(BindableObject bindable)
    {
        return (IEnumerable)bindable.GetValue(ItemsSourceProperty);
    }

    public static void SetItemsSource(BindableObject bindable, IEnumerable itemsSource)
    {
        bindable.SetValue(ItemsSourceProperty, itemsSource);
    }

    public static DataTemplate GetItemTemplate(BindableObject bindable)
    {
        return (DataTemplate)bindable.GetValue(ItemTemplateProperty);
    }

    public static void SetItemTemplate(BindableObject bindable, DataTemplate itemTemplate)
    {
        bindable.SetValue(ItemTemplateProperty, itemTemplate);
    }

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var tabView = (RadTabView)bindable;
        var itemsSource = (IEnumerable)newValue;
        var itemTemplate = GetItemTemplate(bindable);

        UpdateTabViewItems(tabView, itemsSource, itemTemplate);
    }

    private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var tabView = (RadTabView)bindable;
        var itemTemplate = (DataTemplate)newValue;
        var itemsSource = GetItemsSource(bindable);

        UpdateTabViewItems(tabView, itemsSource, itemTemplate);
    }

    private static void UpdateTabViewItems(RadTabView tabView, IEnumerable itemsSource, DataTemplate itemTemplate)
    {
        tabView.Items.Clear();

        if (itemsSource is null || itemTemplate is null)
        {
            return;
        }

        foreach (var dataItem in itemsSource)
        {
            var itemHeader = Convert.ToString(dataItem);
            var itemContent = (View)itemTemplate.CreateContent();

            itemContent.BindingContext = dataItem;

            var tabViewItem = new TabViewItem
            {
                BindingContext = dataItem,
                HeaderText = itemHeader,
                Content = itemContent
            };

            tabView.Items.Add(tabViewItem);
        }
    }
}
