using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace SDKBrowserMaui.Examples.DataGridControl.DynamicDataCategory.DynamicObjectExample
{
    // >> datagrid-dynamicobject-model
    public class MyDynamicObject : DynamicObject, INotifyPropertyChanged
    {
        private int id;
        private readonly IDictionary<string, object> data = new Dictionary<string, object>();

        // CLR property 
        public int Id
        {
            get => this.id;
            set 
            {
                if (this.id != value)
                {
                    this.id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override IEnumerable<string> GetDynamicMemberNames() => data.Keys;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder.Name == nameof(this.Id))
            {
                result = this.Id;
            }
            else
            {
                result = this[binder.Name];
            }

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
                if (data.ContainsKey(columnName))
                {
                    return data[columnName];
                }
                return null;
            }
            set
            {
                if (!data.ContainsKey(columnName))
                {
                    data.Add(columnName, value);
                    OnPropertyChanged(columnName);
                }
                else
                {
                    if (data[columnName] != value)
                    {
                        data[columnName] = value;
                        OnPropertyChanged(columnName);
                    }
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    // << datagrid-dynamicobject-model
}
