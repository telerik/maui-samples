using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.TreeViewControl
{
    // >> treeview-getting-started-viewmodel
    public class ViewModel
    {
        public ViewModel()
        {
            Items = new ObservableCollection<Item>();
            Items.Add(new Item("My Projects")
            {
                Children = new List<Item>()
                {
                    new Item("Using latest Telerik .NET MAUI controls")
                    {
                        Children = new ObservableCollection<Item>()
                        {
                            new Item("TreeView"),
                            new Item("Calendar"),
                            new Item("RichTextEditor"),
                            new Item("PdfViewer"),
                            new Item("SlideView"),
                            new Item("Chat"),
                        }
                    },
                    new Item("Blank project")
                }
            });
            Items.Add(new Item("Shared Documents")
            {
                Children = new List<Item>()
                {
                    new Item("Reports")
                    {
                        Children = new List<Item>()
                        {
                            new Item("October"),
                            new Item("November"),
                            new Item("December")
                        }
                    }
                }
            });
        }
        public ObservableCollection<Item> Items { get; set; }
    }
    // << treeview-getting-started-viewmodel
}
