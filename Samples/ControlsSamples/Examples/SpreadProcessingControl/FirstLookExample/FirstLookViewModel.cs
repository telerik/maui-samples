using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Documents.Common.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Txt;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Utilities;

namespace QSF.Examples.SpreadProcessingControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private const string XlsxFormat = "xlsx";
        private const string CsvFormat = "csv";
        private const string TxtFormat = "txt";
        private const string PdfFormat = "pdf";
        private const string GenerateText = "Generate";
        private const string GeneratingText = "Working...";

        private string text;
        private string selectedExportFormat = XlsxFormat;
        private IEnumerable<string> exportFormats;
        private Command generateCommand = null;

        public FirstLookViewModel()
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

        public static Task<Stream> OpenStreamAsync(Workbook workbook, string exportFormat)
        {
            return Task.Run(() =>
            {
                Stream stream = OpenStream(workbook, exportFormat);
                return stream;
            });
        }

        public static Stream OpenStream(Workbook workbook, string exportFormat)
        {
            IWorkbookFormatProvider formatProvider = GetProvider(exportFormat);
            MemoryStream stream = new MemoryStream();
            formatProvider.Export(workbook, stream);
            return stream;
        }

        public static IWorkbookFormatProvider GetProvider(string exportFormat)
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

        private async static Task GenerateAsync(string exportFormat)
        {
            try
            {
                Workbook workbook = await CreateWorkbookAsync();
                Stream stream = await OpenStreamAsync(workbook, exportFormat);

                using (stream)
                {
                    string fileName = string.Format("RadSpreadProcessingFile.{0}", exportFormat);
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

        private static Task<Workbook> CreateWorkbookAsync()
        {
            return Task.Run(() =>
            {
                Workbook workbook = new Workbook();
                workbook.Worksheets.Add();
                Worksheet worksheet = workbook.ActiveWorksheet;
                string headingStyle = "Heading 1";
                IFill valueFill = PatternFill.CreateSolidFill(new ThemableColor(ThemeColorType.Accent1, ColorShadeType.Shade1));
                CellBorders sumSeparatorBorder = new CellBorders
                {
                    Top = new CellBorder(CellBorderStyle.Dashed, new ThemableColor(ThemeColorType.Text1))
                };

                // header
                worksheet.Cells[0, 0].SetValueAsText("ASSETS");
                worksheet.Cells[0, 0, 0, 1].SetStyleName(headingStyle);

                // current assets
                worksheet.Cells[1, 0].SetValueAsText("CURRENT ASSETS");
                worksheet.Cells[2, 0].SetValueAsText("Cash and cash equivalents");
                worksheet.Cells[2, 1].SetValue(421);
                worksheet.Cells[3, 0].SetValueAsText("Short-term investments");
                worksheet.Cells[3, 1].SetValue(1791);
                worksheet.Cells[4, 0].SetValueAsText("Accounts reveivable");
                worksheet.Cells[4, 1].SetValue(2116);
                worksheet.Cells[5, 0].SetValueAsText("Deferred income taxes");
                worksheet.Cells[5, 1].SetValue(512);
                worksheet.Cells[2, 1, 5, 1].SetFill(valueFill);
                worksheet.Cells[6, 0].SetValueAsText("TOTAL CURRENT ASSETS");
                worksheet.Cells[6, 1].SetValueAsFormula(GetRangeSumFormula(2, 5, 1));
                worksheet.Cells[6, 0, 6, 1].SetBorders(sumSeparatorBorder);

                // other assets
                worksheet.Cells[8, 0].SetValueAsText("OTHER ASSETS");
                worksheet.Cells[9, 0].SetValueAsText("Property and equipment at cost");
                worksheet.Cells[9, 1].SetValue(11354);
                worksheet.Cells[10, 0].SetValueAsText("Less accumulated deprecation");
                worksheet.Cells[10, 1].SetValue(-5106);
                worksheet.Cells[11, 0].SetValueAsText("Property and equipment (net)");
                worksheet.Cells[11, 1].SetValue(6935);
                worksheet.Cells[12, 0].SetValueAsText("Long-term cash investments");
                worksheet.Cells[12, 1].SetValue(511);
                worksheet.Cells[13, 0].SetValueAsText("Equity investments");
                worksheet.Cells[13, 1].SetValue(1971);
                worksheet.Cells[14, 0].SetValueAsText("Deferred income taxes");
                worksheet.Cells[14, 1].SetValue(1145);
                worksheet.Cells[9, 1, 14, 1].SetFill(valueFill);
                worksheet.Cells[15, 0].SetValueAsText("TOTAL OTHER ASSETS");
                worksheet.Cells[15, 1].SetValueAsFormula(GetRangeSumFormula(9, 14, 1));
                worksheet.Cells[15, 0, 15, 1].SetBorders(sumSeparatorBorder);

                // footer
                worksheet.Cells[17, 0].SetValueAsText("TOTAL ASSETS");
                worksheet.Cells[17, 1].SetValueAsFormula(GetCellsSumFormula(6, 1, 15, 1));
                worksheet.Cells[17, 0, 17, 1].SetStyleName(headingStyle);

                // some ui formatting
                worksheet.Cells[1, 0, 17, 0].SetIndent(2);
                worksheet.Cells[1, 1, 17, 1].SetFormat(new CellValueFormat(@"\$#,##0.00"));
                worksheet.Columns[0, 1].AutoFitWidth();

                return workbook;
            });
        }

        private static string GetRangeSumFormula(int startRow, int endRow, int column)
        {
            string cellRangeString = NameConverter.ConvertCellIndexesToName(startRow, column, endRow, column);
            string sumFormula = string.Format("=SUM({0})", cellRangeString);
            return sumFormula;
        }

        private static string GetCellsSumFormula(int cell1Row, int cell1Col, int cell2Row, int cell2Col)
        {
            string cell1Name = NameConverter.ConvertCellIndexToName(cell1Row, cell1Col);
            string cell2Name = NameConverter.ConvertCellIndexToName(cell2Row, cell2Col);
            string sumFormula = string.Format("={0}+{1}", cell1Name, cell2Name);
            return sumFormula;
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
