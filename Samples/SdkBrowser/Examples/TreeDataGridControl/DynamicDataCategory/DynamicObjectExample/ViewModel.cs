using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.TreeDataGridControl.DynamicDataCategory.DynamicObjectExample;

// >> treedatagrid-dynamicobject-viewmodel
public class ViewModel
{
	private int id;

	public ViewModel()
	{
		this.Data = new ObservableCollection<MyDynamicObject>();

		for (int i = 0; i < 100; i++)
		{
			MyDynamicObject dynamicObject = this.CreateDynamicObject(i, 0, 2);
			this.Data.Add(dynamicObject);
		}
	}

	public ObservableCollection<MyDynamicObject> Data { get; set; }

	private MyDynamicObject CreateDynamicObject(int index, int level, int maxLevels)
	{
		MyDynamicObject dynamicObject = new MyDynamicObject();
		int localId = this.id++;
		dynamicObject["ID"] = localId;

		for (int j = 0; j < 10; j++)
		{
			dynamicObject[string.Format("Column{0}", j)] = string.Format("Cell (id:{0}) {1} {2}", localId, index, j);
		}

		if (level < maxLevels)
		{
			ObservableCollection<MyDynamicObject> children = new ObservableCollection<MyDynamicObject>();
			dynamicObject["Children"] = children;

			for (int k = 0; k < 5; k++)
			{
				MyDynamicObject child = this.CreateDynamicObject(k, level + 1, 2);
				children.Add(child);
			}
		}

		return dynamicObject;
	}
}
// << treedatagrid-dynamicobject-viewmodel
