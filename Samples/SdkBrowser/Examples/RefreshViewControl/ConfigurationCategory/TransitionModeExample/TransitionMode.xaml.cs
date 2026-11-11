using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.RefreshView;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.RefreshViewControl.ConfigurationCategory.TransitionModeExample;

public partial class TransitionMode : ContentView
{
    private int refreshCount;

    public TransitionMode()
    {
        InitializeComponent();

        this.transitionModeComboBox.ItemsSource = Enum.GetValues<RefreshViewTransitionMode>();
        this.transitionModeComboBox.SelectedItem = this.refreshView.TransitionMode;
        this.transitionModeComboBox.SelectionChanged += this.OnTransitionModeChanged;
    }

    private void OnTransitionModeChanged(object sender, ComboBoxSelectionChangedEventArgs e)
    {
        if (this.transitionModeComboBox.SelectedItem is RefreshViewTransitionMode mode)
        {
            this.refreshView.TransitionMode = mode;
        }
    }

    // >> refreshview-transition-mode-onrefreshing
    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(1500);
        this.refreshCount++;
        this.statusLabel.Text = $"Refreshed {this.refreshCount} time(s) using {this.refreshView.TransitionMode} transition";
        this.refreshView.IsRefreshing = false;
    }
    // << refreshview-transition-mode-onrefreshing
}
