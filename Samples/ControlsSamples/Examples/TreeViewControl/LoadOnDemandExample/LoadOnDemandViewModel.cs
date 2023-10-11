using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using QSF.Examples.TreeViewControl.Common;
using QSF.ViewModels;
using Telerik.Maui.Controls.TreeView;

namespace QSF.Examples.TreeViewControl.LoadOnDemandExample;

public class LoadOnDemandViewModel : ExampleViewModel
{
    private readonly IFileSystemService fileSystemService;
    private bool isBusy;

    public LoadOnDemandViewModel()
        : this(new FileSystemService())
    {
    }

    public LoadOnDemandViewModel(IFileSystemService fileSystemService)
    {
        this.fileSystemService = fileSystemService;
        this.RootItems = new ObservableCollection<FileViewModel>();
        this.LoadFilesCommand = new Command<TreeViewLoadChildrenOnDemandCommandContext>(this.LoadFilesAsync);

        this.LoadFilesAsync();
    }

    public bool IsBusy
    {
        get => this.isBusy;
        private set => this.UpdateValue(ref this.isBusy, value);
    }

    public IList<FileViewModel> RootItems { get; }

    public ICommand LoadFilesCommand { get; }

    private async void LoadFilesAsync()
    {
        this.IsBusy = true;

        var fileNodes = await this.fileSystemService.FindFilesAsync(string.Empty);

        foreach (var fileNode in fileNodes)
        {
            var fileViewModel = this.CreateViewModel(fileNode);

            this.RootItems.Add(fileViewModel);
        }

        this.IsBusy = false;
    }

    private async Task LoadFilesAsync(FolderViewModel folderViewModel)
    {
        var filePath = this.GetFilePath(folderViewModel);
        var fileNodes = await this.fileSystemService.FindFilesAsync(filePath);

        foreach (var fileNode in fileNodes)
        {
            var fileViewModel = this.CreateViewModel(fileNode);

            fileViewModel.Parent = folderViewModel;
            folderViewModel.Children.Add(fileViewModel);
        }
    }

    private async void LoadFilesAsync(TreeViewLoadChildrenOnDemandCommandContext commandContext)
    {
        if (commandContext.Item is FolderViewModel folderViewModel)
        {
            await this.LoadFilesAsync(folderViewModel);
        }

        commandContext.Finish();
    }

    private string GetFilePath(FileViewModel fileViewModel)
    {
        var filePath = string.Empty;

        while (fileViewModel is not null)
        {
            var fileName = fileViewModel.Name;

            fileName = fileName.Replace(Path.DirectorySeparatorChar, '-');
            fileName = fileName.Replace(Path.AltDirectorySeparatorChar, '-');
            filePath = Path.Combine(fileName, filePath);
            fileViewModel = fileViewModel.Parent;
        }

        return filePath;
    }

    private FileViewModel CreateViewModel(FileNode fileNode)
    {
        if (fileNode is FolderNode folderNode)
        {
            return this.CreateFolderViewModel(folderNode);
        }

        return this.CreateFileViewModel(fileNode);
    }

    private FolderViewModel CreateFolderViewModel(FolderNode folderNode)
    {
        return new FolderViewModel
        {
            Name = folderNode.Name,
            Icon = folderNode.Icon
        };
    }

    private FileViewModel CreateFileViewModel(FileNode fileNode)
    {
        return new FileViewModel
        {
            Name = fileNode.Name,
            Icon = fileNode.Icon
        };
    }
}
