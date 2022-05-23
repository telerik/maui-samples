using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DateTimePickerControl.FeaturesCategory.LocalizationExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Localization : RadContentView
    {
        public Localization()
        {
            // >> datetimepicker-localization-code-behind
            TelerikLocalizationManager.Manager = new CustomDateTimePickerLocalizationManager();
            // << datetimepicker-localization-code-behind
            InitializeComponent();
        }
    }
}