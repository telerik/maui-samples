using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QSF.Examples.TreeViewControl.Common;
using QSF.ExampleUtilities;

namespace QSF.Examples.TreeViewControl.LoadOnDemandExample;

public class FileSystemService : IFileSystemService
{
    private static readonly TimeSpan delayInterval = TimeSpan.FromSeconds(2);

    private readonly IEnumerable<FileNode> rootNodes;

    public FileSystemService()
    {
        this.rootNodes = DataGenerator.GetItems<FolderNode[]>(DataSourcePaths.LoadOnDemandPath);
    }

    public Task<IEnumerable<FileNode>> FindFilesAsync(string filePath)
    {
        return Task.Delay(delayInterval).ContinueWith(_ => this.FindFiles(filePath));
    }

    private IEnumerable<FileNode> FindFiles(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            return this.rootNodes;
        }

        var pathSegments = filePath.Split(Path.DirectorySeparatorChar,
                                          Path.AltDirectorySeparatorChar);

        return this.FindFiles(this.rootNodes, pathSegments);
    }

    private IEnumerable<FileNode> FindFiles(IEnumerable<FileNode> fileNodes, IEnumerable<string> pathSegments)
    {
        foreach (var pathSegment in pathSegments)
        {
            fileNodes = this.FindFiles(fileNodes, pathSegment);
        }

        return fileNodes;
    }

    private IEnumerable<FileNode> FindFiles(IEnumerable<FileNode> fileNodes, string pathSegment)
    {
        foreach (var fileNode in fileNodes)
        {
            if (fileNode is FolderNode folderNode)
            {
                var folderName = folderNode.Name;

                folderName = folderName.Replace(Path.DirectorySeparatorChar, '-');
                folderName = folderName.Replace(Path.AltDirectorySeparatorChar, '-');

                if (folderName == pathSegment)
                {
                    return folderNode.Children;
                }
            }
        }

        return Enumerable.Empty<FileNode>();
    }
}
