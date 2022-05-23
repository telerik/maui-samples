using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using Telerik.Maui.MaskedEntry;

namespace SDKBrowserMaui.Examples.MaskedEntryControl.FeaturesCategory.ValueExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Value : RadContentView
    {
        public Value()
        {
            InitializeComponent();
        }

        private void ClearNumeric_Clicked(object sender, System.EventArgs e)
        {
            this.numericMaskedEntry.Value = null;
        }

        private void ChangeNumericValue_Clicked(object sender, System.EventArgs e)
        {
            // >> numericmaskedentry-value
            this.numericMaskedEntry.Value = 1024;
            // << numericmaskedentry-value
        }

        private void ChangeRegexValue_Clicked(object sender, System.EventArgs e)
        {
            this.regexMaskedEntry.Value = "1024";
        }

        private void ClearRegex_Clicked(object sender, System.EventArgs e)
        {
            // >> regexmaskedentry-value
            this.regexMaskedEntry.Value = null;
            // << regexmaskedentry-value
        }

        private void ChangeTextValue_Clicked(object sender, System.EventArgs e)
        {
            // >> textmaskedentry-value
            this.textMaskedEntry.Value = "Test";
            // << textmaskedentry-value
        }

        private void ClearText_Clicked(object sender, System.EventArgs e)
        {

            this.textMaskedEntry.Value = null;
        }
    }
}