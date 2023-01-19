using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using QSF.Services;
using QSF.ViewModels;
using Telerik.Documents.SpreadsheetStreaming;

namespace QSF.Examples.SpreadStreamProcessingControl.ImportSpreadsheetExample
{
    internal class ImportSpreadsheetViewModel : ExampleViewModel
    {
        private const string importPickerTitle = "Please select a spreadsheet or a CSV file";
        private const string sampleResourceName = "sample.xlsx";

        private static readonly string[] importFileTypes;

        static ImportSpreadsheetViewModel()
        {
            var currentPlatform = DeviceInfo.Current.Platform;

            if (currentPlatform == DevicePlatform.Android)
            {
                importFileTypes = new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "text/comma-separated-values" };
            }
            else if (currentPlatform == DevicePlatform.iOS)
            {
                importFileTypes = new[] { "org.openxmlformats.spreadsheetml.sheet", "public.comma-separated-values-text" };
            }
            else if (currentPlatform == DevicePlatform.MacCatalyst)
            {
                importFileTypes = new[] { "xlsx", "csv" };
            }
            else if (currentPlatform == DevicePlatform.WinUI)
            {
                importFileTypes = new[] { ".xlsx", ".csv" };
            }
            else
            {
                importFileTypes = Array.Empty<string>();
            }
        }

        private WorkbookViewModel workbook;
        private bool isBusy;

        public WorkbookViewModel Workbook
        {
            get => this.workbook;
            private set => this.UpdateValue(ref this.workbook, value);
        }

        public bool IsBusy
        {
            get => this.isBusy;
            private set
            {
                if (this.UpdateValue(ref this.isBusy, value))
                {
                    this.UpdateCommands();
                }
            }
        }

        public ImportSpreadsheetViewModel()
        {
            this.ImportFileCommand = new Command(this.ImportFile, this.CanImport);
            this.ImportSampleCommand = new Command(this.ImportSample, this.CanImport);
        }

        public Command ImportFileCommand { get; }
        public Command ImportSampleCommand { get; }

        private void UpdateCommands()
        {
            this.ImportFileCommand.ChangeCanExecute();
            this.ImportSampleCommand.ChangeCanExecute();
        }

        private bool CanImport()
        {
            return !this.IsBusy;
        }

        private async void ImportFile()
        {
            var filePickerService = DependencyService.Get<IFilePickerService>();
            var filePath = await filePickerService.PickFileAsync(importPickerTitle, importFileTypes);

            if (filePath is null)
            {
                return;
            }

            var fileName = Path.GetFileName(filePath);
            var fileExtension = Path.GetExtension(filePath);

            SpreadDocumentFormat fileFormat;

            if (string.Equals(fileExtension, ".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                fileFormat = SpreadDocumentFormat.Xlsx;
            }
            else if (string.Equals(fileExtension, ".csv", StringComparison.OrdinalIgnoreCase))
            {
                fileFormat = SpreadDocumentFormat.Csv;
            }
            else
            {
                return;
            }

            using (var fileStream = File.OpenRead(filePath))
            {
                await this.InportWorkbookAsync(fileStream, fileFormat);
            }

            this.Workbook.Name = fileName;
        }

        private async void ImportSample()
        {
            var resourceService = DependencyService.Get<IResourceService>();

            using (var resourceStream = resourceService.GetResourceStream(sampleResourceName))
            {
                await this.InportWorkbookAsync(resourceStream, SpreadDocumentFormat.Xlsx);
            }

            this.Workbook.Name = sampleResourceName;
        }

        private async Task InportWorkbookAsync(Stream stream, SpreadDocumentFormat format)
        {
            this.IsBusy = true;
            this.Workbook = await this.LoadWorkbookAsync(stream, format);
            this.IsBusy = false;
        }

        private Task<WorkbookViewModel> LoadWorkbookAsync(Stream stream, SpreadDocumentFormat format)
        {
            return Task.Run(() => this.LoadWorkbookCore(stream, format));
        }

        private WorkbookViewModel LoadWorkbookCore(Stream stream, SpreadDocumentFormat format)
        {
            var workbookViewModel = new WorkbookViewModel();

            using (var workBookImporter = SpreadImporter.CreateWorkbookImporter(format, stream))
            {
                foreach (var worksheetImporter in workBookImporter.WorksheetImporters)
                {
                    var worksheetViewModel = new WorksheetViewModel
                    {
                        Name = worksheetImporter.Name
                    };

                    var columnImporters = worksheetImporter.Columns;

                    if (columnImporters is not null)
                    {
                        foreach (var columnImporter in columnImporters)
                        {
                            var minimumIndex = columnImporter.FromIndex;
                            var maximumIndex = columnImporter.ToIndex;

                            for (int columnIndex = minimumIndex; columnIndex <= maximumIndex; columnIndex++)
                            {
                                var columnName = $"Column {columnIndex}";
                                var columnViewModel = new ColumnViewModel
                                {
                                    Name = columnName
                                };

                                worksheetViewModel.Columns.Add(columnViewModel);
                            }
                        }
                    }

                    var rowImporters = worksheetImporter.Rows;

                    if (rowImporters is not null)
                    {
                        var columnCount = worksheetViewModel.Columns.Count;

                        foreach (var rowImporter in rowImporters)
                        {
                            var cellImporters = rowImporter.Cells;

                            if (cellImporters is not null)
                            {
                                var dataObject = new ExpandoObject();
                                var cellValues = (IDictionary<string, object>)dataObject;

                                foreach (var cellImporter in cellImporters)
                                {
                                    var columnIndex = cellImporter.ColumnIndex;

                                    while (columnIndex >= columnCount)
                                    {
                                        var columnName = $"Column {columnIndex}";
                                        var columnViewModel = new ColumnViewModel
                                        {
                                            Name = columnName
                                        };

                                        worksheetViewModel.Columns.Add(columnViewModel);
                                        columnCount++;
                                    }

                                    var currentViewModel = worksheetViewModel.Columns[columnIndex];
                                    var currentName = currentViewModel.Name;
                                    var currentValue = cellImporter.Value;

                                    cellValues.Add(currentName, currentValue);
                                }

                                worksheetViewModel.Rows.Add(dataObject);
                            }
                        }
                    }

                    foreach (var dataObject in worksheetViewModel.Rows)
                    {
                        var cellValues = (IDictionary<string, object>)dataObject;

                        foreach (var columnViewModel in worksheetViewModel.Columns)
                        {
                            var columnName = columnViewModel.Name;

                            if (!cellValues.ContainsKey(columnName))
                            {
                                cellValues.Add(columnName, string.Empty);
                            }
                        }
                    }

                    workbookViewModel.Worksheets.Add(worksheetViewModel);
                }
            }

            return workbookViewModel;
        }
    }
}
