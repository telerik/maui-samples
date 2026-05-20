using Microsoft.Maui.Controls.Xaml;
using System.Linq;
using Telerik.Maui.Controls;

namespace QSF.Examples.ButtonControl.CustomizationExample;

public partial class CustomizationView : RadContentView
{
    public CustomizationView()
    {
        InitializeComponent();
    }

    private void OnCollectionViewSelectionChanged(object sender, Telerik.Maui.RadSelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count() > 0)
        {
            this.dropDownButton.IsOpen = false;
        }
    }
}
