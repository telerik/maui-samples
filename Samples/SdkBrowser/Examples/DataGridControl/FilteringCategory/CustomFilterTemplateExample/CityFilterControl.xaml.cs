using System.Collections.Generic;
using Telerik.Maui.Controls.DataGrid;
using Telerik.Maui.Controls.Data;

namespace SDKBrowserMaui.Examples.DataGridControl.FilteringCategory.CustomFilterTemplateExample;

// >> datagrid-filter-template-filtercontrolbase-code
public partial class CityFilterControl : DataGridFilterControlBase
{
	public CityFilterControl()
	{
		InitializeComponent();
	}

    public FilterDescriptorBase FilterDescriptor { get; set; }

    public override FilterDescriptorBase BuildDescriptor()
    {
        TextFilterDescriptor textDescriptor = new TextFilterDescriptor();
        textDescriptor.PropertyName = this.PropertyName;
        textDescriptor.Value = this.filterTextEntry.Text;
        textDescriptor.Operator = (TextOperator)this.descriptorOperatorSelector.SelectedItem;

        return textDescriptor;
    }

    protected override void Initialize()
    {
        this.descriptorOperatorSelector.ItemsSource = this.GetOperators();
        var textFilterDescriptor = this.FilterDescriptor as TextFilterDescriptor;
        if (textFilterDescriptor != null)
        {
            this.descriptorOperatorSelector.SelectedIndex = (int)textFilterDescriptor.Operator;
            this.filterTextEntry.Text = textFilterDescriptor.Value.ToString();
        }
        else
        {
            this.descriptorOperatorSelector.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(this.filterTextEntry.Text))
            {
                this.filterTextEntry.Text = null;
            }
        }
    }
    private List<TextOperator> GetOperators()
    {
        var operators = new List<TextOperator>
            {
                TextOperator.Contains,
                TextOperator.DoesNotContain,
                TextOperator.StartsWith,
                TextOperator.EndsWith,
            };
        return operators;
    }
}
// << datagrid-filter-template-filtercontrolbase-code