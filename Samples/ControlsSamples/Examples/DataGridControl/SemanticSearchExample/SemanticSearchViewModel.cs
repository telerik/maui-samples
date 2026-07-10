using Microsoft.Maui.Controls;
using Microsoft.Maui.Networking;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls.DataGrid;
using static QSF.Examples.DataGridControl.SemanticSearchExample.LocalEmbeddingService;

namespace QSF.Examples.DataGridControl.SemanticSearchExample;

public class SemanticSearchViewModel : ExampleViewModel
{
    private static readonly HttpClient HttpClient = new HttpClient();

    private readonly LocalEmbeddingService embedder = new LocalEmbeddingService();
    private readonly Dictionary<string, EmbeddingF32> embeddingCache = new Dictionary<string, EmbeddingF32>();
    private CancellationTokenSource cancellationTokenSource;
    private ObservableCollection<string> suggestedPrompts;
    private ICommand processAICommand;
    private ICommand cancelAICommand;
    private ICommand retryDownloadCommand;
    private bool isDownloading = true;
    private bool hasDownloadError;
    private double downloadProgress;
    private string downloadProgressText;
    private string errorMessage;

    public SemanticSearchViewModel()
    {
        this.suggestedPrompts = this.CreateDefaultSuggestedPrompts();
        this.Products = CreateData();

        var progress = new Progress<double>(p => Application.Current.Dispatcher.Dispatch(() => this.DownloadProgress = p));
        this.DownloadModelAsync(progress);
    }

    public ObservableCollection<ProductCategory> Products { get; }

    public ObservableCollection<string> SuggestedPrompts
        => this.suggestedPrompts;

    public ICommand ProcessAICommand
        => this.processAICommand ?? (this.processAICommand = new Command<DataGridPromptRequestCommandContext>(this.ExecuteProcessAI));

    public ICommand CancelAICommand
        => this.cancelAICommand ?? (this.cancelAICommand = new Command(this.ExecuteCancelAI));

    public ICommand RetryDownloadCommand
        => this.retryDownloadCommand ?? (this.retryDownloadCommand = new Command(this.ExecuteRetryDownload));

    public bool IsDownloading
    {
        get => this.isDownloading;
        set => this.UpdateValue(ref this.isDownloading, value);
    }

    public bool HasDownloadError
    {
        get => this.hasDownloadError;
        set => this.UpdateValue(ref this.hasDownloadError, value);
    }

    public double DownloadProgress
    {
        get => this.downloadProgress;
        set
        {
            if (this.UpdateValue(ref this.downloadProgress, value))
            {
                this.DownloadProgressText = $"Downloading AI model... {value * 100:0}%";
            }
        }
    }

    public string DownloadProgressText
    {
        get => this.downloadProgressText;
        set => this.UpdateValue(ref this.downloadProgressText, value);
    }

    public string ErrorMessage
    {
        get => this.errorMessage;
        set => this.UpdateValue(ref this.errorMessage, value);
    }

    public Action<object> ProvideSearchMatchesAction => (query) =>
    {
        var probe = (Telerik.Maui.Controls.DataGrid.DataGridSemanticSearchCellProbe)query;

        if (!this.embeddingCache.TryGetValue(probe.SearchText, out var search))
        {
            search = this.embedder.Embed(probe.SearchText);
            this.embeddingCache[probe.SearchText] = search;
        }

        var cellValue = probe.CellValue?.ToString() ?? string.Empty;
        if (!string.IsNullOrEmpty(cellValue))
        {
            if (!this.embeddingCache.TryGetValue(cellValue, out var cellEmbedding))
            {
                cellEmbedding = this.embedder.EmbedAsync(cellValue).GetAwaiter().GetResult();
                this.embeddingCache[cellValue] = cellEmbedding;
            }

            var similarity = cellEmbedding.Similarity(search);
            if (similarity > 0.50)
            {
                probe.IsMatch = true;
            }
        }
    };

    protected virtual ObservableCollection<string> CreateDefaultSuggestedPrompts()
    {
        return new ObservableCollection<string>
        {
            "Sort by category name",
            "Group by category name",
            "Clear all filters, sorting and grouping"
        };
    }

    private static ObservableCollection<ProductCategory> CreateData()
    {
        return new ObservableCollection<ProductCategory>
        {
            new ProductCategory { CategoryId = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" },
            new ProductCategory { CategoryId = 2, CategoryName = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings" },
            new ProductCategory { CategoryId = 3, CategoryName = "Confections", Description = "Desserts, candies, and sweet breads" },
            new ProductCategory { CategoryId = 4, CategoryName = "Dairy Products", Description = "Cheeses" },
            new ProductCategory { CategoryId = 5, CategoryName = "Grains/Cereals", Description = "Breads, crackers, pasta, and cereal" },
            new ProductCategory { CategoryId = 6, CategoryName = "Meat/Poultry", Description = "Prepared meats" },
            new ProductCategory { CategoryId = 7, CategoryName = "Produce", Description = "Dried fruit and bean curd" },
            new ProductCategory { CategoryId = 8, CategoryName = "Seafood", Description = "Seaweed and fish" },
            new ProductCategory { CategoryId = 9, CategoryName = "Snacks", Description = "Chips, pretzels, and popcorn" },
            new ProductCategory { CategoryId = 10, CategoryName = "Frozen Foods", Description = "Frozen vegetables, ice cream, and frozen dinners" },
            new ProductCategory { CategoryId = 11, CategoryName = "Household", Description = "Cleaning products, paper goods, and other household items" },
            new ProductCategory { CategoryId = 12, CategoryName = "Personal Care", Description = "Toiletries and personal care products" },
            new ProductCategory { CategoryId = 13, CategoryName = "Health", Description = "Health and wellness products" },
            new ProductCategory { CategoryId = 14, CategoryName = "Baby", Description = "Baby food, diapers, and other baby products" },
            new ProductCategory { CategoryId = 15, CategoryName = "Pet Supplies", Description = "Food and supplies for dogs, cats, and other pets" },
            new ProductCategory { CategoryId = 16, CategoryName = "Office Supplies", Description = "Office and school supplies" },
            new ProductCategory { CategoryId = 17, CategoryName = "Automotive", Description = "Automotive parts and accessories" },
            new ProductCategory { CategoryId = 18, CategoryName = "Books", Description = "Books across various genres and topics" },
            new ProductCategory { CategoryId = 19, CategoryName = "Music", Description = "CDs, vinyl records, and music accessories" },
            new ProductCategory { CategoryId = 20, CategoryName = "Movies", Description = "DVDs, Blu-rays, and streaming media" },
            new ProductCategory { CategoryId = 21, CategoryName = "Electronics", Description = "Computers, phones, and other electronic devices" },
            new ProductCategory { CategoryId = 22, CategoryName = "Clothing", Description = "Men's, women's, and children's apparel" },
        };
    }

    public async void DownloadModelAsync(IProgress<double> progress)
    {
        if (this.embedder.IsModelDownloaded)
        {
            this.HasDownloadError = false;
            this.ErrorMessage = null;
            this.IsDownloading = false;
            return;
        }

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            this.IsDownloading = false;
            this.SetDownloadError("Cannot download AI model.\n No internet connection is available.");
            return;
        }

        this.IsDownloading = true;
        this.HasDownloadError = false;
        this.ErrorMessage = null;

        try
        {
            await this.embedder.DownloadModelAsync(progress);
            this.DownloadProgressText = "100%";
        }
        catch (Exception ex)
        {
            this.SetDownloadError($"Failed to download AI model: {ex.Message}");
        }
        finally
        {
            this.IsDownloading = false;
        }
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
        => this.cancellationTokenSource?.Cancel();

    private void SetDownloadError(string message)
    {
        this.ErrorMessage = message;
        this.HasDownloadError = true;
    }

    private void ExecuteRetryDownload()
    {
        this.DownloadProgress = 0;
        this.DownloadModelAsync(new Progress<double>(p => Application.Current.Dispatcher.Dispatch(() => this.DownloadProgress = p)));
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