using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.ConfigurationExample;

[XmlType("File")]
public class FileNode : ViewModelBase
{
    private string name;
    private string icon;

    public FileNode()
    {
        this.Icon = "\ue82c";
    }

    [XmlAttribute(AttributeName = "Name")]
    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    [XmlIgnore]
    public string Icon
    {
        get => this.icon;
        set => this.UpdateValue(ref this.icon, value);
    }
}