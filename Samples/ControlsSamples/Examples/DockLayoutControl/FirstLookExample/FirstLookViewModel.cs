using QSF.Examples.DockLayoutControl.FirstLook;
using QSF.ViewModels;

namespace QSF.Examples.DockLayoutControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private DockLayoutConfigurationViewModel configurationViewModel = new DockLayoutConfigurationViewModel();

    public string TitlePanelDock => this.configurationViewModel.TitlePanelDock;

    public string ListPanelDock => this.configurationViewModel.ListPanelDock;

    public string NavigationPanelDock => this.configurationViewModel.NavigationPanelDock;

    public bool IsTitlePanelVisible => this.configurationViewModel.IsTitlePanelVisible;

    public bool IsListPanelVisible => this.configurationViewModel.IsListPanelVisible;

    public bool IsNavigationPanelVisible => this.configurationViewModel.IsNavigationPanelVisible;

    public bool IsContentPanelVisible => this.configurationViewModel.IsContentPanelVisible;
}