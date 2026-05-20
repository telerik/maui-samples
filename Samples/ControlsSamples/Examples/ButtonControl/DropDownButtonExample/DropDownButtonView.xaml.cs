using System.Linq;
using Microsoft.Maui.Controls;
using QSF.Examples.ButtonControl.FirstLookExample;
using QSF.Services;
using Telerik.Maui;
using Telerik.Maui.Controls;

namespace QSF.Examples.ButtonControl.DropDownButtonExample;

public partial class DropDownButtonView : ContentView
{
    public DropDownButtonView()
    {
        this.InitializeComponent();
    }

    private void OnDropDownItemSelected(object sender, RadSelectionChangedEventArgs e)
    {
        if (e.AddedItems != null && e.AddedItems.Any())
        {
            var selectedItem = e.AddedItems.First() as MyData;
            if (selectedItem != null)
            {
                this.dropDownButton.IsOpen = false;

                var toastService = DependencyService.Get<IToastMessageService>();
                toastService.ShortAlert($"{selectedItem.Name} picked");

                ((RadCollectionView)sender).SelectedItem = null;
            }
        }
    }
}