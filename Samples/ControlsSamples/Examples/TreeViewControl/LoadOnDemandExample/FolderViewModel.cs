using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.TreeViewControl.LoadOnDemandExample;

public class FolderViewModel : FileViewModel
{
    public FolderViewModel()
    {
        this.Children = new ObservableCollection<FileViewModel>();
    }

    public IList<FileViewModel> Children { get; }
}
