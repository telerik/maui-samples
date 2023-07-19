using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Documents.Common.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Model.Comments;
using Telerik.Windows.Documents.Spreadsheet.Utilities;

namespace QSF.Examples.SpreadProcessingControl.AddNotesExample
{
    public class AddNotesViewModel : ExampleViewModel
    {
        private static readonly int IndexColumnQuantity = 1;
        private static readonly int IndexColumnUnitPrice = 2;
        private static readonly int IndexColumnSubTotal = 3;
        private static readonly int IndexColumnNoteTopLeftCorner = 4;
        private static readonly int IndexRowItemStart = 1;

        private static readonly string AccountFormatString = GenerateCultureDependentFormatString();
        private static readonly ThemableColor InvoiceBackground = new ThemableColor(Telerik.Documents.Media.Color.FromArgb(255, 44, 62, 80));
        private static readonly ThemableColor InvoiceHeading = new ThemableColor(ThemeColorType.Accent3, ColorShadeType.Shade2);
        private static readonly ThemableColor InvoiceHeaderForeground = new ThemableColor(Telerik.Documents.Media.Color.FromArgb(255, 255, 255, 255));

        private readonly Products data;
        private List<Product> products;

        private string author = "John Doe";
        private string description = "Product ID:";
        private ObservableCollection<string> items;
        private string selectedItem;
        private string text;
        private Command viewDocumentWithNotesCommand;


        public AddNotesViewModel()
        {
            this.data = new Products();
            this.GenerateData();
            this.Items = new ObservableCollection<string> { "Product ID", "Date" };
            this.SelectedItem = this.Items.First();
            this.PropertyChanged += AddNotesViewModel_PropertyChanged;
            this.ViewDocumentWithNotesCommand = new Command(this.ViewDocumentWithNotes);

        }

        private void AddNotesViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string text = string.Empty;
            if (this.SelectedItem == "Product ID")
            {
                text = this.GenerateString("<<ID>>");
            }
            else if (this.SelectedItem == "Date")
            {
                text = this.GenerateString("<<Date>>");
            }

            this.Text = text;
        }

        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                if (this.author != value)
                {
                    this.author = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                if (this.items != value)
                {
                    this.items = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Command ViewDocumentWithNotesCommand
        {
            get
            {
                return this.viewDocumentWithNotesCommand;
            }
            set
            {
                if (this.viewDocumentWithNotesCommand != value)
                {
                    this.viewDocumentWithNotesCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double Total
        {
            get
            {
                return this.CalculateTotal();
            }
        }

        public List<Product> Products
        {
            get { return this.products; }
            set
            {
                if (this.products != value)
                {
                    this.products = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void GenerateData()
        {
            this.Products = this.data.GetData(20).ToList();
        }

        private double CalculateTotal()
        {
            double totalAmount = 0;

            foreach (Product product in this.products)
            {
                totalAmount += product.SubTotal;
            }

            return totalAmount;
        }

        private Workbook CreateWorkbook()
        {
            Workbook workbook = new Workbook();
            workbook.Sheets.Add(SheetType.Worksheet);

            Worksheet worksheet = workbook.ActiveWorksheet;

            this.PrepareInvoiceDocument(worksheet, this.products.Count);

            int currentRow = IndexRowItemStart + 1;
            foreach (Product product in this.products)
            {
                worksheet.Cells[currentRow, 0].SetValue(product.Name);
                worksheet.Cells[currentRow, IndexColumnQuantity].SetValue(product.Quantity);
                worksheet.Cells[currentRow, IndexColumnUnitPrice].SetValue(product.UnitPrice);
                worksheet.Cells[currentRow, IndexColumnSubTotal].SetValue(product.SubTotal);

                this.InsertNote(worksheet, product, currentRow);

                currentRow++;
            }

            worksheet.Columns[0].SetWidth(new ColumnWidth(300, true));
            worksheet.Columns[IndexColumnUnitPrice].SetWidth(new ColumnWidth(120, true));
            worksheet.Columns[IndexColumnSubTotal].ExpandToFitNumberValuesWidth();

            return workbook;
        }

        private void InsertNote(Worksheet worksheet, Product product, int currentRow)
        {
            CellIndex cellAnchorIndex = new CellIndex(currentRow, IndexColumnQuantity);
            CellIndex topLetCellIndex = new CellIndex(currentRow - 1, IndexColumnNoteTopLeftCorner);

            string text = string.Empty;
            if (this.SelectedItem == "Product ID")
            {
                text = GenerateString(product.ID.ToString());
            }
            else if (this.SelectedItem == "Date")
            {
                text = GenerateString(product.Date.ToString());
            }

            SpreadsheetNote note = worksheet.Notes.Add(cellAnchorIndex, topLetCellIndex, this.Author, text);
            note.OffsetX = 10;
            note.OffsetY = 10;
            note.Width = 200;
            note.Height = 20;
            note.IsVisible = currentRow % 2 == 0;
        }

        private void PrepareInvoiceDocument(Worksheet worksheet, int itemsCount)
        {
            int lastItemIndexRow = IndexRowItemStart + itemsCount;

            CellIndex firstUsedCellIndex = new CellIndex(0, 0);
            CellIndex lastUsedCellIndex = new CellIndex(lastItemIndexRow + 1, IndexColumnSubTotal);
            CellBorder border = new CellBorder(CellBorderStyle.DashDot, InvoiceBackground);
            worksheet.Cells[firstUsedCellIndex, lastUsedCellIndex].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));

            worksheet.Cells[firstUsedCellIndex].SetValue("INVOICE");
            worksheet.Cells[firstUsedCellIndex].SetFontSize(20);

            worksheet.Cells[firstUsedCellIndex.RowIndex, 0, firstUsedCellIndex.ColumnIndex, IndexColumnSubTotal].SetFill
                (new GradientFill(GradientType.Horizontal, InvoiceHeading, InvoiceHeading));

            worksheet.Cells[IndexRowItemStart, 0].SetValue("Item");
            worksheet.Cells[IndexRowItemStart, IndexColumnQuantity].SetValue("QTY");
            worksheet.Cells[IndexRowItemStart, IndexColumnUnitPrice].SetValue("Unit Price");
            worksheet.Cells[IndexRowItemStart, IndexColumnSubTotal].SetValue("SubTotal");

            worksheet.Cells[IndexRowItemStart, 0, IndexRowItemStart, IndexColumnSubTotal].SetFill
                (new GradientFill(GradientType.Horizontal, InvoiceBackground, InvoiceBackground));
            worksheet.Cells[IndexRowItemStart, 0, IndexRowItemStart, IndexColumnSubTotal].SetForeColor(InvoiceHeaderForeground);
            worksheet.Cells[IndexRowItemStart, IndexColumnUnitPrice, lastItemIndexRow, IndexColumnSubTotal].SetFormat(
                new CellValueFormat(AccountFormatString));

            worksheet.Cells[lastItemIndexRow + 1, IndexColumnUnitPrice].SetValue("TOTAL: ");
            worksheet.Cells[lastItemIndexRow + 1, IndexColumnSubTotal].SetFormat(new CellValueFormat(AccountFormatString));

            string subTotalColumnCellRange = NameConverter.ConvertCellRangeToName(
                new CellIndex(IndexRowItemStart + 1, IndexColumnSubTotal),
                new CellIndex(lastItemIndexRow, IndexColumnSubTotal));

            worksheet.Cells[lastItemIndexRow + 1, IndexColumnSubTotal].SetValue(string.Format("=SUM({0})", subTotalColumnCellRange));

            worksheet.Cells[lastItemIndexRow + 1, IndexColumnUnitPrice, lastItemIndexRow + 1, IndexColumnSubTotal].SetFontSize(20);
        }

        public static Task<Stream> OpenStreamAsync(Workbook workbook)
        {
            return Task.Run(() =>
            {
                Stream stream = OpenStream(workbook);
                return stream;
            });
        }

        public static Stream OpenStream(Workbook workbook)
        {
            IWorkbookFormatProvider formatProvider = new XlsxFormatProvider();
            MemoryStream stream = new MemoryStream();
            formatProvider.Export(workbook, stream);
            return stream;
        }

        public async void ViewDocumentWithNotes(object parameter)
        {
            await GenerateAsync();
        }

        private async Task GenerateAsync()
        {
            try
            {
                Workbook workbook = this.CreateWorkbook();
                Stream stream = await OpenStreamAsync(workbook);

                using (stream)
                {
                    string fileName = string.Format("RadSpreadProcessingFile.xlsx");
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

        private string GenerateString(string param)
        {
            return string.Format("{0}: {1} {2}", this.Author, this.Description, param);
        }

        private static string GenerateCultureDependentFormatString()
        {
            string gS = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            string dS = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            return "_($* #" + gS + "##0" + dS + "00_);_($* (#" + gS + "##0" + dS + "00);_($* \"-\"??_);_(@_)";
        }
    }
}
