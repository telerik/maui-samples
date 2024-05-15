using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.Threading.Tasks;

namespace QSF.Examples.ButtonControl.CustomizationExample;

public class CustomizationViewModel : ExampleViewModel
{
    private const string LoadingButtonNormalText = "Load next";
    private const string LoadingButtonBusyText = "Loading";
    private Command loadingButtonCommand;
    private bool isLoading;
    private string loadingButtonText = LoadingButtonNormalText;

    public CustomizationViewModel()
    {
        this.LoadingButtonCommand = new Command(ExecuteLoadingCommand, CanExecuteLoadingCommand);
    }

     public Command LoadingButtonCommand
    {
        get => this.loadingButtonCommand;
        set => UpdateValue(ref this.loadingButtonCommand, value);
    }

    public bool IsLoading
    {
        get => this.isLoading;
        set
        {
            if (this.UpdateValue(ref this.isLoading, value))
            {
                this.LoadingButtonText = this.isLoading ? LoadingButtonBusyText : LoadingButtonNormalText;
                this.loadingButtonCommand.ChangeCanExecute();
            }
        }
    }

    public string LoadingButtonText
    {
        get => this.loadingButtonText;
        set => this.UpdateValue(ref this.loadingButtonText, value);
    }

    private async void ExecuteLoadingCommand()
    {
        this.IsLoading = true;
        await Task.Delay(TimeSpan.FromSeconds(3));
        this.IsLoading = false;
    }

    private bool CanExecuteLoadingCommand()
    {
        return !this.IsLoading;
    }
}
