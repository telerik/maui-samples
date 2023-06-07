using System.Collections.ObjectModel;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.FirstLookExample;

public class Account : ViewModelBase
{
    private string name;
    private string icon;
    private string address;
    private ObservableCollection<Folder> folders;

    public Account()
    {
        this.Folders = new ObservableCollection<Folder>();
    }

    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public string Icon
    {
        get => this.icon;
        set => this.UpdateValue(ref this.icon, value);
    }

    public string Address
    {
        get => this.address;
        set => this.UpdateValue(ref this.address, value);
    }

    [XmlArray("Folders")]
    [XmlArrayItem("Folder")]
    public ObservableCollection<Folder> Folders 
    {
        get => this.folders;
        set => this.UpdateValue(ref this.folders, value);
    }
}
