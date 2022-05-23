using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.PopupControl.ContextMenuExample
{
    public class ContextMenuViewModel : ExampleViewModel
    {
        public ObservableCollection<FolderViewModel> Items { get; set; }

        public ContextMenuViewModel()
        {
            this.Items = new ObservableCollection<FolderViewModel>();
            for(int i = 1; i < 21; i++)
            {
                this.Items.Add(new FolderViewModel() { Name = "Item" + i, });
            }
            
        }
    }
}
