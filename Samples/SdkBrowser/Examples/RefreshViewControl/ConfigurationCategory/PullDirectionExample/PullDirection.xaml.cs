using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.RefreshView;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.RefreshViewControl.ConfigurationCategory.PullDirectionExample;

public partial class PullDirection : ContentView
{
    private int refreshCount;

    public PullDirection()
    {
        InitializeComponent();

        this.pullDirectionComboBox.ItemsSource = Enum.GetValues<RefreshViewPullDirection>();
        this.pullDirectionComboBox.SelectedItem = this.refreshView.PullDirection;
        this.pullDirectionComboBox.SelectionChanged += this.OnPullDirectionChanged;
    }

    private void OnPullDirectionChanged(object sender, ComboBoxSelectionChangedEventArgs e)
    {
        if (this.pullDirectionComboBox.SelectedItem is RefreshViewPullDirection direction)
        {
            this.refreshView.PullDirection = direction;
            this.hintLabel.Text = $"Pull {direction} to refresh.";
        }
    }

    // >> refreshview-pull-direction-onrefreshing
    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(1500);
        this.refreshCount++;
        this.statusLabel.Text = $"Refreshed {this.refreshCount} time(s)";
        this.refreshView.IsRefreshing = false;
    }
    // << refreshview-pull-direction-onrefreshing
}
