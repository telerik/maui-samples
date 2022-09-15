using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels;
using System;
using System.Linq;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.FeaturesCategory.RemoteSearchExample;

public partial class RemoteSearch : ContentView
{
    private ClientsViewModel viewModel;
    private string currentText;
    private bool isRemoteSearchRunning;
    public RemoteSearch()
	{
		InitializeComponent();
        this.viewModel = new ClientsViewModel();
        this.BindingContext = viewModel;
    }

    // >> autocomplete-remote-search-alorithm
	private void OnTextChanged(object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
	{
        var autoComplete = (RadAutoComplete)sender;
        autoComplete.ItemsSource = null;

        this.currentText = e.NewTextValue ?? "";

        if (this.currentText.Length >= autoComplete.SearchThreshold && !this.isRemoteSearchRunning)
        {
            this.isRemoteSearchRunning = true;
            Dispatcher.StartTimer(TimeSpan.FromMilliseconds(1500), () =>
            {
                this.isRemoteSearchRunning = false;
                string searchText = this.currentText.ToLower();
                autoComplete.ItemsSource = this.viewModel.Source.Where(i => i.Name.ToLower().Contains(searchText));
                return false;
            });
        }
    }
    // << autocomplete-remote-search-alorithm
}