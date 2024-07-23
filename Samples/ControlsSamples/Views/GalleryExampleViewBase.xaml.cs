using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.Collections.Specialized;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataControls;

namespace QSF.Views;

public partial class GalleryExampleViewBase : ContentView
{
    public static readonly BindableProperty HeaderContentProperty =
            BindableProperty.Create(nameof(HeaderContent), typeof(View), typeof(GalleryExampleViewBase), null, propertyChanged: OnHeaderContentPropertyChanged);

    public GalleryExampleViewBase()
    {
        this.InitializeComponent();
    }

    public View HeaderContent
    {
        get { return (View)GetValue(HeaderContentProperty); }
        set { SetValue(HeaderContentProperty, value); }
    }

    private static void OnHeaderContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        GalleryExampleViewBase example = (GalleryExampleViewBase)bindable;
        var headerPresenter = example.HeaderPresenter;
        headerPresenter.Content = (View)newValue;
    }

    internal void OnGallerySelectionChanged(object sender, Telerik.Maui.RadSelectionChangedEventArgs e)
    {
        var collectionView = (RadCollectionView)sender;
        var viewModel = collectionView.SelectedItem;
        if (viewModel != null)
        {
            this.presenter.Children.Clear();
            var galleryItemViewModel = (GalleryItemViewModelBase)viewModel;
            var view = this.GetGalleryItemView(galleryItemViewModel);
            view.BindingContext = viewModel;
            this.presenter.Children.Add(view);
        }
    }

    private View GetGalleryItemView(GalleryItemViewModelBase viewModel)
    {
        string resourceKey = viewModel.DataTemplateResourceKey;

        ResourceDictionary resources = this.Resources;

        if (resources == null || !resources.ContainsKey(resourceKey))
        {
            return null;
        }

        var template = (DataTemplate)resources[resourceKey];

        if (template == null)
        {
            throw new ArgumentException("Missing resource key: " + resourceKey);
        }

        return (View)template.CreateContent();
    }
}