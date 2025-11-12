using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.Common;
using QSF.Helpers;
using QSF.Services;
using System;
using System.Linq;
using System.Reflection;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SettingsView : ContentView
{
    private TelemetryService telemetryService;

    public SettingsView()
    {
        this.InitializeComponent();
        this.telemetryService = ServiceHelper.GetService<TelemetryService>();
        this.sendUsageDataCheckBox.IsChecked = !this.telemetryService.IsDisabled;

        this.appearanceComboBox.ItemsSource = Enum.GetValues(typeof(AppAppearanceMode)).Cast<AppAppearanceMode>();
        this.appearanceComboBox.SelectedItem = AppAppearanceMode.Auto;

        var assemblyVersion = typeof(Telerik.Maui.Controls.RadBorder).GetTypeInfo().Assembly.GetName().Version.ToString();
        this.controlsVersionLabel.Text = assemblyVersion.Substring(0, assemblyVersion.Length - 2);
    }

    private void SendUsageDataCheckBox_IsCheckedChanged(object sender, Telerik.Maui.Controls.IsCheckedChangedEventArgs e)
    {
        this.telemetryService.IsDisabled = !this.sendUsageDataCheckBox.IsChecked.Value;
    }
}