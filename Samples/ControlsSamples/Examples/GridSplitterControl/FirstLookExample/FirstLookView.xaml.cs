using Microsoft.Maui.Controls;

namespace QSF.Examples.GridSplitterControl.FirstLookExample;

public partial class FirstLookView : ContentView
{
    public FirstLookView()
    {
        InitializeComponent();
    }

    private void PropertyItemsCollectionViewSelectionChanged(object sender, Telerik.Maui.RadSelectionChangedEventArgs e)
    {
        if (this.BindingContext is FirstLookViewModel viewModel)
        {
            viewModel.ImageDetails = viewModel.ImageDetailsDictionary[viewModel.SelectedImage];
        }
    }
}