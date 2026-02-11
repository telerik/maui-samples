using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Examples.DataGridControl.PromptControlledCategory.ConfigurationExample;

public partial class Configuration : ContentView
{
    // >> datagrid-promptrequest-handling-view
    private static readonly HttpClient HttpClient = new HttpClient();
    private CancellationTokenSource cancellationTokenSource;
    private ObservableCollection<string> suggestedPrompts;
    private ObservableCollection<string> recentPrompts;
    private readonly ObservableCollection<Person> people = new();
    // << datagrid-promptrequest-handling-view

    // Sample data for the grid (no ViewModel)
    public Configuration()
    {
        InitializeComponent();

        // >> datagrid-aiprompt-config-suggested-prompts-collection
        this.suggestedPrompts = new ObservableCollection<string>
        {
            "Group the employees by Company",
            "Filter employees that work for Northwind Traders",
            "Sort the employees by City in descending order",
            "Lock the Position column",
            "Clear all sorting, filtering and grouping",
        };
        // << datagrid-aiprompt-config-suggested-prompts-collection

        // >> datagrid-aiprompt-config-recent-prompts-collection
        this.recentPrompts = new ObservableCollection<string>
        {
            "Clear all sorting, filtering and grouping",
            "Group the employees by Company",
        };
        // << datagrid-aiprompt-config-recent-prompts-collection

        // >> datagrid-aiprompt-config-assign-collections
        this.AISettings.SuggestedPrompts = this.suggestedPrompts;
        this.AISettings.RecentPrompts = this.recentPrompts;
        // << datagrid-aiprompt-config-assign-collections

        // >> datagrid-prompt-load-sample-data-call
        this.LoadSampleData();
        this.dataGrid.ItemsSource = this.people;
        // << datagrid-prompt-load-sample-data-call
    }

    // >> datagrid-prompt-load-sample-data
    private void LoadSampleData()
    {
        this.people.Add(new Person { Name = "Nancy Davolio", Position = "Sales Representative", City = "Seattle", PostalCode = "98101", Company = "Northwind Traders" });
        this.people.Add(new Person { Name = "Andrew Fuller", Position = "Vice President, Sales", City = "Tacoma", PostalCode = "98402", Company = "Northwind Traders" });
        this.people.Add(new Person { Name = "Janet Leverling", Position = "Sales Representative", City = "Kirkland", PostalCode = "98033", Company = "Northwind Traders" });
        this.people.Add(new Person { Name = "Margaret Peacock", Position = "Sales Representative", City = "Redmond", PostalCode = "98052", Company = "Northwind Traders" });
        this.people.Add(new Person { Name = "Steven Buchanan", Position = "Sales Manager", City = "London", PostalCode = "SW1A 1AA", Company = "Consolidated Holdings" });
        this.people.Add(new Person { Name = "Michael Suyama", Position = "Sales Representative", City = "London", PostalCode = "EC1A 1BB", Company = "Consolidated Holdings" });
        this.people.Add(new Person { Name = "Robert King", Position = "Sales Representative", City = "London", PostalCode = "W1A 1HQ", Company = "Consolidated Holdings" });
        this.people.Add(new Person { Name = "Laura Callahan", Position = "Inside Sales Coordinator", City = "Portland", PostalCode = "97205", Company = "Contoso Ltd." });
        this.people.Add(new Person { Name = "Anne Dodsworth", Position = "Sales Representative", City = "Portland", PostalCode = "97209", Company = "Contoso Ltd." });
        this.people.Add(new Person { Name = "Tim Cook", Position = "Account Manager", City = "San Francisco", PostalCode = "94103", Company = "Fabrikam" });
    }
    // << datagrid-prompt-load-sample-data

    // >> datagrid-promptrequest-handling
    private async void OnPromptRequest(object sender, Telerik.Maui.Controls.DataGrid.DataGridPromptRequestEventArgs e)
    {
        if (this.cancellationTokenSource != null)
        {
            // An AI request is already being processed
            return;
        }

        this.cancellationTokenSource = new CancellationTokenSource();

        try
        {
            var request = JsonSerializer.Deserialize<object>(e.RequestJson);
            var requestResult = await HttpClient.PostAsJsonAsync("https://demos.telerik.com/service/v2/ai/grid/smart-state", request, this.cancellationTokenSource.Token);
            var response = await requestResult.Content.ReadAsStringAsync(this.cancellationTokenSource.Token);

            e.ResponseJson = response;
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            await this.ShowErrorAsync($"Failed to process request: {ex.Message}");
            e.HasError = true;
        }
        finally
        {
            this.cancellationTokenSource?.Dispose();
            this.cancellationTokenSource = null;
        }
    }
    // << datagrid-promptrequest-handling

    // >> datagrid-cancel-promptrequest-handling
    private void OnCancelPromptRequest(object sender, System.EventArgs e)
    {
        this.cancellationTokenSource?.Cancel();
    }
    // << datagrid-cancel-promptrequest-handling

    // >> datagrid-aiprompt-config-show-error-method
    private async Task ShowErrorAsync(string message)
    {
#if NET10_0_OR_GREATER
        await Application.Current?.Windows[0].Page?.DisplayAlertAsync("Error", message, "OK");
#else
        await Application.Current?.Windows[0].Page?.DisplayAlert("Error", message, "OK");
#endif
    }
    // << datagrid-aiprompt-config-show-error-method

    private void OnClearSuggestedPromptsCollectionClicked(object sender, EventArgs e)
    {
        this.suggestedPrompts.Clear();
    }

    private void OnClearRecentPromptsCollectionClicked(object sender, EventArgs e)
    {
        this.recentPrompts.Clear();
    }
}