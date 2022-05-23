using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace QSF.Examples.DockLayoutControl.FirstLook;

public class DockLayoutConfigurationViewModel : NotifyPropertyChangedBase
{
    private string titlePanelDock = "Top";
    private string listPanelDock = "Left";
    private string navigationPanelDock = "Bottom";
    private bool isTitlePanelVisible = true;
    private bool isListPanelVisible = true;
    private bool isNavigationPanelVisible = true;
    private bool isContentPanelVisible = true;

    public string TitlePanelDock
    {
        get
        {
            return this.titlePanelDock;
        }
        set
        {
            this.UpdateValue(ref this.titlePanelDock, value);
        }
    }

    public string ListPanelDock
    {
        get
        {
            return this.listPanelDock;
        }
        set
        {
            this.UpdateValue(ref listPanelDock, value);
        }
    }

    public string NavigationPanelDock
    {
        get
        {
            return this.navigationPanelDock;
        }
        set
        {
            this.UpdateValue(ref navigationPanelDock, value);
        }
    }

    public bool IsTitlePanelVisible
    {
        get
        {
            return this.isTitlePanelVisible;
        }
        set
        {
            this.UpdateValue(ref isTitlePanelVisible, value);
        }
    }

    public bool IsListPanelVisible
    {
        get
        {
            return this.isListPanelVisible;
        }
        set
        {
            this.UpdateValue(ref isListPanelVisible, value);
        }
    }

    public bool IsNavigationPanelVisible
    {
        get
        {
            return this.isNavigationPanelVisible;
        }
        set
        {
            this.UpdateValue(ref isNavigationPanelVisible, value);
        }
    }

    public bool IsContentPanelVisible
    {
        get
        {
            return this.isContentPanelVisible;
        }
        set
        {
            this.UpdateValue(ref isContentPanelVisible, value);
        }
    }

    public List<string> DockPickerOptions { get; set; } = new List<string> { "Top", "Bottom", "Left", "Right" };
}