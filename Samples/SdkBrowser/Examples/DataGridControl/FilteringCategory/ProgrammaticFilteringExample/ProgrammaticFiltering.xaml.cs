using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Common.Data;

namespace SDKBrowserMaui.Examples.DataGridControl.FilteringCategory.ProgrammaticFilteringExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ProgrammaticFiltering : RadContentView
{
    private TextFilterDescriptor textFilterDescriptor = new TextFilterDescriptor() { PropertyName = "Name", IsCaseSensitive = false, Operator = TextOperator.StartsWith };
    private NumericalFilterDescriptor numericalFilterDescriptor = new NumericalFilterDescriptor() { PropertyName = "Population", Operator = NumericalOperator.IsGreaterThan };

    public ProgrammaticFiltering()
    {
        this.InitializeComponent();

        this.BindingContext = new FilteringViewModel();

        this.textOperatorPicker.SelectedItem = TextOperator.StartsWith;
        this.numericalOperatorPicker.SelectedItem = NumericalOperator.IsGreaterThan;
        
    }
    
    private void NameFilterValueChanged(object sender, TextChangedEventArgs e)
    {
        this.textFilterDescriptor.Value = (sender as Entry).Text;
        this.ApplyTextFilter();
    }

    private void TextOperatorChanged(object sender, System.EventArgs e)
    {
        this.textFilterDescriptor.Operator = (TextOperator)(sender as Picker).SelectedItem;
        this.ApplyTextFilter();
    }

    private void ClearNameFilter(object sender, EventArgs e)
    {
        this.grid.FilterDescriptors.Remove(this.textFilterDescriptor);
        this.textFilterEntry.Text = String.Empty;
    }

    private void PopulationFilterValueChanged(object sender, TextChangedEventArgs e)
    {
        this.numericalFilterDescriptor.Value = (sender as Entry).Text;
        this.ApplyNumericalFilter();
    }

    private void NumericalOperatorChanged(object sender, EventArgs e)
    {
        this.numericalFilterDescriptor.Operator = (NumericalOperator)(sender as Picker).SelectedItem;
        this.ApplyNumericalFilter();
    }

    private void ClearPopulationFilter(object sender, EventArgs e)
    {
        this.grid.FilterDescriptors.Remove(this.numericalFilterDescriptor);
        this.populationEntry.Text = String.Empty;
    }

    private void ApplyTextFilter()
    {
        this.grid.FilterDescriptors.Remove(this.textFilterDescriptor);
        var filterValue = this.textFilterDescriptor.Value;
        if (filterValue != null && !filterValue.ToString().Equals(String.Empty))
        {
            this.grid.FilterDescriptors.Add(this.textFilterDescriptor);
        }
    }

    private void ApplyNumericalFilter()
    {
        this.grid.FilterDescriptors.Remove(this.numericalFilterDescriptor);
        var filterValue = this.numericalFilterDescriptor.Value;
        double number;
        if (filterValue != null && Double.TryParse(filterValue.ToString(), out number))
        {
            this.grid.FilterDescriptors.Add(this.numericalFilterDescriptor);
        }
    }
}