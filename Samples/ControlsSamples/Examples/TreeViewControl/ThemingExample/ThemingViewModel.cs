using QSF.Examples.TreeViewControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace QSF.Examples.TreeViewControl.ThemingExample;

public class ThemingViewModel : ExampleViewModel
{
    private ObservableCollection<FolderNode> folders;

    public ThemingViewModel()
    {
        this.Folders = DataGenerator.GetItems<ObservableCollection<FolderNode>>(DataSourcePaths.FileExplorerPath);
    }

    [XmlArray("Folders")]
    [XmlArrayItem("Folder")]
    public ObservableCollection<FolderNode> Folders
    {
        get { return this.folders; }
        set { this.UpdateValue(ref this.folders, value); }
    }
}