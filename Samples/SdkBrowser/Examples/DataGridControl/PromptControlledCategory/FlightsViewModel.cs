using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.PromptControlledCategory;

// >> datagrid-prompt-viewmodel
public class FlightsViewModel
{
    private static readonly HttpClient HttpClient = new HttpClient();
    private CancellationTokenSource cancellationTokenSource;
    private ICommand processAICommand;
    private ICommand cancelAICommand;
    private ObservableCollection<string> suggestedPrompts;

    public FlightsViewModel()
    {
        this.suggestedPrompts = this.CreateDefaultSuggestedPrompts();

        this.Flights = new ObservableCollection<FlightInfo>();

        string[] companies = new[]
        {
            "Air Buzz",
            "North Airlines",
            "United Airlines",
            "Southwest Airlines",
            "East Airlines",
            "Air Cargo",
            "Pacific Airlines",
        };

        string[] cities = new[] 
        { 
            "Seattle",
            "New York",
            "London",
            "Paris",
            "Berlin",
            "Tokyo",
            "Sydney",
            "Toronto",
            "San Francisco",
            "Chicago"
        };

        for (int i = 0; i < 50; i++)
        {
            var company = companies[i % companies.Length];
            var fromIndex = i % cities.Length;
            var toIndex = (i + 3) % cities.Length;
            if (toIndex == fromIndex)
            {
                toIndex = (toIndex + 1) % cities.Length;
            }

            var departureTime = TimeSpan.FromHours(i % 24).Add(TimeSpan.FromMinutes((i * 5) % 60));
            var arrivalTime = departureTime.Add(TimeSpan.FromHours(1 + (i % 2)));

            int flightNumber = 1000 + i;

            this.Flights.Add(new FlightInfo
            {
                Company = company,
                FlightNumber = flightNumber,
                DepartureTime = departureTime,
                ArrivalTime = arrivalTime,
                From = cities[fromIndex],
                To = cities[toIndex]
            });
        }
    }

    public ObservableCollection<FlightInfo> Flights { get; private set; }

    public ObservableCollection<string> SuggestedPrompts => this.suggestedPrompts;

    public ICommand ProcessAICommand
    {
        get => this.processAICommand ?? (this.processAICommand = new Command<DataGridPromptRequestCommandContext>(this.ExecuteProcessAI));
    }

    public ICommand CancelAICommand
    {
        get => this.cancelAICommand ?? (this.cancelAICommand = new Command(this.ExecuteCancelAI));
    }

    private ObservableCollection<string> CreateDefaultSuggestedPrompts()
    {
        return new ObservableCollection<string>
        {
            "Group flights by destination column",
            "Filter flights that arrival time is before 9 AM",
            "Show flights which destination is Tokyo and Sydney",
            "Lock flight number column",
            "Reset all filters, grouping and sorting",
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
// << datagrid-prompt-viewmodel