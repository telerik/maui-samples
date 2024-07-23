using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Telerik.Theming.Styles;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class DataGrid : ResourceDictionary
{
	public DataGrid()
	{
		InitializeComponent();
	}
}