using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;
using QSF.Views;

namespace QSF.Examples.TemplatedPickerControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : PickerExampleViewBase
    {
        public FirstLookView()
        {
            InitializeComponent();
            if (DeviceInfo.Idiom != DeviceIdiom.Phone)
            {
                this.templatedPicker.MaximumWidthRequest = 300;
                this.ItemTemplate = (DataTemplate)this.Resources["VerticalItemTemplate"];
            }
            else
            {
                this.ItemTemplate = (DataTemplate)this.Resources["HorizontalItemTemplate"];
            }
        }
    }
}