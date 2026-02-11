using Microsoft.Maui.Controls;
using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls.DataGrid;

namespace QSF.Examples.DataGridControl.PromptControlledExample;

public class PromptControlledViewModel : ExampleViewModel
{
    private static readonly HttpClient HttpClient = new HttpClient();
    private CancellationTokenSource cancellationTokenSource;
    private ICommand processAICommand;
    private ICommand cancelAICommand;
    private ObservableCollection<string> suggestedPrompts;

    public PromptControlledViewModel()
    {
        this.suggestedPrompts = this.CreateDefaultSuggestedPrompts();

        this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
    }

    public ObservableCollection<Order> Orders { get; private set; }

    public ObservableCollection<string> SuggestedPrompts => this.suggestedPrompts;

    public ICommand ProcessAICommand
    {
        get => this.processAICommand ?? (this.processAICommand = new Command<DataGridPromptRequestCommandContext>(this.ExecuteProcessAI));
    }

    public ICommand CancelAICommand
    {
        get => this.cancelAICommand ?? (this.cancelAICommand = new Command(this.ExecuteCancelAI));
    }

    protected virtual ObservableCollection<string> CreateDefaultSuggestedPrompts()
    {
        return new ObservableCollection<string>
        {
            "Show Ships only from Germany and sort them by shipped date descending",
            "Filter by freight greater than $50.00",
            "Lock order date column and sort it descending",
            "Sort by city ascending",
            "Group by order date",
            "Clear all filters, sorting and grouping"
        };
    }

    private async void ExecuteProcessAI(DataGridPromptRequestCommandContext context)
    {
        if (this.cancellationTokenSource != null)
        {
            // An AI request is already being processed
            return;
        }

        this.cancellationTokenSource = new CancellationTokenSource();

        try
        {
            var request = JsonSerializer.Deserialize<object>(context.RequestJson);
            var requestResult = await HttpClient.PostAsJsonAsync("https://demos.telerik.com/service/v2/ai/grid/smart-state", request, this.cancellationTokenSource.Token);
            var response = await requestResult.Content.ReadAsStringAsync(this.cancellationTokenSource.Token);

            context.ResponseJson = response;
        }
        catch (OperationCanceledException)
        {
            // Cancellation was already handled by setting ProcessingState to Canceled
            // No need to set it again here
        }
        catch (Exception ex)
        {
            await this.ShowErrorAsync($"Failed to process request: {ex.Message}");
            context.HasError = true;
        }
        finally
        {
            this.cancellationTokenSource?.Dispose();
            this.cancellationTokenSource = null;
        }
    }

    private void ExecuteCancelAI()
    {
        this.cancellationTokenSource?.Cancel();
    }

    private async Task ShowErrorAsync(string message)
    {
#if NET10_0_OR_GREATER
        await Microsoft.Maui.Controls.Application.Current?.Windows[0].Page?.DisplayAlertAsync("Error", message, "OK");
#else
		await Microsoft.Maui.Controls.Application.Current?.Windows[0].Page?.DisplayAlert("Error", message, "OK");
#endif
    }
}
