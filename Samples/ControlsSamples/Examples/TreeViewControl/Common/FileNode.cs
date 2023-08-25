using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.Common;

[XmlType("File")]
public class FileNode : ViewModelBase
{
    private string name;
    private string icon;
    private string dateModified;
    private string size;

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

    [XmlAttribute(AttributeName = "DateModified")]
    public string DateModified
    {
        get => this.dateModified;
        set => this.UpdateValue(ref this.dateModified, value);
    }

    [XmlAttribute(AttributeName = "Size")]
    public string Size
    {
        get => this.size;
        set => this.UpdateValue(ref this.size, value);
    }
}