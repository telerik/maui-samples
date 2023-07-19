using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.RowDetailsCategory.RowDetailsExample;

public partial class RowDetails : RadContentView
{
    public RowDetails()
	{
		InitializeComponent();

        // >> datagrid-items-source
        List<Data> items = new List<Data>
        {
            new Data { Country = "India", Capital = "New Delhi" , Details = "New Delhi is the capital of India and a part of the National Capital Territory of Delhi (NCT). New Delhi is the seat of all three branches of the Government of India, hosting the Rashtrapati Bhavan, Sansad Bhavan, and the Supreme Court."},
            new Data { Country = "South Africa", Capital = "Cape Town", Details = "Cape Town is South Africa's oldest city. It serves as the country's legislative capital, being the seat of the South African Parliament.It is the country's second-largest city (after Johannesburg) and the largest in the Western Cape."},
            new Data { Country = "Nigeria", Capital = "Abuja" , Details = "Abuja is the capital city of Nigeria. When it was decided to move the national capital from Lagos in 1976, a capital territory was chosen for its location near the centre of the country. The planned city is located in the centre of what is now the Federal Capital Territory." },
            new Data { Country = "Singapore", Capital = "Singapore" , Details = "Singapore is the capital city of the Republic of Singapore. It occupies the southern part of Singapore Island. Its strategic position on the strait between the Indian Ocean and South China Sea, complemented by its deepwater harbour, has made it the largest port in Southeast Asia." }
        };
        // << datagrid-items-source

        // >> datagrid-expand-rowdetails
        this.dataGrid.ItemsSource = items;
        this.dataGrid.ExpandedRowDetails = new ObservableCollection<Data>(items);
        // << datagrid-expand-rowdetails
    }

    // >> datagrid-business-model
    public class Data
    {
        public string Country { get; set; }
        public string Capital { get; set; }
        public string Details { get; set; }
    }
    // << datagrid-business-model
}