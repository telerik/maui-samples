using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ThemingComboBoxView : RadBorder
{
    public ThemingComboBoxView()
    {
        this.InitializeComponent();
        themesCombo.SelectionChanged += this.ThemesCombo_SelectedIndexChanged;
    }

    private void ThemesCombo_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        var combo = sender as RadComboBox;
        bool wasEnabled = combo.IsEnabled;
        combo.IsEnabled = false;
        combo.IsEnabled = wasEnabled;
        combo.InvalidateMeasure();
    }
}