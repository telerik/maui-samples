using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace SDKBrowserMaui.Examples.CheckBoxControl.FeaturesCategory.CheckBoxCommandExample;

// >> checkbox-command-viewmodel
public class ViewModel
{
    public ViewModel()
    {
        this.MyCheckBoxCommand = new Command(this.CommandExecute, this.CommandCanExecute);
    }

    public ICommand MyCheckBoxCommand { get; set; }

    private bool CommandCanExecute(object p)
    {
        return true;
    }
    private void CommandExecute(object p)
    {
        var context = (bool)p;
        if (context == true)
        {
            Application.Current.Windows[0].Page.DisplayAlert("Message", "You have selected this option!", "OK");
        }
        else
        {
            Application.Current.Windows[0].Page.DisplayAlert("Message", "You have unselected the option", "OK");
        }
    }
}
// << checkbox-command-viewmodel
