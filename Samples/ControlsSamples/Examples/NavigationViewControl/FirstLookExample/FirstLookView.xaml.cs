using System;
using System.Collections.Generic;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.NavigationView;

namespace QSF.Examples.NavigationViewControl.FirstLookExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstLookView : RadContentView
{
    private readonly List<string> itemImagesPool;
    private RadPopup popup;

    public FirstLookView()
    {
        InitializeComponent();

        this.itemImagesPool = new List<string>();

        for (int i = 1; i <= 12; i++)
        {
            this.itemImagesPool.Add($"navigationview_firstlook_image_{i}.png");
        }

        this.navigationView.SelectionChanged += this.NavigationViewSelectionChanged;
    }

    public IEnumerable<string> ImagePaths { get; set; }

    private void NavigationViewSelectionChanged(object sender, EventArgs e)
    {
        var itemIndex = this.navigationView.Items.IndexOf((NavigationViewItem)this.navigationView.SelectedItem);
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
        => Launcher.OpenAsync("https://docs.telerik.com/devtools/maui/controls/navigationview/overview");

    private void OnClosePopupBtnClicked(object sender, EventArgs e)
        => this.popup.IsOpen = false;
}
