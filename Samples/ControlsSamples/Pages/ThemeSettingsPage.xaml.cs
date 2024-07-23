using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;

namespace QSF.Pages;

public partial class ThemeSettingsPage : QSFPage
{
    public override Grid SafeAreaGridWithHeader => this.root;
#if IOS
    public override View Acrylic => this.acrylic;
#endif

    public override View FullScreenScrollView => this.listview;

    public override Grid ContentGrid => this.content;

    public ThemeSettingsPage()
	{
		this.InitializeComponent();
        this.BaseInitializeComponent();
	}

    private async void Back_Clicked(object sender, EventArgs e)
    {
        ThemeSettingsViewModel vm = (ThemeSettingsViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}