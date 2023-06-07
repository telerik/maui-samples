using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Data;
using System.Runtime.CompilerServices;
using Telerik.Maui;

namespace QSF.Examples.DataGridControl.VariousDataSourcesExample;

public class VariousDataSourcesViewModel : ConfigurationExampleViewModel
{
    private string datasourceType = "Data Table";
    private object items;

    public VariousDataSourcesViewModel()
    {
        UpdateDataSource();
    }

    public string DatasourceType
    {
        get
        {
            return this.datasourceType;
        }
        set
        {
            if (this.datasourceType != value)
            {
                this.datasourceType = value;
                this.OnPropertyChanged();
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

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == nameof(DatasourceType))
        {
            UpdateDataSource();
        }
    }

    private void UpdateDataSource()
    {
        if (this.DatasourceType == "Collection")
        {
            this.Items = DataGenerator.GetItems<ObservableItemCollection<SalesPerson>>(ResourcePaths.PeoplePath);
        }
        else
        {
            this.Items = GetDataTable();
        }
    }

    private DataTable GetDataTable()
    {
        DataTable salesData = new DataTable();
        salesData.TableName = "Items";
        salesData.Columns.Add("FullName", typeof(string));
        salesData.Columns.Add("Sales", typeof(double));

        salesData.Rows.Add("Brian Albrecht", 23000);
        salesData.Rows.Add("Greg Alderson", 2500);
        salesData.Rows.Add("Dan Bacon", 3100);
        salesData.Rows.Add("Natalie Bailey", 12200);
        salesData.Rows.Add("Bryan Baker", 7880);
        salesData.Rows.Add("Angela Barbariol", 4800);
        salesData.Rows.Add("David Barber", 7200);
        salesData.Rows.Add("Isabella Barnes", 6300);
        salesData.Rows.Add("Paula Barreto de Mattos", 18000);
        salesData.Rows.Add("Mark Bebbington", 8450);
        salesData.Rows.Add("Bonnie Beck", 11100);
        salesData.Rows.Add("Gregory Becker", 14600);
        salesData.Rows.Add("Alexandra Bell", 17000);
        salesData.Rows.Add("Albert Cabello", 2100);
        salesData.Rows.Add("Crystal Cai", 10300);
        salesData.Rows.Add("John Campbell", 9810);
        salesData.Rows.Add("Gabrielle Cannata", 5700);
        salesData.Rows.Add("Jun Cao", 5550);
        salesData.Rows.Add("Francis Carlson", 9200);
        salesData.Rows.Add("Jillian Carson", 16200);

        return salesData;
    }
}