using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace QSF.Examples.SpreadProcessingControl.FindAndReplaceExample
{
    public class FindAndReplaceViewModel : ExampleViewModel
    {
        private const string ViewDocumentText_Const = "View Document";
        private const string ReplaceAndSaveText_Const = "Replace and Save";
        private const string WorkingText_Const = "Working...";

        private static Workbook workbookCache;

        private string viewDocumentText;
        private Command viewDocumentCommand;
        private string findWhat;
        private string replaceWith;
        private bool matchCase;
        private bool matchEntireCellContents;
        private string replaceAndSaveText;
        private Command replaceAndSaveCommand;

        public FindAndReplaceViewModel()
        {
            this.viewDocumentText = ViewDocumentText_Const;
            this.viewDocumentCommand = new Command(ViewDocument, this.CanViewDocument);
            this.replaceAndSaveText = ReplaceAndSaveText_Const;
            this.replaceAndSaveCommand = new Command(this.ReplaceAndSave, this.CanReplaceAndSave);
        }

        public string ViewDocumentText
        {
            get
            {
                return this.viewDocumentText;
            }
            private set
            {
                if (this.viewDocumentText != value)
                {
                    this.viewDocumentText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand ViewDocumentCommand
        {
            get
            {
                return this.viewDocumentCommand;
            }
        }

        public string FindWhat
        {
            get
            {
                return this.findWhat;
            }
            set
            {
                if (this.findWhat != value)
                {
                    this.findWhat = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ReplaceWith
        {
            get
            {
                return this.replaceWith;
            }
            set
            {
                if (this.replaceWith != value)
                {
                    this.replaceWith = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool MatchCase
        {
            get
            {
                return this.matchCase;
            }
            set
            {
                if (this.matchCase != value)
                {
                    this.matchCase = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool MatchEntireCellContents
        {
            get
            {
                return this.matchEntireCellContents;
            }
            set
            {
                if (this.matchEntireCellContents != value)
                {
                    this.matchEntireCellContents = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ReplaceAndSaveText
        {
            get
            {
                return this.replaceAndSaveText;
            }
            private set
            {
                if (this.replaceAndSaveText != value)
                {
                    this.replaceAndSaveText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand ReplaceAndSaveCommand
        {
            get
            {
                return this.replaceAndSaveCommand;
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
            Assembly assembly = typeof(FindAndReplaceViewModel).Assembly;
            string fileName = assembly.GetManifestResourceNames().First(n => n.Contains("SpreadProcessingDocument1.xlsx"));

            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                IWorkbookFormatProvider provider = new XlsxFormatProvider();
                Workbook workbook = provider.Import(stream);
                return workbook;
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

        private static async Task ViewDocumentAsync(Workbook workbook, string fileName)
        {
            Stream stream = await OpenStreamAsync(workbook);

            using (stream)
            {
                IFileViewerService fileViewerService = DependencyService.Get<IFileViewerService>();
                await fileViewerService.View(stream, fileName);
            }
        }

        private static Task<Workbook> ReplaceAsync(string findWhat, string replaceWith, bool matchCase, bool matchEntireCellContents)
        {
            return ImportAndCacheWorkbookAsync().ContinueWith(t =>
            {
                Workbook originalDoc = t.Result;

                Workbook workbook = new Workbook();
                workbook.Worksheets.Add();
                workbook.ActiveWorksheet.CopyFrom(originalDoc.Worksheets[0]);

                ReplaceOptions replaceOptions = new ReplaceOptions()
                {
                    FindWhat = findWhat,
                    ReplaceWith = replaceWith,
                    MatchCase = matchCase,
                    MatchEntireCellContents = matchEntireCellContents,
                    FindIn = FindInContentType.Values,
                };

                workbook.ReplaceAll(replaceOptions);

                return workbook;
            });
        }

        private async void ViewDocument(object obj)
        {
            this.ViewDocumentText = WorkingText_Const;
            this.viewDocumentCommand.ChangeCanExecute();

            try
            {
                Workbook workbook = await ImportAndCacheWorkbookAsync();
                await ViewDocumentAsync(workbook, "SpreadProcessingDocument1.xlsx");
            }
            catch
            {
                IToastMessageService messageService = DependencyService.Get<IToastMessageService>();
                messageService.ShortAlert("An error occured, please try again.");
            }

            this.ViewDocumentText = ViewDocumentText_Const;
            this.viewDocumentCommand.ChangeCanExecute();
        }

        private bool CanViewDocument(object arg)
        {
            return this.viewDocumentText == ViewDocumentText_Const;
        }

        private async void ReplaceAndSave(object obj)
        {
            this.ReplaceAndSaveText = WorkingText_Const;
            this.replaceAndSaveCommand.ChangeCanExecute();

            try
            {
                Workbook workbook = await ReplaceAsync(this.findWhat, this.replaceWith, this.matchCase, this.matchEntireCellContents);
                await ViewDocumentAsync(workbook, "SpreadProcessing_FindAndReplace.xlsx");
            }
            catch
            {
                IToastMessageService messageService = DependencyService.Get<IToastMessageService>();
                messageService.ShortAlert("An error occured, please try again.");
            }


            this.ReplaceAndSaveText = ReplaceAndSaveText_Const;
            this.replaceAndSaveCommand.ChangeCanExecute();
        }

        private bool CanReplaceAndSave(object arg)
        {
            return this.replaceAndSaveText == ReplaceAndSaveText_Const;
        }
    }
}
