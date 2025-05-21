using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.TreeDataGridControl;

// >> treedatagrid-viewmodel
public class ViewModel
{
	public ViewModel()
	{
		Items = new ObservableCollection<Data>();
		Items.Add(new Data("My Projects", 234 ,"Folder")
		{
			Children = new ObservableCollection<Data>()
				{
					new Data("Using latest Telerik .NET MAUI controls", 234 ,"Folder")
					{
						Children = new ObservableCollection<Data>()
						{
							new Data("TreeDataGrid", 6, "File"),
							new Data("CollectionView", 6, "File"),
							new Data("DataGrid", 54, "File"),
							new Data("Scheduler", 12, "File"),
							new Data("TreeView", 2, "File"),
							new Data("Calendar", 23, "File"),
							new Data("RichTextEditor", 0, "File"),
							new Data("PdfViewer", 55, "File"),
							new Data("ToggleButton", 21, "File"),
							new Data("TemplatedButton", 88, "File"),
						}
					},
					new Data("Blank project", 0, "Folder")
				}
		});
		Items.Add(new Data("Shared Documents", 643, "Folder")
		{
			Children = new ObservableCollection<Data>()
				{
					new Data("Reports", 643, "Folder")
					{
						Children = new ObservableCollection<Data>()
						{
							new Data("October", 234, "File"),
							new Data("November", 0, "File"),
							new Data("December", 409, "File")
						}
					}
				}
		});
		Items.Add(new Data("Pictures", 643, "Folder")
		{
			Children = new ObservableCollection<Data>()
				{
					new Data("Camera Roll", 231, "Folder")
					{
						Children = new ObservableCollection<Data>()
						{
							new Data("hello.png", 107, "File"),
							new Data("happy_summer.png", 0, "File"),
							new Data("avatar.png", 124, "File")
						}
					},
					new Data("Saved Pictures", 453, "Folder")
					{
						Children = new ObservableCollection<Data>()
						{
							new Data("vacation.png", 234, "File"),
							new Data("november.png", 0, "File"),
							new Data("mountains.png", 227, "File")
						}
					}
				}
		});
		Items.Add(new Data("Documents", 876, "Folder")
		{
			Children = new ObservableCollection<Data>()
				{
					new Data("Internal Usage Only", 643, "Folder")
					{
						Children = new ObservableCollection<Data>()
						{
							new Data("reports.docx", 234, "File"),
							new Data("confidential.xlsx", 0, "File"),
							new Data("internal_usage.pdf", 409, "File")
						}
					}
				}
		});
	}
	public ObservableCollection<Data> Items { get; set; }
}
// << treedatagrid-viewmodel
