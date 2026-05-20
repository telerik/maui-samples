using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.Services;
using Telerik.Maui;
using Telerik.Maui.Controls;

namespace QSF.Examples.ButtonControl.FirstLookExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstLookView : RadContentView
{
    public FirstLookView()
    {
        InitializeComponent();
    }

    private void OnDropDownItemSelected(object sender, RadSelectionChangedEventArgs e)
    {
        this.HandleDropDownSelection(sender, e, this.dropDownButton);
    }

    private void OnIconTextDropDownItemSelected(object sender, RadSelectionChangedEventArgs e)
    {
        this.HandleDropDownSelection(sender, e, this.iconTextDropDownButton);
    }

    private void OnIconDropDownItemSelected(object sender, RadSelectionChangedEventArgs e)
    {
        this.HandleDropDownSelection(sender, e, this.iconDropDownButton);
    }

    private void HandleDropDownSelection(object sender, RadSelectionChangedEventArgs e, RadDropDownButton dropDown)
    {
        if (e.AddedItems != null && e.AddedItems.Any())
        {
            var selectedItem = e.AddedItems.First() as MyData;
            if (selectedItem != null)
            {
                dropDown.IsOpen = false;

                var toastService = DependencyService.Get<IToastMessageService>();
                toastService.ShortAlert($"{selectedItem.Name} picked");

                ((RadCollectionView)sender).SelectedItem = null;
            }
        }
    }
}
