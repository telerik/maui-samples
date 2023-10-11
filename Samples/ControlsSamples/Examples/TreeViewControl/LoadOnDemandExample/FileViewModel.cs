using Telerik.Maui.Controls;

namespace QSF.Examples.TreeViewControl.LoadOnDemandExample;

public class FileViewModel : NotifyPropertyChangedBase
{
    private FolderViewModel parent;
    private string name;
    private string icon;

    public FolderViewModel Parent
    {
        get => this.parent;
        set => this.UpdateValue(ref this.parent, value);
    }

    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public string Icon
    {
        get => this.icon;
        set => this.UpdateValue(ref this.icon, value);
    }
}
