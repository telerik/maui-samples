using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;

namespace QSF.Pages;

public partial class SettingsPageMobile : QSFPage
{
    public override Grid SafeAreaGridWithHeader => this.root;

#if IOS
    public override View Acrylic => this.acrylic;
#endif

    public override Grid ContentGrid => this.content;

    public SettingsPageMobile()
    {
        this.InitializeComponent();
        this.BaseInitializeComponent();
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        SettingsViewModel vm = (SettingsViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}
