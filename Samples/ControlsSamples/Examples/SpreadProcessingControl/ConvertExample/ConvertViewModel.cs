using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Txt;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace QSF.Examples.SpreadProcessingControl.ConvertExample
{
    public class ConvertViewModel : ExampleViewModel
    {
        private const string XlsxFormat = "xlsx";
        private const string CsvFormat = "csv";
        private const string TxtFormat = "txt";
        private const string PdfFormat = "pdf";
        private const string GenerateText = "Convert";
        private const string GeneratingText = "Working...";

        private static Workbook workbookCache;

        private string text;
        private string selectedExportFormat = XlsxFormat;
        private IEnumerable<string> exportFormats;
        private Command generateCommand = null;

        public ConvertViewModel()
        {
            this.text = GenerateText;
            this.selectedExportFormat = XlsxFormat;
            this.generateCommand = new Command(this.Generate, this.CanGenerate);
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            private set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand GenerateCommand
        {
            get
            {
                return this.generateCommand;
            }
        }

        public string SelectedExportFormat
        {
            get
            {
                return this.selectedExportFormat;
            }
            set
            {
                if (!object.Equals(this.selectedExportFormat, value))
                {
                    this.selectedExportFormat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public IEnumerable<string> ExportFormats
        {
            get
            {
                if (this.exportFormats == null)
                {
                    this.exportFormats = new string[] { XlsxFormat, CsvFormat, TxtFormat, PdfFormat };
                }

                return this.exportFormats;
            }
        }

        private async static Task GenerateAsync(string exportFormat)
        {
            try
            {
                Workbook workbook = workbookCache ?? await ImportAndCacheWorkbookAsync();
                Stream stream = await OpenStreamAsync(workbook, exportFormat);

                using (stream)
                {
                    string fileName = string.Format("RadSpreadProcessingConvertedFile.{0}", exportFormat);
                    IFileViewerService fileViewerService = DependencyService.Get<IFileViewerService>();
                    await fileViewerService.View(stream, fileName);
                }
            }
            catch
            {
                IToastMessageService messageService = DependencyService.Get<IToastMessageService>();
                messageService.ShortAlert("An error occured, please try again.");
            }
        }

        private static Task<Workbook> ImportAndCacheWorkbookAsync()
        {
            return Task.Run(() =>
            {
                if (workbookCache == null)
                {
                    workbookCache = ImportWorkbook();
                }

                return workbookCache;
            });
        }

        private static Workbook ImportWorkbook()
        {
            Assembly assembly = typeof(ConvertViewModel).Assembly;
            string fileName = assembly.GetManifestResourceNames().First(n => n.Contains("SpreadProcessingDocument1.xlsx"));
            string fileFormat = XlsxFormat;

            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                IWorkbookFormatProvider provider = GetProvider(fileFormat);
                Workbook workbook = provider.Import(stream);
                return workbook;
            }
        }

        private static Task<Stream> OpenStreamAsync(Workbook workbook, string exportFormat)
        {
            return Task.Run(() =>
            {
                Stream stream = OpenStream(workbook, exportFormat);
                return stream;
            });
        }

        private static Stream OpenStream(Workbook workbook, string exportFormat)
        {
            IWorkbookFormatProvider formatProvider = GetProvider(exportFormat);
            MemoryStream stream = new MemoryStream();
            formatProvider.Export(workbook, stream);
            return stream;
        }

        private static IWorkbookFormatProvider GetProvider(string exportFormat)
        {
            switch (exportFormat)
            {
                case XlsxFormat:
                    return new XlsxFormatProvider();
                case CsvFormat:
                    return new CsvFormatProvider();
                case TxtFormat:
                    return new TxtFormatProvider();
                case PdfFormat:
                    return new PdfFormatProvider();
                default:
                    throw new InvalidOperationException(string.Format("Invalid export format: {0}.", exportFormat));
            }
        }

        private async void Generate(object obj)
        {
            this.Text = GeneratingText;
            this.generateCommand.ChangeCanExecute();

            await GenerateAsync(this.selectedExportFormat);

            this.Text = GenerateText;
            this.generateCommand.ChangeCanExecute();
        }

        private bool CanGenerate(object arg)
        {
            return this.text == GenerateText;
        }
    }
}
