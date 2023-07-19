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
using Telerik.Documents.Common.Model;
using Telerik.Documents.Media;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Model.ConditionalFormattings;

namespace QSF.Examples.SpreadProcessingControl.ConditionalFormattingExample
{
    public class ConditionalFormattingViewModel : ExampleViewModel
    {
        private static Workbook workbookCache;

        private int priceRank;
        private double discountThreshold;
        private string shippingSearch;
        private string selectedShippingTerm;
        private ClientsRule selectedClientsRule;
        private PriceFilter selectedPriceFilter;
        private DiscountComparison selectedDiscountComparison;
        private IEnumerable<DiscountComparison> discountComparisonOperators;
        private IEnumerable<PriceFilter> priceFilters;
        private IEnumerable<ClientsRule> clientsRules;
        private IEnumerable<string> shippingTerms;
        private Command viewDocumentCommand;
        private Command viewFormattedDocumentCommand;

        public ConditionalFormattingViewModel()
        {
            this.SelectedClientsRule = ClientsRule.Duplicate;
            this.SelectedDiscountComparison = DiscountComparison.GreaterThanOrEqual;
            this.SelectedShippingTerm = this.ShippingTerms.Last();
            this.PriceRank = 5;
            this.SelectedPriceFilter = PriceFilter.Top;
            this.DiscountThreshold = 0.07;

            this.viewDocumentCommand = new Command(this.ViewDocument);
            this.viewFormattedDocumentCommand = new Command(this.ViewFormattedDocument);
        }

        public double DiscountThreshold
        {
            get { return this.discountThreshold; }
            set
            {
                if (this.discountThreshold != value)
                {
                    this.discountThreshold = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int PriceRank
        {
            get { return this.priceRank; }
            set
            {
                if (this.priceRank != value)
                {
                    this.priceRank = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string SelectedShippingTerm
        {
            get { return this.selectedShippingTerm; }
            set
            {
                if (this.selectedShippingTerm != value)
                {
                    this.selectedShippingTerm = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ClientsRule SelectedClientsRule
        {
            get { return this.selectedClientsRule; }
            set
            {
                if (this.selectedClientsRule != value)
                {
                    this.selectedClientsRule = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public DiscountComparison SelectedDiscountComparison
        {
            get { return this.selectedDiscountComparison; }
            set
            {
                if (this.selectedDiscountComparison != value)
                {
                    this.selectedDiscountComparison = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public PriceFilter SelectedPriceFilter
        {
            get { return this.selectedPriceFilter; }
            set
            {
                if (this.selectedPriceFilter != value)
                {
                    this.selectedPriceFilter = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string ShippingSearch
        {
            get { return this.shippingSearch; }
            set
            {
                if (this.shippingSearch != value)
                {
                    this.shippingSearch = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public IEnumerable<ClientsRule> ClientsRules
        {
            get
            {
                if (this.clientsRules == null)
                {
                    this.clientsRules = Enum.GetValues(typeof(ClientsRule)).Cast<ClientsRule>();
                }
                return this.clientsRules;
            }
        }

        public IEnumerable<PriceFilter> PriceFilters
        {
            get
            {
                if (this.priceFilters == null)
                {
                    this.priceFilters = Enum.GetValues(typeof(PriceFilter)).Cast<PriceFilter>();
                }
                return this.priceFilters;
            }
        }

        public IEnumerable<DiscountComparison> DiscountComparisonOperators
        {
            get
            {
                if (this.discountComparisonOperators == null)
                {
                    this.discountComparisonOperators = Enum.GetValues(typeof(DiscountComparison)).Cast<DiscountComparison>();
                }
                return this.discountComparisonOperators;
            }
        }

        public IEnumerable<string> ShippingTerms
        {
            get
            {
                if (this.shippingTerms == null)
                {
                    this.shippingTerms = new List<string>() { "1 day", "2 days", "regular", "express" };
                }
                return this.shippingTerms;
            }
        }

        public ICommand ViewDocumentCommand
        {
            get
            {
                return this.viewDocumentCommand;
            }
        }

        public ICommand ViewFormattedDocumentCommand
        {
            get
            {
                return this.viewFormattedDocumentCommand;
            }
        }

        private static Task<Stream> OpenStreamAsync(Workbook workbook)
        {
            return Task.Run(() =>
            {
                Stream stream = OpenStream(workbook);
                return stream;
            });
        }

        private static Stream OpenStream(Workbook workbook)
        {
            IWorkbookFormatProvider formatProvider = new XlsxFormatProvider();
            MemoryStream stream = new MemoryStream();
            formatProvider.Export(workbook, stream);
            return stream;
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
            Assembly assembly = typeof(ConditionalFormattingViewModel).Assembly;
            string fileName = assembly.GetManifestResourceNames().First(n => n.Contains("SpreadProcessingDocument2.xlsx"));

            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                IWorkbookFormatProvider provider = new XlsxFormatProvider();
                Workbook workbook = provider.Import(stream);
                return workbook;
            }
        }

        private static async Task ViewDocumentAsync(Workbook workbook, string fileName)
        {
            Stream stream = await OpenStreamAsync(workbook);

            using (stream)
            {
                IFileViewerService fileViewerService = DependencyService.Get<IFileViewerService>();
                await fileViewerService.View(stream, fileName);
            }
        }

        private async void ViewDocument(object obj)
        {
            this.viewDocumentCommand.ChangeCanExecute();

            try
            {
                Workbook workbook = await ImportAndCacheWorkbookAsync();
                await ViewDocumentAsync(workbook, "SpreadProcessingDocument2.xlsx");
            }
            catch
            {
                IToastMessageService messageService = DependencyService.Get<IToastMessageService>();
                messageService.ShortAlert("An error occured, please try again.");
            }

            this.viewDocumentCommand.ChangeCanExecute();
        }

        public async void ViewFormattedDocument(object parameter)
        {
            IWorkbookFormatProvider provider = new XlsxFormatProvider();
            Workbook workbook;

            try
            {
                Assembly assembly = typeof(ConditionalFormattingViewModel).Assembly;
                string fileName = assembly.GetManifestResourceNames().First(n => n.Contains("SpreadProcessingDocument2.xlsx"));

                using (Stream stream = assembly.GetManifestResourceStream(fileName))
                {
                    workbook = provider.Import(stream);
                }
                this.AddRules(workbook);

                Stream mystream = await OpenStreamAsync(workbook);
                using (mystream)
                {
                    IFileViewerService fileViewerService = DependencyService.Get<IFileViewerService>();
                    await fileViewerService.View(mystream, fileName);
                }
            }
            catch
            {
                IToastMessageService messageService = DependencyService.Get<IToastMessageService>();
                messageService.ShortAlert("An error occured, please try again.");
            }
        }

        private void AddRules(Workbook workBook)
        {
            Worksheet sheet = workBook.ActiveSheet as Worksheet;
            this.AddClientsRule(sheet);
            this.AddShippingRule(sheet);
            this.AddDiscountRule(sheet);
            this.AddTotalPriceRule(sheet);
        }

        private void AddShippingRule(Worksheet sheet)
        {
            ConditionalFormattingDxfRule shippingRule = null;
            shippingRule = new ContainsRule(this.SelectedShippingTerm);
            shippingRule.Formatting = new DifferentialFormatting();
            ThemableColor color = new ThemableColor(Colors.Green);
            shippingRule.Formatting.ForeColor = color;
            shippingRule.Formatting.IsBold = true;

            sheet.Cells[3, 3, 48, 3].AddConditionalFormatting(new ConditionalFormatting(shippingRule));
        }

        private void AddTotalPriceRule(Worksheet sheet)
        {
            ConditionalFormattingDxfRule totalPriceRule = null;
            switch (this.SelectedPriceFilter)
            {
                case PriceFilter.Bottom:
                    totalPriceRule = new BottomRule(ConditionalFormattingUnit.Items, this.PriceRank);
                    break;
                case PriceFilter.Top:
                    totalPriceRule = new TopRule(ConditionalFormattingUnit.Items, this.PriceRank);
                    break;
            }

            totalPriceRule.Formatting = new DifferentialFormatting();
            ThemableColor fillColor = new ThemableColor(Telerik.Documents.Media.Color.FromArgb(255, 255, 199, 206));
            ThemableColor foreColor = new ThemableColor(Telerik.Documents.Media.Color.FromArgb(255, 156, 0, 6));

            totalPriceRule.Formatting.ForeColor = foreColor;
            totalPriceRule.Formatting.Fill = new PatternFill(PatternType.Solid, fillColor, fillColor);

            sheet.Cells[3, 4, 48, 4].AddConditionalFormatting(new ConditionalFormatting(totalPriceRule));
        }

        private void AddDiscountRule(Worksheet sheet)
        {
            ConditionalFormattingDxfRule discountRule = null;
            switch (this.SelectedDiscountComparison)
            {
                case DiscountComparison.GreaterThan:
                    discountRule = new GreaterThanRule(this.discountThreshold.ToString());
                    break;
                case DiscountComparison.GreaterThanOrEqual:
                    discountRule = new GreaterThanOrEqualToRule(this.discountThreshold.ToString());
                    break;
                case DiscountComparison.LessThan:
                    discountRule = new LessThanRule(this.discountThreshold.ToString());
                    break;
                case DiscountComparison.LessThanOrEqual:
                    discountRule = new LessThanOrEqualToRule(this.discountThreshold.ToString());
                    break;
            }

            discountRule.Formatting = new DifferentialFormatting();
            ThemableColor fillColor = new ThemableColor(Telerik.Documents.Media.Color.FromArgb(255, 198, 239, 206));
            ThemableColor foreColor = new ThemableColor(Telerik.Documents.Media.Color.FromArgb(255, 0, 97, 0));
            discountRule.Formatting.ForeColor = foreColor;
            discountRule.Formatting.Fill = new PatternFill(PatternType.Solid, fillColor, fillColor);

            sheet.Cells[3, 5, 48, 5].AddConditionalFormatting(new ConditionalFormatting(discountRule));
        }

        private void AddClientsRule(Worksheet sheet)
        {
            ConditionalFormattingDxfRule clientsFormatting;
            if (this.SelectedClientsRule == ClientsRule.Duplicate)
            {
                clientsFormatting = new DuplicateValuesRule();
            }
            else
            {
                clientsFormatting = new UniqueValuesRule();
            }

            clientsFormatting.Formatting = new DifferentialFormatting();
            ThemableColor color = new ThemableColor(ThemeColorType.Accent4, 0.6);
            clientsFormatting.Formatting.Fill = new PatternFill(PatternType.Solid, color, color);

            sheet.Cells[3, 1, 48, 1].AddConditionalFormatting(new ConditionalFormatting(clientsFormatting));
        }
    }

    public enum ClientsRule
    {
        Duplicate,
        Unique
    }

    public enum PriceFilter
    {
        Top,
        Bottom
    }

    public enum DiscountComparison
    {
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }
}
