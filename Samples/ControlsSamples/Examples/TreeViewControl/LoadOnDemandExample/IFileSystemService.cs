using System.Collections.Generic;
using System.Threading.Tasks;
using QSF.Examples.TreeViewControl.Common;

namespace QSF.Examples.TreeViewControl.LoadOnDemandExample;

public interface IFileSystemService
{
    Task<IEnumerable<FileNode>> FindFilesAsync(string filePath);
}
