using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TemplatedButtonControl.FeaturesCategory.CommandExample;

// >> templatedbutton-command-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private string myText = "Click to change content";

    public ViewModel()
    {
        this.MyTemplatedButtonCommand = new Microsoft.Maui.Controls.Command(this.CommandExecute);
    }

    public Microsoft.Maui.Controls.Command MyTemplatedButtonCommand { get; set; }

    public string MyText 
    { 
        get => this.myText;
        set { this.UpdateValue(ref this.myText, value); }
    }

    private void CommandExecute(object p)
    {
        this.MyText = "Content Changed";
        Application.Current.Windows[0].Page.DisplayAlert("", "Command is executed and TemplatedButton Content is changed", "OK");
    }
}
// << templatedbutton-command-viewmodel
