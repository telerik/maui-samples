using Microsoft.Maui.Controls;
using QSF.Examples.SpreadStreamProcessingControl.FirstLookExample;

namespace QSF.Examples.DataGridControl.ExportToExcelExample;

public partial class ExportToExcelView : ContentView
{
	public ExportToExcelView()
	{
		InitializeComponent();

		this.BindingContext = new FirstLookViewModel();
	}
}