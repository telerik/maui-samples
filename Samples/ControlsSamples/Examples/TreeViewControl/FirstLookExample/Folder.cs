using System.Collections.ObjectModel;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.FirstLookExample;

public class Folder : ViewModelBase
{
    private const string OpenFolderIcon = "\uE829";
    private const string ClosedFolderIcon = "\uE82A";

    private string name;
    private string icon;
    private bool isExpanded;
    private bool isLeaf;

    public Folder()
    {
        this.Folders = new ObservableCollection<Folder>();
        this.Emails = new ObservableCollection<Email>();
    }

    [XmlAttribute(AttributeName = "Name")]
    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    [XmlAttribute(AttributeName = "Icon")]
    public string Icon
    {
        get => this.icon;
        set => this.UpdateValue(ref this.icon, value);
    }

    [XmlIgnore]
    public bool IsExpanded
    {
        get => this.isExpanded;
        set
        {
            this.UpdateValue(ref this.isExpanded, value);

            if (this.isLeaf)
            {
                return;
            }

            this.Icon = this.isExpanded ? OpenFolderIcon : ClosedFolderIcon;
        }
    }

    [XmlIgnore]
    public bool IsLeaf
    {
        get => this.isLeaf;
        set => this.UpdateValue(ref this.isLeaf, value);
    }

    [XmlArray("Folders")]
    [XmlArrayItem("Folder")]
    public ObservableCollection<Folder> Folders { get; private set; }

    [XmlArray("Emails")]
    [XmlArrayItem("Email")]
    public ObservableCollection<Email> Emails { get; private set; }
}
