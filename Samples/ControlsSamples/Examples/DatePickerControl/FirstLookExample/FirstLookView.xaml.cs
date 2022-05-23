using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;
using QSF.Views;

namespace QSF.Examples.DatePickerControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : PickerExampleViewBase
    {
        public FirstLookView()
        {
            InitializeComponent();
            if (DeviceInfo.Idiom != DeviceIdiom.Phone)
            {
                this.datePicker1.MaximumWidthRequest = 300;
                this.datePicker2.MaximumWidthRequest = 300;
            }
        }
    }
}