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

namespace SDKBrowserMaui.Examples.TreeDataGridControl.PromptControlledCategory.PromptControlledExample;

// >> treedatagrid-prompt-viewmodel
public class ViewModel
{
    private static readonly HttpClient HttpClient = new HttpClient();
    private CancellationTokenSource cancellationTokenSource;
    private ICommand processAICommand;
    private ICommand cancelAICommand;
    private readonly ObservableCollection<string> suggestedPrompts;

    public ViewModel()
    {
        this.suggestedPrompts = this.CreateDefaultSuggestedPrompts();
        this.Items = this.CreateSampleData();
    }

    public ObservableCollection<Data> Items { get; private set; }

    public ObservableCollection<string> SuggestedPrompts => this.suggestedPrompts;

    public ICommand ProcessAICommand
    {
        get => this.processAICommand ?? (this.processAICommand = new Command<DataGridPromptRequestCommandContext>(this.ExecuteProcessAI));
    }

    public ICommand CancelAICommand
    {
        get => this.cancelAICommand ?? (this.cancelAICommand = new Command(this.ExecuteCancelAI));
    }

    private ObservableCollection<Data> CreateSampleData()
    {
        var root = new ObservableCollection<Data>();

        var folderDocs = new Data("Documents", 0, "Folder");
        folderDocs.Children.Add(new Data("Resume.docx", 24, "File"));
        folderDocs.Children.Add(new Data("Budget.xlsx", 120, "File"));
        // More documents
        folderDocs.Children.Add(new Data("ProjectProposal.pdf", 340, "File"));
        folderDocs.Children.Add(new Data("MeetingNotes.txt", 18, "File"));
        var reportsFolder = new Data("Reports", 0, "Folder");
        reportsFolder.Children.Add(new Data("Q1.pdf", 220, "File"));
        reportsFolder.Children.Add(new Data("Q2.pdf", 240, "File"));
        reportsFolder.Children.Add(new Data("Q3.pdf", 260, "File"));
        reportsFolder.Children.Add(new Data("Q4.pdf", 280, "File"));
        folderDocs.Children.Add(reportsFolder);

        var folderMedia = new Data("Media", 0, "Folder");
        var folderPhotos = new Data("Photos", 0, "Folder");
        folderPhotos.Children.Add(new Data("Vacation1.jpg", 2048, "Image"));
        folderPhotos.Children.Add(new Data("Vacation2.jpg", 1950, "Image"));
        folderPhotos.Children.Add(new Data("Family1.png", 980, "Image"));
        folderPhotos.Children.Add(new Data("Family2.png", 1120, "Image"));
        folderPhotos.Children.Add(new Data("Sunset.tif", 3250, "Image"));
        var eventsPhotos = new Data("Events", 0, "Folder");
        eventsPhotos.Children.Add(new Data("Birthday.jpg", 1540, "Image"));
        eventsPhotos.Children.Add(new Data("Conference.jpg", 1760, "Image"));
        folderPhotos.Children.Add(eventsPhotos);
        folderMedia.Children.Add(folderPhotos);
        folderMedia.Children.Add(new Data("Video.mp4", 150000, "Video"));
        folderMedia.Children.Add(new Data("Trailer.mov", 98000, "Video"));
        var musicFolder = new Data("Music", 0, "Folder");
        musicFolder.Children.Add(new Data("Track1.mp3", 5300, "Audio"));
        musicFolder.Children.Add(new Data("Track2.mp3", 6100, "Audio"));
        var albumFolder = new Data("Album - 2024", 0, "Folder");
        albumFolder.Children.Add(new Data("SongA.flac", 14500, "Audio"));
        albumFolder.Children.Add(new Data("SongB.flac", 15200, "Audio"));
        musicFolder.Children.Add(albumFolder);
        folderMedia.Children.Add(musicFolder);

        var folderSrc = new Data("Source", 0, "Folder");
        var subFolderApp = new Data("App", 0, "Folder");
        subFolderApp.Children.Add(new Data("MainPage.xaml", 12, "Xaml"));
        subFolderApp.Children.Add(new Data("MainPage.xaml.cs", 8, "CSharp"));
        subFolderApp.Children.Add(new Data("AppShell.xaml", 10, "Xaml"));
        subFolderApp.Children.Add(new Data("AppShell.xaml.cs", 7, "CSharp"));
        var subFolderViewModels = new Data("ViewModels", 0, "Folder");
        subFolderViewModels.Children.Add(new Data("MainViewModel.cs", 16, "CSharp"));
        subFolderViewModels.Children.Add(new Data("DetailsViewModel.cs", 22, "CSharp"));
        var subFolderServices = new Data("Services", 0, "Folder");
        subFolderServices.Children.Add(new Data("DataService.cs", 30, "CSharp"));
        subFolderServices.Children.Add(new Data("AuthService.cs", 26, "CSharp"));
        folderSrc.Children.Add(subFolderApp);
        folderSrc.Children.Add(subFolderViewModels);
        folderSrc.Children.Add(subFolderServices);
        folderSrc.Children.Add(new Data("Utils.cs", 4, "CSharp"));
        var folderTests = new Data("Tests", 0, "Folder");
        var unitFolder = new Data("UnitTests", 0, "Folder");
        unitFolder.Children.Add(new Data("ViewModelTests.cs", 18, "CSharp"));
        unitFolder.Children.Add(new Data("ServiceTests.cs", 20, "CSharp"));
        var uiFolder = new Data("UITests", 0, "Folder");
        uiFolder.Children.Add(new Data("LoginFlowTests.cs", 25, "CSharp"));
        uiFolder.Children.Add(new Data("CheckoutFlowTests.cs", 28, "CSharp"));
        folderTests.Children.Add(unitFolder);
        folderTests.Children.Add(uiFolder);
        folderSrc.Children.Add(folderTests);

        var folderAssets = new Data("Assets", 0, "Folder");
        var images = new Data("Images", 0, "Folder");
        images.Children.Add(new Data("logo.png", 256, "Image"));
        images.Children.Add(new Data("banner.jpg", 1024, "Image"));
        images.Children.Add(new Data("icon.svg", 34, "Vector"));
        var fonts = new Data("Fonts", 0, "Folder");
        fonts.Children.Add(new Data("OpenSans-Regular.ttf", 1920, "Font"));
        fonts.Children.Add(new Data("OpenSans-Bold.ttf", 2048, "Font"));
        folderAssets.Children.Add(images);
        folderAssets.Children.Add(fonts);

        var folderConfigs = new Data("Configs", 0, "Folder");
        folderConfigs.Children.Add(new Data("appsettings.json", 6, "Config"));
        folderConfigs.Children.Add(new Data("launchSettings.json", 4, "Config"));
        var envFolder = new Data("Environments", 0, "Folder");
        envFolder.Children.Add(new Data("dev.json", 3, "Config"));
        envFolder.Children.Add(new Data("staging.json", 3, "Config"));
        envFolder.Children.Add(new Data("prod.json", 3, "Config"));
        folderConfigs.Children.Add(envFolder);

        var folderDownloads = new Data("Downloads", 0, "Folder");
        folderDownloads.Children.Add(new Data("Installer.exe", 54000, "Binary"));
        folderDownloads.Children.Add(new Data("Archive.zip", 24000, "Compressed"));
        folderDownloads.Children.Add(new Data("Readme.md", 2, "Text"));

        var folderSandbox = new Data("Sandbox", 0, "Folder");
        var experiments = new Data("Experiments", 0, "Folder");
        experiments.Children.Add(new Data("PhysicsSim.py", 12, "Script"));
        experiments.Children.Add(new Data("MLModel.ipynb", 64, "Notebook"));
        experiments.Children.Add(new Data("shader.glsl", 6, "Shader"));
        var prototypes = new Data("Prototypes", 0, "Folder");
        prototypes.Children.Add(new Data("UIPrototype.fig", 8500, "Design"));
        prototypes.Children.Add(new Data("FlowDiagram.drawio", 1200, "Diagram"));
        folderSandbox.Children.Add(experiments);
        folderSandbox.Children.Add(prototypes);

        root.Add(folderDocs);
        root.Add(folderMedia);
        root.Add(folderSrc);
        root.Add(folderAssets);
        root.Add(folderConfigs);
        root.Add(folderDownloads);
        root.Add(folderSandbox);

        return root;
    }

    private ObservableCollection<string> CreateDefaultSuggestedPrompts()
    {
        return new ObservableCollection<string>
        {
            "Filter files by type equals to Image",
            "Show files which size is greater than 2000",
            "Sort by name ascending",
            "Clear all filters and sorting"
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
            var requestResult = await HttpClient.PostAsJsonAsync(
                "https://demos.telerik.com/service/v2/ai/grid/smart-state",
                request,
                this.cancellationTokenSource.Token);

            var response = await requestResult.Content.ReadAsStringAsync(this.cancellationTokenSource.Token);
            context.ResponseJson = response;
        }
        catch (OperationCanceledException)
        {
            // Cancellation was already handled by setting ProcessingState to Canceled
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
// << treedatagrid-prompt-viewmodel
