using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Datasync.Client;

namespace TelerikCRM.Maui.Models.DataService;

public abstract class ServiceModelBase<T> : DatasyncClientData, IEquatable<T>, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    // Requirement for DatasyncClientData and replaces BaseDataObject
    public abstract bool Equals(T other);

    public abstract T Copy();

    public abstract void CopyFrom(T other);

    protected bool SetProperty<P>(ref P backingStore, P value,[CallerMemberName]string propertyName = "", Action onChanged = null)
    {
        if (EqualityComparer<P>.Default.Equals(backingStore, value))
        {
            return false;
        }

        backingStore = value;
        onChanged?.Invoke();
        this.OnPropertyChanged(propertyName);

        return true;
    }

    protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}