using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using QSF.Examples.PdfViewerControl.Common;
using QSF.Services;
using QSF.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls.PdfViewer;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;

namespace QSF.Examples.PdfViewerControl.OpenSaveShareExample;

public class OpenSaveShareViewModel : ExampleViewModel
{
    private const string PdfFileName = "pdf_file.pdf";
    private const string ImportPickerTitle = "Please select a PDF file";

    private static readonly string[] importFileTypes;

    private DocumentSource pdfDocumentSource;
    private RadFixedDocument document;

    static OpenSaveShareViewModel()
    {
        var currentPlatform = DeviceInfo.Current.Platform;
        if (currentPlatform == DevicePlatform.Android)
        {
            importFileTypes = new[] { "application/pdf" };
        }
        else if (currentPlatform == DevicePlatform.iOS)
        {
            importFileTypes = new[] { "com.adobe.pdf" };
        }
        else if (currentPlatform == DevicePlatform.MacCatalyst)
        {
            importFileTypes = new[] { "pdf" };
        }
        else if (currentPlatform == DevicePlatform.WinUI)
        {
            importFileTypes = new[] { ".pdf" };
        }
        else
        {
            importFileTypes = Array.Empty<string>();
        }
    }

    public OpenSaveShareViewModel()
    {
        this.OpenDocumentCommand = new Command(this.OpenDocument);
        this.SaveDocumentCommand = new Command(this.SaveDocument);
        this.ShareDocumentCommand = new Command(this.ShareDocument);
        this.PdfDocumentSource = ResourceHelper.GetStreamFromEmbeddedResource(PdfFileName);
    }

    public DocumentSource PdfDocumentSource
    {
        get => this.pdfDocumentSource;
        set => this.UpdateValue(ref this.pdfDocumentSource, value);
    }

    public RadFixedDocument Document
    {
        get => this.document;
        set => this.UpdateValue(ref this.document, value);
    }

    public ICommand OpenDocumentCommand { get; set; }

    public ICommand SaveDocumentCommand { get; set; }

    public ICommand ShareDocumentCommand { get; set; }

    private static Func<CancellationToken, Task<Stream>> GetStreamFromFilePath(string filePath)
    {
        return ct => Task.Run(() =>
        {
            Stream stream = File.OpenRead(filePath);
            return stream;
        });
    }

    private async void OpenDocument(object obj)
    {
        await OpenAsync();
    }

    private async void SaveDocument(object obj)
    {
        await SaveAsync();
    }

    private async void ShareDocument(object obj)
    {
        await ShareAsync();
    }

    private async Task OpenAsync()
    {
        var filePickerService = DependencyService.Get<IFilePickerService>();
        var filePath = await filePickerService.PickFileAsync(ImportPickerTitle, importFileTypes);

        if (filePath is null)
        {
            return;
        }

        this.PdfDocumentSource = GetStreamFromFilePath(filePath);
    }

    private async Task SaveAsync()
    {
        var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(localFolder, PdfFileName);

        if (this.Document == null)
        {
            return;
        }

        using (Stream output = File.OpenWrite(filePath))
        {
            var provider = new PdfFormatProvider();
            provider.Export(this.Document, output);
        }

        await Application.Current.MainPage.DisplayAlert("Saved on this device as " + PdfFileName + ".", "Location: " + filePath, "OK");
    }

    private async Task ShareAsync()
    {
        Assembly assembly = typeof(OpenSaveShareViewModel).Assembly;
        string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains(PdfFileName));
        var cacheFile = Path.Combine(FileSystem.CacheDirectory, PdfFileName);

        using (Stream stream = assembly.GetManifestResourceStream(fileName))
        {
            using (var file = new FileStream(cacheFile, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(file);
            }
        }

        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = "Share PDF file",
            File = new ShareFile(cacheFile)
        });
    }
}