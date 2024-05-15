using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TemplatedButtonControl.FeaturesCategory.LoadingButtonExample;

// >> loadingbutton-viewmodel
public class LoadingButtonViewModel : NotifyPropertyChangedBase
{
    private const string NormalText = "Click to update data";
    private const string LoadingText = "Updating data";
    private Command command;
    private bool isLoading;
    private string text = NormalText;

    public LoadingButtonViewModel()
    {
        this.Command = new Command(ExecuteCommand, CanExecuteCommand);
    }

    public Command Command
    {
        get => this.command;
        set => UpdateValue(ref this.command, value);
    }

    public bool IsLoading
    {
        get => this.isLoading;
        set
        {
            if (this.UpdateValue(ref this.isLoading, value))
            {
                this.Text = this.isLoading ? LoadingText : NormalText;
                this.command.ChangeCanExecute();
            }
        }
    }

    public string Text
    {
        get => this.text;
        set => this.UpdateValue(ref this.text, value);
    }

    private async void ExecuteCommand()
    {
        this.IsLoading = true;
        await Task.Delay(TimeSpan.FromSeconds(5));
        this.IsLoading = false;
    }

    private bool CanExecuteCommand()
    {
        return !this.IsLoading;
    }
}
// << loadingbutton-viewmodel