using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.StylingCategory.RowBackgroundStyleSelectorExample;

// >> datagrid-rowbackground-styleselector-class
public class MyRowBackgroundStyleSelector : IStyleSelector
{
	public Style RowBackgroundStyle { get; set; }

	public Style AlternateRowBackgroundStyle { get; set; }

	public Style RowDetailsBackgroundStyle { get; set; }

	public Style AlternateRowDetailsBackgroundStyle { get; set; }

	public Style SelectStyle(object item, BindableObject bindable)
	{
		if (item is DataGridRowInfo rowInfo)
		{
			// In case you want to style the row background based on the business object associated with the row.
			var myitem = rowInfo.Item as MyData;
			
			if (rowInfo.IsRowDetails)
			{
				if (rowInfo.IsAlternate)
				{
					return this.AlternateRowDetailsBackgroundStyle;
				}
				else
				{
					return this.RowDetailsBackgroundStyle;
				}
			}
			else
			{
				if (rowInfo.IsAlternate)
				{
					return this.AlternateRowBackgroundStyle;
				}
				else
				{
					return this.RowBackgroundStyle;
				}
			}
		}

		return null;
	}
}
// << datagrid-rowbackground-styleselector-class
