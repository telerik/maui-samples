using System.Collections.ObjectModel;
using System.Xml.Serialization;
using QSF.Examples.TreeViewControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.CustomizationExample;

public class CustomizationViewModel : ExampleViewModel
{
    private ObservableCollection<FolderNode> folders;

    public CustomizationViewModel()
    {
        this.Folders = DataGenerator.GetItems<ObservableCollection<FolderNode>>(DataSourcePaths.FavoriteFoldersPath);
    }

    [XmlArray("Folders")]
    [XmlArrayItem("Folder")]
    public ObservableCollection<FolderNode> Folders
    {
        get { return this.folders; }
        set { this.UpdateValue(ref this.folders, value); }
    }
}