﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.ViewModels;
using System.Runtime.CompilerServices;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SearchViewDesktop : ContentView
{
    public static readonly BindableProperty SelectedSearchResultProperty = BindableProperty.Create(
        nameof(SelectedSearchResult), typeof(HighlightedSearchResult), typeof(SearchViewDesktop), defaultBindingMode: BindingMode.TwoWay);

    public SearchViewDesktop()
    {
        this.InitializeComponent();
    }

    public HighlightedSearchResult SelectedSearchResult
    {
        get { return (HighlightedSearchResult)this.GetValue(SelectedSearchResultProperty); }
        set { this.SetValue(SelectedSearchResultProperty, value); }
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == nameof(this.IsVisible))
        {
            if (this.IsVisible && this.searchEntry != null)
            {
                this.searchEntry.Text = null;
                this.searchEntry.Focus();
            }
        }
    }
}