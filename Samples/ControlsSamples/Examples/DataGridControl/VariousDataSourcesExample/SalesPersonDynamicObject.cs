using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace QSF.Examples.DataGridControl.VariousDataSourcesExample;

public class SalesPersonDynamicObject : DynamicObject, INotifyPropertyChanged
{
    private readonly IDictionary<string, object> dictionary;

    public SalesPersonDynamicObject()
    {
        this.dictionary = new Dictionary<string, object>();
    }

    public SalesPersonDynamicObject(IDictionary<string, object> source)
    {
        this.dictionary = source;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public object this[string propertyName]
    {
        get
        {
            return this.dictionary.ContainsKey(propertyName) ? this.dictionary[propertyName] : null;
        }
        set
        {
            if (!this.dictionary.ContainsKey(propertyName))
            {
                this.dictionary.Add(propertyName, value);
                this.OnPropertyChanged(propertyName);
            }
            else
            {
                if (this.dictionary[propertyName] != value)
                {
                    this.dictionary[propertyName] = value;
                    this.OnPropertyChanged(propertyName);
                }
            }
        }
    }

    public override IEnumerable<string> GetDynamicMemberNames()
           => this.dictionary.Keys;

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        return this.dictionary.TryGetValue(binder.Name, out result);
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        this.dictionary[binder.Name] = value;
        return true;
    }

    private void OnPropertyChanged(string propertyName)
        => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
