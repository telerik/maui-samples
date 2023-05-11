using Microsoft.Maui.Controls;
using QSF.Services;

namespace QSF.Examples.DataFormControl.FirstLookExample
{
    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            this.InitializeComponent();
        }

        private void ReserveButton_Clicked(object sender, System.EventArgs e)
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Reserving");
        }
    }
}
