using QSF.Examples.TreeDataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace QSF.Examples.TreeDataGridControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
	private ObservableCollection<FolderModel> folders;

	public FirstLookViewModel()
	{
		this.Folders = DataGenerator.GetItems<ObservableCollection<FolderModel>>(DataSourcePaths.FolderDataSource);
	}

	public ObservableCollection<FolderModel> Folders { get; set; }
}