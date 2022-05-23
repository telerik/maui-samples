using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace QSF.Examples.ButtonControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : RadContentView
    {
        public FirstLookView()
        {
            InitializeComponent();

            //this.BindingContext = new TestViewModel();
        }
        
        private void button_Clicked(object sender, System.EventArgs e)
        {
        }
    }
}