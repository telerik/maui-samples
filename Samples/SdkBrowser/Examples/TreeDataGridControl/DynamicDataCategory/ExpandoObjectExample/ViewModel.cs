using System.Collections.ObjectModel;
using System.Dynamic;

namespace SDKBrowserMaui.Examples.TreeDataGridControl.DynamicDataCategory.ExpandoObjectExample;

// >> treedatagrid-expandoobject-viewmodel
public class ViewModel
{
	private int id;

	public ViewModel()
	{
		this.Data = new ObservableCollection<ExpandoObject>();

		for (int i = 0; i < 10; i++)
		{
			dynamic item = new ExpandoObject();
			item.Country = string.Format("Country (id:{0}) {1} {2}", ++this.id, 0, i);
			item.Capital = string.Format("Capital (id:{0}) {1} {2}", this.id, 0, i);
			item.Population = i * 1000;
			item.Children = new ObservableCollection<ExpandoObject>();

			for (int k = 0; k < 3; k++)
			{
				dynamic child1 = new ExpandoObject();
				child1.Country = string.Format("Country (id:{0}) {1} {2}", ++this.id, 1, k);
				child1.Capital = string.Format("Capital (id:{0}) {1} {2}", this.id, 1, k);
				child1.Population = k * 100;
				child1.Children = new ObservableCollection<ExpandoObject>();

				for (int m = 0; m < 3; m++)
				{
					dynamic child2 = new ExpandoObject();
					child2.Country = string.Format("Country (id:{0}) {1} {2}", ++this.id, 2, m);
					child2.Capital = string.Format("Capital (id:{0}) {1} {2}", this.id, 2, m);
					child2.Population = i * 10 + k * 10 + m * 10;

					child1.Children.Add(child2);
				}

				item.Children.Add(child1);
			}

			this.Data.Add(item);
		}
	}

	public ObservableCollection<ExpandoObject> Data { get; set; }
}
// << treedatagrid-expandoobject-viewmodel
