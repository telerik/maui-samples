using Microsoft.Maui.Controls;
using QSF.Services;

namespace QSF.Examples.DataFormControl.CustomEditorsExample;

public partial class CustomEditorsView : ContentView
{
    public CustomEditorsView()
    {
        this.InitializeComponent();
    }

    private void OnSubmittedFeedbackButtonClicked(object sender, System.EventArgs e)
    {
        var toastService = DependencyService.Get<IToastMessageService>();
        toastService.ShortAlert("Your feedback was submitted. Thank you!");
    }
}
