using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.FormatProviders.Txt;
using Telerik.Windows.Documents.Flow.Model;

namespace QSF.Examples.WordsProcessingControl.ExportExample
{
    public class ExportViewModel : ExampleViewModel
    {
        private readonly IFileViewerService fileViewerService;

        private IEnumerable<string> exportFormats;
        private string selectedExportFormat;
        private RadFlowDocument flowDocument;

        public ExportViewModel()
        {
            this.ExportCommand = new Command(this.ExportCommandExecute);
            this.fileViewerService = DependencyService.Get<IFileViewerService>();
            this.SelectedExportFormat = this.ExportFormats.ElementAt(0);
        }

        public IEnumerable<string> ExportFormats
        {
            get
            {
                if (this.exportFormats == null)
                {
                    this.exportFormats = new string[] { "PDF (*.pdf)", "Rich Text Format (*.rtf)", "HTML (*.html)", "Plain Text (*.txt)" };
                }

                return this.exportFormats;
            }
        }

        public ICommand ExportCommand { get; set; }

        public string SelectedExportFormat
        {
            get
            {
                return this.selectedExportFormat;
            }
            set
            {
                if (this.selectedExportFormat != value)
                {
                    this.selectedExportFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private async void ExportCommandExecute()
        {
            this.OpenSample();

            IFormatProvider<RadFlowDocument> formatProvider = null;
            string exampleName = null;

            switch (this.selectedExportFormat)
            {
                case "PDF (*.pdf)":
                    formatProvider = new PdfFormatProvider();
                    exampleName = "example.pdf";
                    break;
                case "Rich Text Format (*.rtf)":
                    formatProvider = new RtfFormatProvider();
                    exampleName = "example.rtf";
                    break;
                case "HTML (*.html)":
                    formatProvider = new HtmlFormatProvider();
                    exampleName = "example.html";
                    break;
                case "Plain Text (*.txt)":
                    formatProvider = new TxtFormatProvider();
                    exampleName = "example.txt";
                    break;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                formatProvider.Export(this.flowDocument, stream);
                stream.Seek(0, SeekOrigin.Begin);
                await this.fileViewerService.View(stream, exampleName);
            }
        }

        private void OpenSample()
        {
            Assembly assembly = typeof(ExportView).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("DocToBeProcessed.docx"));

            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                this.flowDocument = new DocxFormatProvider().Import(stream);
            }
        }
    }
}
