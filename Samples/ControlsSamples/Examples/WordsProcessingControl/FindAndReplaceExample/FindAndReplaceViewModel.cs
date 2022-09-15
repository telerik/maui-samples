using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;

namespace QSF.Examples.WordsProcessingControl.FindAndReplaceExample
{
    public class FindAndReplaceViewModel : ExampleViewModel
    {
        private const string documentName = "JohnGrisham.docx";

        private readonly IFileViewerService fileViewerService;
        private bool useRegex;
        private bool matchCase;
        private bool matchWholeWord;
        private string findWhat;
        private string replaceWith;
        private RadFlowDocument replacedDocument;
        private RadFlowDocument sampleDocument;

        public FindAndReplaceViewModel()
        {
            this.GenerateSampleCommand = new Command(this.GenerateSampleExecute);
            this.ReplaceAndSaveCommand = new Command(this.ReplaceAndSaveExecute);

            this.fileViewerService = DependencyService.Get<IFileViewerService>();
        }

        public ICommand ReplaceAndSaveCommand { get; set; }

        public ICommand GenerateSampleCommand { get; set; }

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

        public bool MatchWholeWord
        {
            get
            {
                return this.matchWholeWord;
            }
            set
            {
                if (this.matchWholeWord != value)
                {
                    this.matchWholeWord = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool UseRegex
        {
            get
            {
                return this.useRegex;
            }
            set
            {
                if (this.useRegex != value)
                {
                    this.useRegex = value;
                    this.OnPropertyChanged();
                }
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

        internal static RadFlowDocument OpenSample(string docName)
        {
            Assembly assembly = typeof(FindAndReplaceView).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains(docName));

            RadFlowDocument doc = new RadFlowDocument();
            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                doc = new DocxFormatProvider().Import(stream);
            }

            return doc;
        }

        private async void GenerateSampleExecute()
        {
            if (this.sampleDocument == null)
            {
                this.sampleDocument = OpenSample(documentName);
            }

            IFormatProvider<RadFlowDocument> formatProvider = new DocxFormatProvider();
            string exampleName = "example.docx";

            using (MemoryStream stream = new MemoryStream())
            {
                formatProvider.Export(this.sampleDocument, stream);
                stream.Seek(0, SeekOrigin.Begin);
                await this.fileViewerService.View(stream, exampleName);
            }
        }

        private async void ReplaceAndSaveExecute()
        {
            if (this.replacedDocument == null)
            {
                this.replacedDocument = OpenSample(documentName);
            }

            this.ReplaceText();

            IFormatProvider<RadFlowDocument> formatProvider = new DocxFormatProvider();
            string exampleName = "example.docx";

            using (MemoryStream stream = new MemoryStream())
            {
                formatProvider.Export(this.replacedDocument, stream);
                stream.Seek(0, SeekOrigin.Begin);
                await this.fileViewerService.View(stream, exampleName);
            }
        }

        private void ReplaceText()
        {
            if (string.IsNullOrEmpty(this.findWhat))
            {
                return;
            }

            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(this.replacedDocument);

            if (this.useRegex)
            {
                Regex oldTextRegex = new Regex(this.findWhat);
                editor.ReplaceText(oldTextRegex, this.replaceWith);
            }
            else
            {
                editor.ReplaceText(this.findWhat, this.replaceWith, this.matchCase, this.matchWholeWord);
            }
        }
    }
}
