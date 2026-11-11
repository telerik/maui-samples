using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace QSF.Examples.ChartsControl.FirstLookExample;

public partial class FirstLookView : ContentView
{
    private const double DesktopBreakpoint = 900;

    private bool? isDesktopLayout;

    public FirstLookView()
    {
        this.InitializeComponent();
        this.ApplyLayout(DeviceInfo.Idiom == DeviceIdiom.Desktop);
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width > 0)
        {
            this.ApplyLayout(width >= DesktopBreakpoint);
        }
    }

    private void ApplyLayout(bool isDesktop)
    {
        if (this.isDesktopLayout == isDesktop)
        {
            return;
        }

        this.isDesktopLayout = isDesktop;

        if (isDesktop)
        {
            this.ApplyDesktopLayout();
        }
        else
        {
            this.ApplyMobileLayout();
        }

        this.LayoutKpis(isDesktop ? 4 : 2);
    }

    // Desktop: KPI row on top, then a 3-column dashboard grid.
    private void ApplyDesktopLayout()
    {
        this.SetColumns(new GridLength(2, GridUnitType.Star), GridLength.Star, GridLength.Star);
        this.SetRows(6);

        Place(this.headerPanel, 0, 0, 3);
        Place(this.kpiPanel, 1, 0, 3);
        Place(this.revenueCard, 2, 0, 2);
        Place(this.channelsCard, 2, 2);
        Place(this.regionsCard, 3, 0);
        Place(this.campaignsCard, 3, 1);
        Place(this.productsCard, 3, 2);
        Place(this.candlestickCard, 4, 0, 2);
        Place(this.trafficCard, 4, 2);
        Place(this.customersCard, 5, 0);
        Place(this.ohlcCard, 5, 1, 2);
    }

    // Mobile: single column, with the most important cards first.
    private void ApplyMobileLayout()
    {
        this.SetColumns(GridLength.Star);
        this.SetRows(11);

        var order = new View[]
        {
            this.headerPanel, this.kpiPanel, this.revenueCard, this.channelsCard, this.regionsCard, this.trafficCard,
            this.campaignsCard, this.productsCard, this.customersCard, this.candlestickCard, this.ohlcCard,
        };

        for (int i = 0; i < order.Length; i++)
        {
            Place(order[i], i, 0);
        }
    }

    private void LayoutKpis(int columns)
    {
        if (this.BindingContext is not FirstLookViewModel context)
        {
            return;
        }

        var template = (DataTemplate)this.Resources["KpiTemplate"];

        this.kpiPanel.Children.Clear();
        this.kpiPanel.ColumnDefinitions.Clear();
        this.kpiPanel.RowDefinitions.Clear();

        for (int c = 0; c < columns; c++)
        {
            this.kpiPanel.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        for (int i = 0; i < context.Kpis.Count; i++)
        {
            if (i % columns == 0)
            {
                this.kpiPanel.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
            }

            var view = (View)template.CreateContent();
            view.BindingContext = context.Kpis[i];
            Grid.SetRow(view, i / columns);
            Grid.SetColumn(view, i % columns);
            this.kpiPanel.Children.Add(view);
        }
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (this.BindingContext is FirstLookViewModel && this.isDesktopLayout.HasValue)
        {
            this.LayoutKpis(this.isDesktopLayout.Value ? 4 : 2);
        }
    }

    private void SetColumns(params GridLength[] widths)
    {
        this.dashboard.ColumnDefinitions.Clear();
        foreach (var width in widths)
        {
            this.dashboard.ColumnDefinitions.Add(new ColumnDefinition(width));
        }
    }

    private void SetRows(int count)
    {
        this.dashboard.RowDefinitions.Clear();
        for (int i = 0; i < count; i++)
        {
            this.dashboard.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        }
    }

    private static void Place(View view, int row, int column, int columnSpan = 1)
    {
        Grid.SetRow(view, row);
        Grid.SetColumn(view, column);
        Grid.SetColumnSpan(view, columnSpan);
    }
}
