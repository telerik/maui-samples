using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DropDownButtonControl.FeaturesCategory.CommandExample;

// >> dropdownbutton-command-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private string buttonText = "Click to open drop-down";
    private string statusText = "The Command has not been executed yet.";
    private int clickCount;

    public ViewModel()
    {
        this.ToggleCommand = new Microsoft.Maui.Controls.Command(this.OnToggleExecute);
    }

    public Microsoft.Maui.Controls.Command ToggleCommand { get; }

    public string ButtonText
    {
        get => this.buttonText;
        set => this.UpdateValue(ref this.buttonText, value);
    }

    public string StatusText
    {
        get => this.statusText;
        set => this.UpdateValue(ref this.statusText, value);
    }

    private void OnToggleExecute(object parameter)
    {
        this.clickCount++;
        this.ButtonText = $"Clicked {this.clickCount} time(s)";
        this.StatusText = $"Command executed. Click count: {this.clickCount}.";
    }
}
// << dropdownbutton-command-viewmodel
