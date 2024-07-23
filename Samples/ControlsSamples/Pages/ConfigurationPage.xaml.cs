using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using Telerik.Maui.Controls.Compatibility.DataControls;

namespace QSF.Pages;

public partial class ConfigurationPage : QSFPage
{
    public override Grid SafeAreaGridWithHeader => this.root;

    public override View FullScreenScrollView => this.scrollview;

#if IOS
    public override View Acrylic => this.acrylic;
#endif

    public override Grid ContentGrid => this.content;

    public ConfigurationPage()
    {
        this.InitializeComponent();
        this.BaseInitializeComponent();
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        ExampleViewModel vm = (ExampleViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}
