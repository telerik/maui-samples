using System.IO;
using System.Threading.Tasks;
using Telerik.Maui.Controls;

namespace QSF.Examples.ChatControl;

public class AttachedFileData : NotifyPropertyChangedBase
{
    private string name;
    private long size;
    private Func<Task<Stream>> getStream;
    private Guid guid;

    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public long Size
    {
        get => this.size;
        set => this.UpdateValue(ref this.size, value);
    }

    public Guid Guid
    {
        get => this.guid;
        set => this.UpdateValue(ref this.guid, value);
    }

    public Func<Task<Stream>> GetStream
    {
        get => this.getStream;
        set => this.UpdateValue(ref this.getStream, value);
    }
}
