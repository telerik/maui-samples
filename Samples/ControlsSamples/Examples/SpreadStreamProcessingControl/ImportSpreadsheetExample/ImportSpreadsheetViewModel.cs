using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using Telerik.Documents.SpreadsheetStreaming;

namespace QSF.Examples.SpreadStreamProcessingControl.ImportSpreadsheetExample
{
    internal class ImportSpreadsheetViewModel : ExampleViewModel
    {
        private string console = String.Empty;
        private string fileLabelText = String.Empty;

        public string Console
        {
            get => console;
            set => UpdateValue(ref console, value);
        }
        public string FileLabelText
        {
            get => fileLabelText;
            set => UpdateValue(ref fileLabelText, value);
        }
        public Command ImportFileCommand { get; }
        public Command ImportSampleCommand { get; }

        public ImportSpreadsheetViewModel()
        {
            Console = "Import the sample or a custom file and see the data here.";
            ImportFileCommand = new Command(ImportFile);
            ImportSampleCommand = new Command(ImportSample);
        }

        private async void ImportFile()
        {
            var filePickerService = DependencyService.Get<IFilePickerService>();
            string selectedFilePath = await filePickerService.PickFileAsync("Please select a spreadsheet or a CSV file",  ".xlsx", ".csv" );

            if (selectedFilePath == null)
            {
                return;
            }
            string extension = Path.GetExtension(selectedFilePath);

            using (FileStream fileStream = File.OpenRead(selectedFilePath))
            {
                FileLabelText = Path.GetFileName(selectedFilePath);

                if (extension == ".xlsx")
                {
                    await ImportAsync(fileStream, SpreadDocumentFormat.Xlsx);
                }
                else
                {
                    await ImportAsync(fileStream, SpreadDocumentFormat.Csv);
                }
            }
        }

        private async void ImportSample()
        {
            FileLabelText = "Sample.xlsx";

            var resourceService = DependencyService.Get<IResourceService>();

            using (var resourceStream = resourceService.GetResourceStream("sample.xlsx"))
            {
                await ImportAsync(resourceStream, SpreadDocumentFormat.Xlsx);
            }

        }
        private async Task ImportAsync(Stream stream, SpreadDocumentFormat format)
        {
            await Task.Run(() =>
            {
                Import(stream, format);
            });
        }

        private void Import(Stream stream, SpreadDocumentFormat format)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (IWorkbookImporter workBookImporter = SpreadImporter.CreateWorkbookImporter(format, stream))
            {
                foreach (IWorksheetImporter worksheetImporter in workBookImporter.WorksheetImporters)
                {
                    foreach (IRowImporter rowImporter in worksheetImporter.Rows)
                    {
                        foreach (ICellImporter cell in rowImporter.Cells)
                        {
                            if (cell.Value != null)
                            {
                                stringBuilder.Append(cell.Value);
                                stringBuilder.Append(", ");
                            }
                        }
                        stringBuilder.AppendLine();
                    }
                }
            }
            Console = stringBuilder.ToString();
        }
    }
}
