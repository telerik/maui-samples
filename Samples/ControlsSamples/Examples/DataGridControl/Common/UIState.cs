using Telerik.Maui.Controls;

namespace QSF.Examples.DataGridControl.Common;

public class UIState : NotifyPropertyChangedBase
{
    private int selectedTabIndex;

    public int SelectedTabIndex
	{
        get => this.selectedTabIndex;
        set => this.UpdateValue(ref this.selectedTabIndex, value);
    }
}