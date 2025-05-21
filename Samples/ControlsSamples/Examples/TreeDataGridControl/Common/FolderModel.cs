using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace QSF.Examples.TreeDataGridControl.Common;

[XmlType("Folder")]
public class FolderModel : ViewModelBase
{
	private string name;
	private DateTime creationDate;
	private string size;
	private string icon;
	private string type;

    public FolderModel()
	{
		this.Children = new ObservableCollection<FolderModel>();
	}

	[XmlAttribute(AttributeName = "Name")]
	public string Name
	{
		get => this.name;
		set => this.UpdateValue(ref this.name, value);
	}

	[XmlAttribute(AttributeName = "CreationDate")]
	public DateTime CreationDate
	{
		get => this.creationDate;
		set => this.UpdateValue(ref this.creationDate, value);
	}

	[XmlAttribute(AttributeName = "Size")]
	public string Size
	{
		get => this.size;
		set => this.UpdateValue(ref this.size, value);
	}

	[XmlAttribute(AttributeName = "Icon")]
	public string Icon
	{
		get => this.icon;
		set => this.UpdateValue(ref this.icon, value);
	}

    [XmlAttribute(AttributeName = "Type")]
    public string Type
    {
        get => this.type;
        set => this.UpdateValue(ref this.type, value);
    }

    [XmlArray("Children")]
	[XmlArrayItem("Folder")]
	public ObservableCollection<FolderModel> Children { get; private set; }
}
