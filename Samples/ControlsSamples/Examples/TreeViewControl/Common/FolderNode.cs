using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace QSF.Examples.TreeViewControl.Common;

[XmlType("Folder")]
public class FolderNode : FileNode
{
    public FolderNode()
    {
        this.Children = new ObservableCollection<FileNode>();
    }

    [XmlArray("Children")]
    [XmlArrayItem(typeof(FolderNode), ElementName = "Folder"), XmlArrayItem(typeof(FileNode), ElementName = "File")]
    public ObservableCollection<FileNode> Children { get; private set; }
}