using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Telerik.Maui.Controls;

namespace QSF.Examples.TreeViewControl.ConfigurationExample;

[XmlType("Folder")]
public class FolderNode : FileNode
{
    public FolderNode()
    {
        this.Children = new ObservableCollection<FileNode>();
        this.Icon = "\ue82a";
    }

    [XmlArray("Children")]
    [XmlArrayItem(typeof(FolderNode), ElementName = "Folder"), XmlArrayItem(typeof(FileNode), ElementName = "File")]
    public ObservableCollection<FileNode> Children { get; private set; }
}