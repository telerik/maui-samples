using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;
using QSF.Views;

namespace QSF.Examples.ListPickerControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : PickerExampleViewBase
    {
        public FirstLookView()
        {
            this.InitializeComponent();
            if (DeviceInfo.Idiom != DeviceIdiom.Phone)
            {
                this.listPicker.MaximumWidthRequest = 300;
            }
        }
    }
}