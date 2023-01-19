using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ButtonControl.GettingStartedCategory.GettingStartedExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonGettingStartedXaml : RadContentView
    {
        int count = 0;
        public ButtonGettingStartedXaml()
        {
            InitializeComponent();
        }
        
        // >> button-getting-started-click-event
        private void button_Clicked(object sender, System.EventArgs e)
        {
            count++;

            if (count == 1)
                label1.Text = $"Clicked {count} time";
            else
                label1.Text = $"Clicked {count} times";
        }
        // << button-getting-started-click-event
    }
}