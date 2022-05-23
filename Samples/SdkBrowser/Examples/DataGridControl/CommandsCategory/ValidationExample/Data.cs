using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SDKBrowserMaui.Examples.DataGridControl.CommandsCategory.ValidationExample;

// >> datagrid-commands-validation-businessobject
public class Data : INotifyDataErrorInfo, INotifyPropertyChanged
{
    private Dictionary<string, HashSet<object>> errors = new Dictionary<string, HashSet<object>>();
    private string country;

    public string Country
    {
        get
        {
            return this.country;
        }

        set
        {
            this.country = value;

            if (this.country.Length > 15)
            {
                this.AddError("Country", string.Format("Country too long", new DateTime()));
            }
            else
            {
                this.RemoveErrors("Country");
            }

            this.OnPropertyChanged("Country");
        }
    }

    public string Capital { get; set; }

    public bool HasErrors
    {
        get
        {
            return this.errors.Count > 0;
        }
    }

    public IEnumerable GetErrors(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return this.errors.SelectMany(c => c.Value);
        }

        HashSet<object> propertyErrors;

        this.errors.TryGetValue(propertyName, out propertyErrors);

        return propertyErrors ?? Enumerable.Empty<object>();
    }

    protected virtual void AddError(string propertyName, object errorMessage)
    {
        HashSet<object> propertyErrors;

        if (!this.errors.TryGetValue(propertyName, out propertyErrors))
        {
            propertyErrors = new HashSet<object>();
            this.errors.Add(propertyName, propertyErrors);

            propertyErrors.Add(errorMessage);

            this.OnErrorsChanged(propertyName);
        }
        else
        {
            if (!propertyErrors.Contains(errorMessage))
            {
                propertyErrors.Add(errorMessage);
                this.OnErrorsChanged(propertyName);
            }
        }
    }

    protected virtual void RemoveErrors(string propertyName = null)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            if (this.errors.Count > 0)
            {
                this.errors.Clear();
                this.OnErrorsChanged(propertyName);
            }
        }
        else
        {
            if (this.errors.Remove(propertyName))
            {
                this.OnErrorsChanged(propertyName);
            }
        }
    }

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    protected virtual void OnErrorsChanged(string propertyName)
    {
        if (this.ErrorsChanged != null)
        {
            this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
// << datagrid-commands-validation-businessobject