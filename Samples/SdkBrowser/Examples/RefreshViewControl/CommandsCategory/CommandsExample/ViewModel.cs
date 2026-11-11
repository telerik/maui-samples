using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.RefreshViewControl.CommandsCategory.CommandsExample;

// >> refreshview-commands-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private readonly string[] headlines =
    {
        "Telerik UI for .NET MAUI ships a new RefreshView",
        "Pull-to-refresh now works with any content",
        "Customize the refresh indicator with styles and templates",
    };

    private readonly Command refreshCommand;
    private bool isRefreshing;
    private bool canRefresh = true;
    private int refreshCount;
    private string headline;
    private string status = "Not refreshed yet";

    public ViewModel()
    {
        this.headline = this.headlines[0];
        this.refreshCommand = new Command(this.ExecuteRefresh, this.CanExecuteRefresh);
    }

    public ICommand RefreshCommand => this.refreshCommand;

    public bool IsRefreshing
    {
        get => this.isRefreshing;
        set => this.UpdateValue(ref this.isRefreshing, value);
    }

    public bool CanRefresh
    {
        get => this.canRefresh;
        set
        {
            if (this.UpdateValue(ref this.canRefresh, value))
            {
                this.refreshCommand.ChangeCanExecute();
            }
        }
    }

    public string Headline
    {
        get => this.headline;
        set => this.UpdateValue(ref this.headline, value);
    }

    public string Status
    {
        get => this.status;
        set => this.UpdateValue(ref this.status, value);
    }

    private bool CanExecuteRefresh(object parameter) => this.CanRefresh;

    private async void ExecuteRefresh(object parameter)
    {
        await Task.Delay(1500);
        this.refreshCount++;
        this.Headline = this.headlines[this.refreshCount % this.headlines.Length];
        this.Status = $"{parameter} refreshed {this.refreshCount} time(s)";
        this.IsRefreshing = false;
    }
}
// << refreshview-commands-viewmodel
