using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace SDKBrowserMaui.Examples.TreeDataGridControl.DynamicDataCategory.DynamicObjectExample;

// >> treedatagrid-dynamicobject-model
public class MyDynamicObject : DynamicObject, INotifyPropertyChanged
{
	readonly IDictionary<string, object> data;

	public MyDynamicObject()
	{
		this.data = new Dictionary<string, object>();
	}

	public MyDynamicObject(IDictionary<string, object> source)
	{
		this.data = source;
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public override IEnumerable<string> GetDynamicMemberNames()
		=> this.data.Keys;

	public override bool TryGetMember(GetMemberBinder binder, out object result)
	{
		result = this[binder.Name];
		return true;
	}

	public override bool TrySetMember(SetMemberBinder binder, object value)
	{
		this[binder.Name] = value;
		return true;
	}

	public object this[string columnName]
	{
		get
		{
			if (this.data.ContainsKey(columnName))
			{
				return this.data[columnName];
			}

			return null;
		}
		set
		{
			if (!this.data.ContainsKey(columnName))
			{
				this.data.Add(columnName, value);

				this.OnPropertyChanged(columnName);
			}
			else
			{
				if (this.data[columnName] != value)
				{
					this.data[columnName] = value;

					this.OnPropertyChanged(columnName);
				}
			}
		}
	}

	private void OnPropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
// << treedatagrid-dynamicobject-model
