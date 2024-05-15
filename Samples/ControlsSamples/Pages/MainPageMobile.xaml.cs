using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.ComponentModel;

namespace QSF.Pages;

public partial class MainPageMobile : ContentPage
{
    private int tapCounter;

    public MainPageMobile()
    {
        this.InitializeComponent();

        this.BindingContext = new HomeViewModel();
    }

    private void Settings_Clicked(object sender, EventArgs e)
    {
        HomeViewModel vm = (HomeViewModel)this.BindingContext;
        vm.NavigateToSettings();
    }

    private void Search_Clicked(object sender, EventArgs e)
    {
        HomeViewModel vm = (HomeViewModel)this.BindingContext;
        vm.NavigateToSearch();
    }

    private void controlsListView_ItemTapped(object sender, Telerik.Maui.Controls.Compatibility.DataControls.ListView.ItemTapEventArgs e)
    {
        HomeViewModel vm = (HomeViewModel)this.BindingContext;
        vm?.SelectControlCommand?.Execute(e.Item);
    }

    private void OnTitleLabelTapped(object sender, EventArgs e)
    {
        this.tapCounter++;
        if (this.tapCounter > 8)
        {
            ((HomeViewModel)this.BindingContext).IsTestSearchEntryVisible = true;
        }
    }
}
