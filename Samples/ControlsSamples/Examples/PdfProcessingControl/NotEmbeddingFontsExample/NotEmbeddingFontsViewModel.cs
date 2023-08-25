using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;

namespace QSF.Examples.PdfProcessingControl.NotEmbeddingFontsExample
{
    public class NotEmbeddingFontsViewModel : ExampleViewModel
    {
        private readonly IFileViewerService fileViewerService;
        private long loadedFileSize;
        private long exportedFileSize;
        private PdfFormatProvider provider;
        private RadFixedDocument document;

        public NotEmbeddingFontsViewModel()
        {
            this.ExportCommand = new Command(this.ExportDocument);

            this.provider = new PdfFormatProvider();
            this.provider.ExportSettings.ShouldEmbedFonts = false;

            this.ExportedFileSize = 0;
            this.fileViewerService = DependencyService.Get<IFileViewerService>();

            this.OpenPdfDocument();
        }

        public ICommand ExportCommand { get; set; }

        public long LoadedFileSize
        {
            get
            {
                return this.loadedFileSize;
            }
            set
            {
                if (this.loadedFileSize != value)
                {
                    this.loadedFileSize = value;
                    this.OnPropertyChanged("LoadedFileSize");
                }
            }
        }

        public long ExportedFileSize
        {
            get
            {
                return this.exportedFileSize;
            }
            set
            {
                if (this.exportedFileSize != value)
                {
                    this.exportedFileSize = value;
                    this.OnPropertyChanged("ExportedFileSize");
                }
            }
        }

        private async void ExportDocument(object obj)
        {
            await ExportAsync();
        }

        private async Task ExportAsync()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                provider.Export(this.document, stream);
                stream.Seek(0, SeekOrigin.Begin);

                this.ExportedFileSize = stream.Length / 1024;
                await this.fileViewerService.View(stream, "example.pdf");
            }
        }

        private void OpenPdfDocument()
        {
            Assembly assembly = typeof(NotEmbeddingFontsView).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf_file.pdf"));

            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                this.LoadedFileSize = stream.Length / 1024;
                this.document = provider.Import(stream);
            }
        }
    }
}
