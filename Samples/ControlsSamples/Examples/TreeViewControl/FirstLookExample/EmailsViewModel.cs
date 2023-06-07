using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.FirstLookExample;

public class EmailsViewModel : ExampleViewModel
{
    public EmailsViewModel(Folder folder)
    {
        this.Folder = folder;
    }

    public Folder Folder { get; private set; }
}