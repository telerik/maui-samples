using Microsoft.Maui.Controls;
using QSF.Services;

namespace QSF.Examples.DataFormControl.CustomizationExample;

public partial class CustomizationView : ContentView
{
    public CustomizationView()
    {
        this.InitializeComponent();
    }

    private void ReserveButton_Clicked(object sender, System.EventArgs e)
    {
        var toastService = DependencyService.Get<IToastMessageService>();
        toastService.ShortAlert("Reserving");
    }
}
