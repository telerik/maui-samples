using Telerik.Maui.Controls;

namespace QSF.Examples.ChatControl;

public class AttachmentData : NotifyPropertyChangedBase
{
    private string name;
    private long size;
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
}
