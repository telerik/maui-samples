using Microsoft.Maui.Controls;
using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace QSF.Examples.DataGridControl.EmptyTemplateExample;

public class EmptyTemplateViewModel : ConfigurationExampleViewModel
{
    private object itemsSource;
    private EmptyContentDisplayMode emptyContentDisplayMode;
    private bool useEmptyCollectionSource;
    private bool useNonEmptyCollectionSource;
    private bool useNullSource;
    private DataGridColumnCollection columns;

    public EmptyTemplateViewModel()
    {
        this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
        this.EmptyContentDisplayMode = this.EmptyContentDisplayModes.Last();
        this.useEmptyCollectionSource = true;
    }

    public IEnumerable<EmptyContentDisplayMode> EmptyContentDisplayModes { get; } = Enum.GetValues(typeof(EmptyContentDisplayMode)).Cast<EmptyContentDisplayMode>();

    public ObservableCollection<Order> Orders { get; private set; }

    public DataGridColumnCollection Columns
    {
        get => this.columns;
        set
        {
            this.columns = value;

            if (this.useNullSource)
            {
                this.SetNullItemsSource();
            }
            else if (this.useEmptyCollectionSource)
            {
                this.SetEmptyCollectionItemsSource();
            }
            else if (this.useNonEmptyCollectionSource)
            {
                this.SetOrdersItemsSource();
            }
        }
    }

    public object ItemsSource
    {
        get => this.itemsSource;
        set => this.UpdateValue(ref this.itemsSource, value);
    }

    public EmptyContentDisplayMode EmptyContentDisplayMode
    {
        get => this.emptyContentDisplayMode;
        set => this.UpdateValue(ref this.emptyContentDisplayMode, value);
    }

    public bool UseEmptyCollectionSource
    {
        get => this.useEmptyCollectionSource;
        set
        {
            if (this.useEmptyCollectionSource != value)
            {
                this.useEmptyCollectionSource = value;
                this.OnPropertyChanged();

                if (this.useEmptyCollectionSource)
                {
                    this.UseNonEmptyCollectionSource = false;
                    this.UseNullSource = false;

                    this.SetEmptyCollectionItemsSource();
                }
            }
        }
    }

    public bool UseNonEmptyCollectionSource
    {
        get => this.useNonEmptyCollectionSource;
        set
        {
            if (this.useNonEmptyCollectionSource != value)
            {
                this.useNonEmptyCollectionSource = value;
                this.OnPropertyChanged();

                if (this.useNonEmptyCollectionSource)
                {
                    this.UseEmptyCollectionSource = false;
                    this.UseNullSource = false;

                    this.SetOrdersItemsSource();
                }
            }
        }
    }

    public bool UseNullSource
    {
        get => this.useNullSource;
        set
        {
            if (this.useNullSource != value)
            {
                this.useNullSource = value;
                this.OnPropertyChanged();

                if (this.useNullSource)
                {
                    this.UseNonEmptyCollectionSource = false;
                    this.UseEmptyCollectionSource = false;

                    this.SetNullItemsSource();
                }
            }
        }
    }

    private void InitColumns()
    {
        if (this.columns == null || this.columns.Count > 0)
        {
            return;
        }

        this.columns.Clear();
        this.columns.Add(new DataGridNumericalColumn() { PropertyName = "OrderID", HeaderText="Order ID" });
        this.columns.Add(new DataGridNumericalColumn() { PropertyName = "OrderDate", HeaderText="Order Date" });
        this.columns.Add(new DataGridNumericalColumn() { PropertyName = "ShippedDate", HeaderText="Shipped Date" });
        this.columns.Add(new DataGridNumericalColumn() { PropertyName = "ShipName", HeaderText="Ship Name" });
        this.columns.Add(new DataGridNumericalColumn() { PropertyName = "ShipCity", HeaderText="Ship City" });
        this.columns.Add(new DataGridNumericalColumn() { PropertyName = "ShipCountry", HeaderText="Ship Country" });
        this.columns.Add(new DataGridNumericalColumn() { PropertyName = "ShipPostalCode", HeaderText="Ship Postal Code" });
    }

    private void SetOrdersItemsSource()
    {
        this.ItemsSource = this.Orders;
        this.InitColumns();
    }

    private void SetNullItemsSource()
    {
        this.ItemsSource = null;
        this.columns?.Clear();
    }

    private void SetEmptyCollectionItemsSource()
    {
        this.ItemsSource = new ObservableCollection<Order>();
        this.InitColumns();
    }
}

