using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.RefreshView;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.RefreshViewControl.ConfigurationCategory.IndicatorTypeExample;

public partial class IndicatorType : ContentView
{
    private int refreshCount;

    public IndicatorType()
    {
        InitializeComponent();

        this.indicatorTypeComboBox.ItemsSource = Enum.GetValues<RefreshViewIndicatorType>();
        this.indicatorTypeComboBox.SelectedItem = this.refreshView.IndicatorType;
        this.indicatorTypeComboBox.SelectionChanged += OnIndicatorTypeChanged;
    }

    private void OnIndicatorTypeChanged(object sender, ComboBoxSelectionChangedEventArgs e)
    {
        if (this.indicatorTypeComboBox.SelectedItem is RefreshViewIndicatorType type)
        {
            this.refreshView.IndicatorType = type;
        }
    }

    // >> refreshview-indicator-type-onrefreshing
    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Task.Delay(1500);
        this.refreshCount++;
        this.statusLabel.Text = $"Refreshed {this.refreshCount} time(s) using {this.refreshView.IndicatorType} indicator";
        this.refreshView.IsRefreshing = false;
    }
    // << refreshview-indicator-type-onrefreshing
}
