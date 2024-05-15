using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ToggleButtonControl.FeaturesCategory.CommandExample;

// >> togglebutton-command-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private string myText = "Click to change content";

    public ViewModel()
    {
        this.MyToggleButtonCommand = new Microsoft.Maui.Controls.Command(this.CommandExecute, this.CommandCanExecute);
    }

    public Microsoft.Maui.Controls.Command MyToggleButtonCommand { get; set; }

    public string MyText
    {
        get => this.myText;
        set { this.UpdateValue(ref this.myText, value); }
    }

    private bool CommandCanExecute(object p)
    {
        return p == null || p is bool;
    }

    private void CommandExecute(object p)
    {
        var context = (bool?)p;
        if (context == true)
        {
            this.MyText = "Button is toggled";
            Application.Current.MainPage.DisplayAlert("", "Command is executed and ToggleButton is in toggled state", "OK");
        }
        else if (context == false)
        {
            this.MyText = "Button is untoggled";
            Application.Current.MainPage.DisplayAlert("", "Command is executed and ToggleButton is in untoggled state", "OK");
        }
        else 
        {
            this.MyText = "Indeterminate state";
            Application.Current.MainPage.DisplayAlert("", "Command is executed and ToggleButton is in indeterminate state", "OK"); 
        }
    }
}
// << togglebutton-command-viewmodel
