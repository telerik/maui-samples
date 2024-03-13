using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using Telerik.Maui;

namespace QSF.Examples.DataGridControl.VariousDataSourcesExample;

public class VariousDataSourcesViewModel : ConfigurationExampleViewModel
{
    private DataSourcesTypes dataSourcesType = DataSourcesTypes.DataTable;
    private DataTable dataTableSource;
    private ObservableCollection<ExpandoObject> expandoObjectsSource;
    private ObservableCollection<DynamicObject> dynamicObjectsSource;
    private ObservableItemCollection<SalesPerson> salesPeople;
    private object items;

    public VariousDataSourcesViewModel()
    {
        this.salesPeople = DataGenerator.GetItems<ObservableItemCollection<SalesPerson>>(ResourcePaths.PeoplePath);
        this.UpdateDataSource();
    }

    public Array DataSources
    {
        get => Enum.GetValues(typeof(DataSourcesTypes));
    }

    public DataSourcesTypes DataSourcesType
    {
        get
        {
            return this.dataSourcesType;
        }
        set
        {
            if (this.dataSourcesType != value)
            {
                this.dataSourcesType = value;
                this.OnPropertyChanged();
                this.UpdateDataSource();
            }
        }
    }

    public object Items
    {
        get
        {
            return this.items;
        }
        set
        {
            if (this.items != value)
            {
                this.items = value;
                this.OnPropertyChanged();
            }
        }
    }

    private void UpdateDataSource()
    {
        switch (this.DataSourcesType)
        {
            case DataSourcesTypes.Collection:
                this.Items = this.salesPeople;
                break;
#if WINDOWS || ANDROID
            case DataSourcesTypes.ExpandoObjects:
                this.Items = this.GetExpandoObjectItems();
                break;
            case DataSourcesTypes.DynamicObjects:
                this.Items = this.GetDynamicObjectItems();
                break;
#endif
            default:
                this.Items = this.GetDataTable();
                break;
        }
    }

    private DataTable GetDataTable()
    {
        if (this.dataTableSource != null)
        {
            return this.dataTableSource;
        }

        this.dataTableSource = new DataTable();
        this.dataTableSource.TableName = "Items";
        this.dataTableSource.Columns.Add("FullName", typeof(string));
        this.dataTableSource.Columns.Add("Sales", typeof(double));

        foreach (var salesPerson in salesPeople.Skip(50).Take(40))
        {
            this.dataTableSource.Rows.Add(salesPerson.FullName, salesPerson.Sales);
        }

        return this.dataTableSource;
    }

#if WINDOWS || ANDROID
    private ObservableCollection<ExpandoObject> GetExpandoObjectItems()
    {
        if (this.expandoObjectsSource != null)
        {
            return this.expandoObjectsSource;
        }

        this.expandoObjectsSource = new ObservableCollection<ExpandoObject>();

        foreach (var salesPerson in salesPeople.Skip(90).Take(30))
        {
            dynamic expandoPerson = new ExpandoObject();
            expandoPerson.FullName = salesPerson.FullName;
            expandoPerson.Sales = salesPerson.Sales;
            this.expandoObjectsSource.Add(expandoPerson);
        }

        return this.expandoObjectsSource;
    }

    private ObservableCollection<DynamicObject> GetDynamicObjectItems()
    {
        if (this.dynamicObjectsSource != null)
        {
            return this.dynamicObjectsSource;
        }

        this.dynamicObjectsSource = new ObservableCollection<DynamicObject>();

        foreach (var salesPerson in salesPeople.TakeLast(60))
        {
            SalesPersonDynamicObject dynamicPerson = new SalesPersonDynamicObject();
            dynamicPerson["FullName"] = salesPerson.FullName;
            dynamicPerson["Sales"] = salesPerson.Sales;
            this.dynamicObjectsSource.Add(dynamicPerson);
        }

        return this.dynamicObjectsSource;
    }
#endif
}