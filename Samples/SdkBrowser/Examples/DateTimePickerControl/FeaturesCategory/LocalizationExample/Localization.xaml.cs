using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DateTimePickerControl.FeaturesCategory.LocalizationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Localization : RadContentView
    {
        private ILocalizationManager manager;

        public Localization()
        {
            this.manager = TelerikLocalizationManager.Manager;
            // >> datetimepicker-localization-code-behind
            TelerikLocalizationManager.Manager = new CustomDateTimePickerLocalizationManager();
            // << datetimepicker-localization-code-behind
            InitializeComponent();
        }

        private void RadContentView_Unloaded(object sender, System.EventArgs e)
        {
            TelerikLocalizationManager.Manager = this.manager;
        }
    }
}