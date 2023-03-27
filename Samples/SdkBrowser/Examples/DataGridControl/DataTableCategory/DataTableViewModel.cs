using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.DataTableCategory
{
    // >> datagrid-datatable-viewmodel
    public class DataTableViewModel : NotifyPropertyChangedBase
    {
        private DataTable data;

        public DataTableViewModel() 
        {
            this.Data = this.GetData();
        }

        public DataTable Data
        {
            get { return this.data; }
            set { this.UpdateValue(ref this.data, value); }
        }

        private DataTable GetData()
        { 
            DataTable country = new DataTable();
            country.TableName = "Cities";
            country.Columns.Add("Id", typeof(int));
            country.Columns.Add("City", typeof(string));
            country.Columns.Add("Population", typeof(int));
            country.Columns.Add("Visited On", typeof(DateTime));
            country.Columns.Add("IsVisited", typeof(bool));

            country.Rows.Add(0, "Sofia", 2000000, new DateTime(2012, 10, 1), true);
            country.Rows.Add(1, "New York", 8419000, null, false);
            country.Rows.Add(2, "London", 8982000, new DateTime(2019, 02, 11), true);
            country.Rows.Add(3, "Los Angeles", 3967000, null, false);
            country.Rows.Add(4, "Budapest", 1765000, new DateTime(2013, 02, 1), true);
            country.Rows.Add(5, "Tokyo", 9375104, new DateTime(2015, 09, 1), true);

            return country;
        }
    }
    // << datagrid-datatable-viewmodel
}
