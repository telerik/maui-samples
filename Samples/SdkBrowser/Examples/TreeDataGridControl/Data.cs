using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.TreeDataGridControl;

// >> treedatagrid-data-model
public class Data
{
	public Data(string name, int size, string type)
	{
		this.Name = name;
		this.Size = size;
		this.Type = type;
		this.Children = new ObservableCollection<Data>();
	}

	public string Name { get; set; }
	public int Size { get; set; }
	public string Type { get; set; }
	public ObservableCollection<Data> Children { get; set; }
}
// << treedatagrid-data-model
