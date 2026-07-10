using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class ImageEditorViewModel : ViewModelBase
{
    public ImageEditorViewModel(string imageUri)
        : this()
    {
        this.ImageUri = imageUri;
    }

    public ImageEditorViewModel()
    {
#if !(MACCATALYST || WINDOWS)
        this.CanNavigateBack = true;
        this.CanSave = true;
#endif
        this.Title = "Edit Image";
        this.SaveCommand = new Command(OnSaveCommandExecuted);
    }

    public string ImageUri { get; set; }

    private async void OnSaveCommandExecuted(object obj)
    {
        await this.SaveImageAsync();
    }

    private async Task SaveImageAsync()
    {
        await this.DisplayAlertAsync("Success", $"You have successfully edited the image. However, since the app is in read-only mode, the changes will not be persisted.", "OK");
        await DependencyService.Get<INavigationService>().NavigateBackAsync();

        // **** READONLY DEMO **** //
        // This demo is readonly, we're not saving the changes to the cloud.
        // Below are ways you can save the changes depending on where you image data is kept.

        //using var memStream = new MemoryStream();

        // Step 1. Save the image to the stream using Png format, with 100% quality.
        //await imageEditor.SaveAsync(memStream, originalFormat, 1);

        // Step 2. Rewind the stream
        //memStream.Position = 0;

        // Step 3. Upload/save image to final data location.
        // Three options are presented below, but you can do whatever you need with the stream


        // **** Example 1 ****
        // - Save file to local filesystem
        //await using var fs = File.OpenWrite(Path.Join(FileSystem.Current.AppDataDirectory, "my-image.png"));
        //await memStream.CopyToAsync(fs);

        // **** Example 2 ****
        // - Saving byte[] to local database
        // https://stackoverflow.com/a/41337488
        // var imageBytes = memStream.ToArray();

        // **** Example 3 ****
        // - If you're using an Azure blob storage, you upload the file:
        // https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet#upload-blobs-to-a-container
        //BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
        //BlobClient blobClient = containerClient.GetBlobClient(fileName);
        //await blobClient.UploadAsync(memStream);
    }
}