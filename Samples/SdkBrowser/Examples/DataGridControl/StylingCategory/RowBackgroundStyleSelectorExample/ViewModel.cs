using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.StylingCategory.RowBackgroundStyleSelectorExample;

// >> datagrid-rowbackground-styleselector-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
	public ViewModel()
	{
		this.Items = new ObservableCollection<MyData>()
		{
			new MyData { Name = "John" , Age = 18, City = "London"},
			new MyData { Name = "Mike" , Age = 56, City = "New York"},
			new MyData { Name = "Joan" , Age = 64, City = "Madrid"},
			new MyData { Name = "Seiji" , Age = 23, City = "Tokyo"},
			new MyData { Name = "Dobi" , Age = 21, City = "Amsterdam"},
			new MyData { Name = "Gun" , Age = 34, City = "Seoul"},
			new MyData { Name = "Chao" , Age = 23, City = "Nanjing"},
			new MyData { Name = "Jake" , Age = 34, City = "LA"},
			new MyData { Name = "Andrea" , Age = 53, City = "London"},
			new MyData { Name = "Miki" , Age = 12, City = "New York"},
			new MyData { Name = "Tonny" , Age = 43, City = "Seattle"},
			new MyData { Name = "John" , Age = 48, City = "Chicago"},
			new MyData { Name = "Eva" , Age = 22, City = "Mexico"},
			new MyData { Name = "Nicki" , Age = 19, City = "Chicago"},
			new MyData { Name = "Emma" , Age = 48, City = "Brazil"},
			
		};
	}

	public ObservableCollection<MyData> Items { get; set; } 
}
// << datagrid-rowbackground-styleselector-viewmodel
