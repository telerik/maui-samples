using Microsoft.Maui.Controls;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.PromptControlledCategory.TemplatesExample;

public partial class Templates : ContentView
{
    private FlightsViewModel viewModel;
    public Templates()
    {
        InitializeComponent();

        this.viewModel = new FlightsViewModel();
        this.BindingContext = this.viewModel;
    }

    private void OnApplyPromptViewTemplateClicked(object sender, System.EventArgs e)
    {
        this.dataGrid.AIViewTemplate = this.Resources["AIViewTemplate"] as DataTemplate;
    }

    private void OnApplyEmptyPromptTemplateClicked(object sender, System.EventArgs e)
    {
        this.dataGrid.ClearValue(RadDataGrid.AIViewTemplateProperty);
        this.AISettings.IsSuggestedPromptsVisible = false;
        this.AISettings.IsRecentPromptsVisible = false;
        this.AISettings.EmptyContentTemplate = this.Resources["EmptyContentTemplate"] as DataTemplate;
    }
}