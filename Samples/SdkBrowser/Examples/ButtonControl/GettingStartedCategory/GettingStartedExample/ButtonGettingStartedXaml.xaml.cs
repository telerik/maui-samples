using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ButtonControl.GettingStartedCategory.GettingStartedExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonGettingStartedXaml : RadContentView
    {
        public ButtonGettingStartedXaml()
        {
            InitializeComponent();
            this.button.AutomationId = "button";
        }
    }
}