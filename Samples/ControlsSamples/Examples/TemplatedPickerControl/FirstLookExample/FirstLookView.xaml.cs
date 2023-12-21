using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.Views;

namespace QSF.Examples.TemplatedPickerControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : PickerExampleViewBase
    {
        public FirstLookView()
        {
            InitializeComponent();

            this.ItemTemplate = (DataTemplate)this.Resources["ProductItemTemplate"];
        }
    }
}