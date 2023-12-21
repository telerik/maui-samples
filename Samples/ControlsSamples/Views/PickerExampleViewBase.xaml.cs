using Microsoft.Maui.Controls;
using QSF.ExampleUtilities;
using System.Collections.Generic;
using Telerik.Maui.Controls.DataControls;

namespace QSF.Views;

public partial class PickerExampleViewBase : ContentView
{
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(List<CardItem>), typeof(PickerExampleViewBase));

    public static readonly BindableProperty CollectionTitleProperty =
        BindableProperty.Create(nameof(CollectionTitle), typeof(string), typeof(PickerExampleViewBase));

    public static readonly BindableProperty ButtonCommandProperty =
        BindableProperty.Create(nameof(ButtonCommand), typeof(Command), typeof(PickerExampleViewBase));

    public static readonly BindableProperty ButtonTextProperty =
        BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(PickerExampleViewBase));

    public static readonly BindableProperty ItemTemplateProperty =
        BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), 
            typeof(PickerExampleViewBase), defaultValueCreator: b => ((PickerExampleViewBase)b).CreateDefaultItemTemplate());

    public PickerExampleViewBase()
    {
        this.InitializeComponent();

        this.Loaded += (s, e) =>
        {
            if (this.GetTemplateChild("PART_CardsItemsControl") is NonVirtualizedItemsControl cardsItemsControl)
            {
                cardsItemsControl.SelectedItem = this.ItemsSource[0];
            }
        };
    }

    public List<CardItem> ItemsSource
    {
        get => (List<CardItem>)this.GetValue(ItemsSourceProperty);
        set => this.SetValue(ItemsSourceProperty, value);
    }

    public string CollectionTitle
    {
        get => (string)this.GetValue(CollectionTitleProperty);
        set => this.SetValue(CollectionTitleProperty, value);
    }

    public Command ButtonCommand
    {
        get => (Command)this.GetValue(ButtonCommandProperty);
        set => this.SetValue(ButtonCommandProperty, value);
    }

    public string ButtonText
    {
        get => (string)this.GetValue(ButtonTextProperty);
        set => this.SetValue(ButtonTextProperty, value);
    }

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)this.GetValue(ItemTemplateProperty);
        set => this.SetValue(ItemTemplateProperty, value);
    }

    private DataTemplate CreateDefaultItemTemplate()
    {
        return (DataTemplate)this.Resources["DefaultItemTemplate"];
    }
}