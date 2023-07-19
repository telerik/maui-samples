using Microsoft.Maui.Controls;
using QSF.Examples.SpreadProcessingControl.AddNotesExample;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.Generic;
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

namespace QSF.Examples.SpreadProcessingControl.AddCommentsExample
{
    internal class AddCommentsViewModel : ExampleViewModel
    {
        private static readonly int IndexColumnQuantity = 1;
        private static readonly int IndexColumnUnitPrice = 2;
        private static readonly int IndexColumnSubTotal = 3;
        private static readonly int IndexRowItemStart = 1;

        private static readonly string AccountFormatString = GenerateCultureDependentFormatString();
        private static readonly ThemableColor InvoiceBackground = new ThemableColor(Telerik.Documents.Media.Color.FromArgb(255, 44, 62, 80));
        private static readonly ThemableColor InvoiceHeading = new ThemableColor(ThemeColorType.Accent3, ColorShadeType.Shade2);
        private static readonly ThemableColor InvoiceHeaderForeground = new ThemableColor(Telerik.Documents.Media.Color.FromArgb(255, 255, 255, 255));

        private readonly Products data;

        private List<Product> products;
        private Command insertCommentCommand;
        private Command viewDocumentWithCommentsCommand = null;

        private readonly Workbook workbook;
        private string author = "John Doe";
        private string text = "Sample text";
        private string relatedCell = "A1";
        private CellIndex relatedCellIndex = new CellIndex(0, 0);
        private bool isComment = true;

        public AddCommentsViewModel()
        {
            this.data = new Products();
            this.GenerateData();
            this.workbook = this.CreateWorkbook();

            this.ViewDocumentWithCommentsCommand = new Command(this.ViewDocumentWithComments);
            this.InsertCommentCommand = new Command(this.InsertComment);
        }

        public string RelatedCell
        {
            get
            {
                return this.relatedCell;
            }
            set
            {
                if (this.relatedCell != value)
                {
                    this.relatedCell = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public CellIndex RelatedCellIndex
        {
            get
            {
                return this.relatedCellIndex;
            }
            set
            {
                if (this.relatedCellIndex != value)
                {
                    this.relatedCellIndex = value;
                    OnPropertyChanged();
                }
            }
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
                    OnPropertyChanged();
                }
            }
        }

        public bool IsComment
        {
            get
            {
                return this.isComment;
            }
            set
            {
                if (this.isComment != value)
                {
                    this.isComment = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Command InsertCommentCommand
        {
            get
            {
                return this.insertCommentCommand;
            }
            set
            {
                if (this.insertCommentCommand != value)
                {
                    this.insertCommentCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command ViewDocumentWithCommentsCommand
        {
            get
            {
                return this.viewDocumentWithCommentsCommand;
            }
            set
            {
                if (this.viewDocumentWithCommentsCommand != value)
                {
                    this.viewDocumentWithCommentsCommand = value;
                    OnPropertyChanged();
                }
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

        public double Total
        {
            get
            {
                return this.CalculateTotal();
            }
        }

        private void GenerateData()
        {
            this.Products = this.data.GetData(8).ToList();
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
                currentRow++;
            }

            worksheet.Columns[0].SetWidth(new ColumnWidth(100, true));
            worksheet.Columns[IndexColumnUnitPrice].SetWidth(new ColumnWidth(70, true));
            worksheet.Columns[IndexColumnSubTotal].ExpandToFitNumberValuesWidth();

            return workbook;
        }

        private void InsertComment(object param)
        {
            int rowIndex;
            int columnIndex;
            bool isCellNameValid = NameConverter.TryConvertCellNameToIndex(this.RelatedCell, out rowIndex, out columnIndex);

            if (!isCellNameValid)
            {
                IToastMessageService messageService = DependencyService.Get<IToastMessageService>();
                messageService.ShortAlert("The cell name is not valid. Please, enter a valid cell name.");
                this.RelatedCell = NameConverter.ConvertCellIndexToName(this.RelatedCellIndex);
            }
            else
            {
                this.RelatedCellIndex = new CellIndex(rowIndex, columnIndex);
                this.IsComment = !this.DoesCellContainComment(this.RelatedCellIndex);
                Worksheet worksheet = this.workbook.ActiveWorksheet;
                if (this.IsComment)
                {
                    SpreadsheetComment comment = worksheet.Comments.Add(this.RelatedCellIndex, this.Author, this.Text);
                    this.IsComment = false;
                }
                else
                {
                    SpreadsheetComment comment = worksheet.Comments.Single(c => c.RelatedCellIndex == this.RelatedCellIndex);
                    SpreadsheetCommentReply reply = comment.AddReply(this.Author, this.Text);
                }
            }
        }

        private void PrepareInvoiceDocument(Worksheet worksheet, int itemsCount)
        {
            int lastItemIndexRow = IndexRowItemStart + itemsCount;

            CellIndex firstUsedCellIndex = new CellIndex(0, 0);
            CellIndex lastUsedCellIndex = new CellIndex(lastItemIndexRow + 1, IndexColumnSubTotal);
            CellBorder border = new CellBorder(CellBorderStyle.Dotted, InvoiceBackground);
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

        public async void ViewDocumentWithComments(object parameter)
        {
            await GenerateAsync();
        }

        private async Task GenerateAsync()
        {
            try
            {
                Workbook workbook = this.workbook;
                Stream stream = await OpenStreamAsync(workbook);

                using (stream)
                {
                    string fileName = string.Format("RadSpreadProcessingFile1.xlsx");
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

        private static string GenerateCultureDependentFormatString()
        {
            string gS = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            string dS = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            return "_($* #" + gS + "##0" + dS + "00_);_($* (#" + gS + "##0" + dS + "00);_($* \"-\"??_);_(@_)";
        }

        private bool DoesCellContainComment(CellIndex cellIndex)
        {
            return this.workbook.ActiveWorksheet.Comments.Any(n => n.RelatedCellIndex == cellIndex);
        }
    }
}
