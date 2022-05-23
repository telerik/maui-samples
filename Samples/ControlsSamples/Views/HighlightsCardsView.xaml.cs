using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using QSF.Helpers;
using System;
using System.Collections;
using System.Windows.Input;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class HighlightsCardsView : ContentView
{
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
        nameof(ItemTemplate), typeof(DataTemplate), typeof(HighlightsCardsView), null,
        propertyChanged: (b, o, n) => ((HighlightsCardsView)b).OnItemTemplateChanged());

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource), typeof(IEnumerable), typeof(HighlightsCardsView), null,
        propertyChanged: (b, o, n) => ((HighlightsCardsView)b).OnItemsSourceChanged());

    public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create(
        nameof(ItemTappedCommand), typeof(ICommand), typeof(HighlightsCardsView), null);

    private static readonly DataTemplate defaultItemTemplate;

    static HighlightsCardsView()
    {
        defaultItemTemplate = new DataTemplate(() =>
        {
            Label label = new Label();
            label.SetBinding(Label.TextProperty, new Binding { Path = "." });
            return label;
        });
    }

    public HighlightsCardsView()
    {
        this.InitializeComponent();
    }

    public DataTemplate ItemTemplate
    {
        get { return (DataTemplate)this.GetValue(ItemTemplateProperty); }
        set { this.SetValue(ItemTemplateProperty, value); }
    }

    public IEnumerable ItemsSource
    {
        get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
        set { this.SetValue(ItemsSourceProperty, value); }
    }

    public ICommand ItemTappedCommand
    {
        get { return (ICommand)this.GetValue(ItemTappedCommandProperty); }
        set { this.SetValue(ItemTappedCommandProperty, value); }
    }

    private void OnItemTemplateChanged()
    {
        this.UpdateGridChildren();
    }

    private void OnItemsSourceChanged()
    {
        this.UpdateGridChildren();
    }

    private void UpdateGridChildren()
    {
        foreach (View child in this.grid.Children)
        {
            child.BindingContext = null;
        }

        this.grid.Children.Clear();
        this.grid.RowDefinitions.Clear();
        this.grid.ColumnDefinitions.Clear();

        IEnumerable itemsSource = this.ItemsSource;
        if (itemsSource == null)
        {
            return;
        }

        int rows;
        int columns;
        this.CalculateRowsAndColumns(out rows, out columns);
        this.AddRowDefs(rows);
        this.AddColDefs(columns);

        foreach (object item in itemsSource)
        {
            int index = this.grid.Children.Count;

            View container = this.CreateContainer(index);
            container.BindingContext = item;

            if (this.ItemTappedCommand != null && this.ItemTappedCommand.CanExecute(item))
            {
                DesktopUtils.SetMouseCursorType(container, MouseCursorType.Hand);
            }

            Grid.SetRow(container, index / columns);
            Grid.SetColumn(container, index % columns);

            this.grid.Children.Add(container);
        }
    }

    private View CreateContainer(int index)
    {
        DataTemplate itemTemplate = this.ItemTemplate ?? defaultItemTemplate;
        View itemView = (View)itemTemplate.CreateContent();

        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Tapped += this.ItemContainer_Tapped;

        ContentView itemContainer = new ContentView();
        itemContainer.BackgroundColor = this.GetContainerBackgroundColor(index);
        itemContainer.GestureRecognizers.Add(tap);
        itemContainer.Content = itemView;

        return itemContainer;
    }

    private Color GetContainerBackgroundColor(int index)
    {
        index = index % 4;

        string key =
            index == 0 ? "Color1" :
            index == 1 ? "Color2" :
            index == 2 ? "Color3" :
            "Color4";

        object colorRes = this.Resources[key];
        Color color;

        if (colorRes is OnPlatform<Color> onPlatform)
        {
            color = (Color)onPlatform;
        }
        else if (colorRes is Color clr)
        {
            color = clr;
        }
        else
        {
            color = Colors.Red;
        }

        return color;
    }

    private void ItemContainer_Tapped(object sender, EventArgs e)
    {
        View view = (View)sender;
        ICommand command = this.ItemTappedCommand;
        object param = view.BindingContext;

        if (command != null && command.CanExecute(param))
        {
            command.Execute(param);
        }
    }

    private void CalculateRowsAndColumns(out int rows, out int columns)
    {
        int count = this.GetItemsCount();

        double sqrt = Math.Sqrt(count);
        columns = (int)Math.Ceiling(sqrt);
        rows = (int)Math.Floor(sqrt);

        if (rows * columns < count)
        {
            rows++;
        }
    }

    private int GetItemsCount()
    {
        int count = 0;

        if (this.ItemsSource != null)
        {
            foreach (object item in this.ItemsSource)
            {
                count++;
            }
        }

        return count;
    }

    private void AddRowDefs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            RowDefinition rowDefinition = new RowDefinition { Height = GridLength.Star };
            this.grid.RowDefinitions.Add(rowDefinition);
        }
    }

    private void AddColDefs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ColumnDefinition colDefinition = new ColumnDefinition { Width = GridLength.Star };
            this.grid.ColumnDefinitions.Add(colDefinition);
        }
    }
}