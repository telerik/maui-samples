using Microsoft.Maui.Controls;

namespace QSF.Examples.AutoCompleteControl.CustomizationExample
{
    public partial class CustomizationView : Grid
    {
        public CustomizationView()
        {
            this.InitializeComponent();
        }

        private void TokenCloseButton_Tapped(object sender, System.EventArgs e)
        {
            if (sender is Label closeLabel)
            {
                if (closeLabel.BindingContext is JobTitle item)
                {
                    this.autoComplete.Tokens.Remove(item);
                }
            }
        }
    }
}
