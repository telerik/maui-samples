using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.Common;
using System;
using System.Linq;
using System.Reflection;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SettingsView : ContentView
{
    public SettingsView()
    {
        this.InitializeComponent();

        this.appearanceComboBox.ItemsSource = Enum.GetValues(typeof(AppAppearanceMode)).Cast<AppAppearanceMode>();
        this.appearanceComboBox.SelectedItem = AppAppearanceMode.Auto;

        var assemblyVersion = typeof(Telerik.Maui.Controls.RadBorder).GetTypeInfo().Assembly.GetName().Version.ToString();
        this.controlsVersionLabel.Text = assemblyVersion.Substring(0, assemblyVersion.Length - 2);
    }
}