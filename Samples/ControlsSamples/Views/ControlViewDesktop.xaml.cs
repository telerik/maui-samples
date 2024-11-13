using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.Common;
using QSF.ViewModels;
using Telerik.Maui.Controls;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ControlViewDesktop : ContentView
{
    private ThemeSettingsViewModel themeViewModel;

    public ControlViewDesktop()
    {
        this.InitializeComponent();

        this.themeViewModel = new ThemeSettingsViewModel() { MergedDictionaries = this.exampleContainer.Resources.MergedDictionaries };
        this.themesCombo.BindingContext = this.themeViewModel;

        this.Unloaded += this.OnUnloaded;
    }

    private void ExampleContainerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RadContentView.Content))
        {
            var item = this.examplesList.SelectedItem as Example;
            if (item != null && !item.IsThemable)
            {
                this.themeViewModel.CurrentTheme = this.themeViewModel.ThemesCatalog[0];
            }
        }
    }

    private void OnUnloaded(object sender, EventArgs e)
    {
        this.Unloaded -= this.OnUnloaded;
        this.themeViewModel.CurrentTheme = this.themeViewModel.ThemesCatalog[0];
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        this.themeViewModel.ParentViewModel = (ControlViewModel)this.BindingContext;
    }
}
