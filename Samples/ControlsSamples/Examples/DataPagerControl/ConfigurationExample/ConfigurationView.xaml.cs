using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.DataPager;

namespace QSF.Examples.DataPagerControl.ConfigurationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ConfigurationView : ContentView
{
    public ConfigurationView()
    {
        InitializeComponent();
        this.ellipsisModeComboBox.SelectionChanged += OnEllipsisModeComboBoxSelectionChanged;
    }

    private void OnEllipsisModeComboBoxSelectionChanged(object sender, ComboBoxSelectionChangedEventArgs e)
    {
        if (sender is RadComboBox comboBox)
        {
            if ((DataPagerEllipsisMode)comboBox.SelectedItem != DataPagerEllipsisMode.None)
            {
                this.dataPager.PageIndex = 4;
            }
        }
    }
}