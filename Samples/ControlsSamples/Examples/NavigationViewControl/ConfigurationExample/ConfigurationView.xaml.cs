using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.NavigationView;

namespace QSF.Examples.NavigationViewControl.ConfigurationExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ConfigurationView : ContentView
{
    private readonly List<string> itemImagesPool = new List<string>();
    private RadPopup popup;

    public ConfigurationView()
	{
        this.InitImagesPool();
		InitializeComponent();
        this.navigationView.SelectedItem = this.navigationView.Items[0];
    }

    private void InitImagesPool()
    {
        for (int i = 1; i <= 12; i++)
        {
            this.itemImagesPool.Add($"navigationview_firstlook_image_{i}.png");
        }
    }

    private void NavigationViewSelectionChanged(object sender, EventArgs e)
    {
        RadNavigationView navigationView = (RadNavigationView)sender;
        var itemIndex = navigationView.Items.IndexOf((NavigationViewItem)navigationView.SelectedItem);
        var itemIndexMultiplier = itemIndex * 3;

        if (itemIndex != -1 && (itemIndexMultiplier + 2 < this.itemImagesPool.Count))
        {
            this.itemImage1.Source = this.itemImagesPool[itemIndexMultiplier];
            this.itemImage2.Source = this.itemImagesPool[itemIndexMultiplier + 1];
            this.itemImage3.Source = this.itemImagesPool[itemIndexMultiplier + 2];
        }
    }

    private void SettingsItemClicked(object sender, EventArgs e)
    {
        if (this.popup == null)
        {
            this.popup = new RadPopup
            {
                IsModal = true,
                Placement = PlacementMode.Center,
                ContentTemplate = (DataTemplate)this.Resources["SettingsPopupContentTemplate"]
            };
        }

        this.popup.IsOpen = true;
    }

    private void OnHyperlinkTapped(object sender, TappedEventArgs e)
        => Launcher.OpenAsync("https://www.telerik.com/maui-ui/documentation/controls/navigationview/overview");

    private void OnClosePopupBtnClicked(object sender, EventArgs e)
        => this.popup.IsOpen = false;
}