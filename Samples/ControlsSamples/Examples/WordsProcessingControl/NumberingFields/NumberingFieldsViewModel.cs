using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Common.FormatProviders;
using Microsoft.Maui.Controls;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Collections;
using Telerik.Windows.Documents.Flow.Model.Styles;
using Telerik.Documents.Core.Fonts;
using Telerik.Windows.Documents.Flow.Model.Fields;
using System.IO;
using QSF.Examples.WordsProcessingControl.FindAndReplaceExample;
using QSF.Services;

namespace QSF.Examples.WordsProcessingControl.NumberingFieldsExample
{
    public class NumberingFieldsViewModel : ExampleViewModel
    {
        const string PageFieldCode = @"PAGE \* Ordinal";
        const string NumPagesCode = @"NUMPAGES";
        const string SectionCode = @"SECTION \* CardText";
        const string SectionPagesCode = @"SECTIONPAGES \* CardText";
        const string PageRefCode = @"PAGEREF";
        const string SampleDocument = "SampleDocument.docx";

        private readonly IFileViewerService fileViewerService;

        private RadFlowDocument document;
        private bool isDocumentLoaded;
        private readonly ICommand loadSampleCommand;
        private readonly ICommand exportCommand;
        private readonly Command insertNumberingFieldsCommand;
        private readonly Command updateNumberingFieldsCommand;

#if ANDROID || IOS
        private readonly ObservableCollection<string> exportFormats = new ObservableCollection<string> { "DOCX files(*.docx)", "RTF files(*.rtf)" };
#else
        private readonly ObservableCollection<string> exportFormats = new ObservableCollection<string> { "DOCX files(*.docx)", "PDF files(*.pdf)", "RTF files(*.rtf)" };
#endif


        private string fileName;
        private string selectedExportFormat;
        private string footerPreview;
        private string bookmarksPreview;
        private bool areNumberingFieldsInserted;

        public NumberingFieldsViewModel()
        {
            this.fileViewerService = DependencyService.Get<IFileViewerService>();

            this.SampleDocumentFile = SampleDocument;
            this.SelectedExportFormat = this.exportFormats.ElementAt(0);

            this.exportCommand = new Command(o => this.Export());
            this.loadSampleCommand = new Command(o => this.LoadSample());
            this.insertNumberingFieldsCommand = new Command(o => this.InsertNumberingFields(), o => CanExecuteInsert());
            this.updateNumberingFieldsCommand = new Command(o => this.UpdateNumberingFields(), o => CanExecuteUpdate());
        }

        public string SampleDocumentFile { get; private set; }

        public RadFlowDocument Document
        {
            get
            {
                return this.document;
            }
            set
            {
                this.UpdateValue(ref this.document, value);
            }
        }

        public bool IsDocumentLoaded
        {
            get
            {
                return this.isDocumentLoaded;
            }
            set
            {
                this.UpdateValue(ref this.isDocumentLoaded, value);
            }
        }

        public bool AreNumberingFieldsInserted
        {
            get
            {
                return this.areNumberingFieldsInserted;
            }
            set
            {
                this.UpdateValue(ref this.areNumberingFieldsInserted, value);
            }
        }

        public ICommand LoadSampleCommand
        {
            get
            {
                return this.loadSampleCommand;
            }
        }

        public ICommand ExportCommand
        {
            get
            {
                return this.exportCommand;
            }
        }

        public ICommand InsertNumberingFieldsCommand
        {
            get
            {
                return this.insertNumberingFieldsCommand;
            }
        }

        public ICommand UpdateNumberingFieldsCommand
        {
            get
            {
                return this.updateNumberingFieldsCommand;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.UpdateValue(ref this.fileName, value);
            }
        }

        public ObservableCollection<string> ExportFormats
        {
            get
            {
                return this.exportFormats;
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
                this.UpdateValue(ref this.selectedExportFormat, value);
            }
        }

        public string FooterPreview
        {
            get
            {
                return this.footerPreview;
            }
            set
            {
                this.UpdateValue(ref this.footerPreview, value);
            }
        }

        public string BookmarksPreview
        {
            get
            {
                return this.bookmarksPreview;
            }
            set
            {
                this.UpdateValue(ref this.bookmarksPreview, value);
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.Document):
                    this.IsDocumentLoaded = this.Document != null;
                    break;
                case nameof(AreNumberingFieldsInserted):
                    this.insertNumberingFieldsCommand.ChangeCanExecute();
                    this.updateNumberingFieldsCommand.ChangeCanExecute();
                    break;
                case nameof(this.IsDocumentLoaded):
                    this.AreNumberingFieldsInserted = false;
                    this.insertNumberingFieldsCommand.ChangeCanExecute();
                    break;
                default:
                    break;
            }
        }

        private void LoadSample()
        {
            this.Document = FindAndReplaceViewModel.OpenSample(this.SampleDocumentFile);
            this.FileName = this.SampleDocumentFile;
            this.AreNumberingFieldsInserted = false;

            this.FooterPreview = null;
            this.BookmarksPreview = null;
        }

        private async void Export()
        {
            string selectedFormat = this.SelectedExportFormat;
            await this.SaveDocument(this.Document, selectedFormat);
        }

        public async Task<bool> SaveDocument(RadFlowDocument document, string selectedFormat)
        {
            IFormatProvider<RadFlowDocument> formatProvider = null;
            string exampleName = null;
            switch (selectedFormat)
            {
                case "PDF files(*.pdf)":
                    formatProvider = new PdfFormatProvider();
                    exampleName = "example.pdf";
                    break;
                case "RTF files(*.rtf)":
                    formatProvider = new RtfFormatProvider();
                    exampleName = "example.rtf";
                    break;
                case "DOCX files(*.docx)":
                    formatProvider = new DocxFormatProvider();
                    exampleName = "example.docx";
                    break;
            }

            if (formatProvider == null)
            {
                return false;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                formatProvider.Export(document, stream);
                stream.Seek(0, SeekOrigin.Begin);
                await this.fileViewerService.View(stream, exampleName);
                return true;
            }
        }

        private void InsertNumberingFields()
        {
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(this.document);

            InsertFieldsInFooter(editor);
            AppendSectionWithBookmars(editor);

            this.UpdateFooterPreview();
            this.UpdateBookmarksPreview();

            this.AreNumberingFieldsInserted = true;
        }

        private bool CanExecuteInsert()
        {
            return this.IsDocumentLoaded && !this.AreNumberingFieldsInserted;
        }

        private bool CanExecuteUpdate()
        {
            return this.AreNumberingFieldsInserted;
        }

        private static void InsertFieldsInFooter(RadFlowDocumentEditor editor)
        {
            SectionCollection sections = editor.Document.Sections;

            if (sections.Count == 0)
            {
                sections.AddSection().Footers.Add(HeaderFooterType.Default);
            }

            foreach (Section section in sections)
            {
                Footer defaultFooter = section.Footers.Default;
                if (defaultFooter == null)
                {
                    defaultFooter = sections[0].Footers.Add(HeaderFooterType.Default);
                }
                else if (defaultFooter != null && defaultFooter.Blocks.Count > 0)
                {
                    defaultFooter.Blocks.Clear();
                }

                Paragraph paragraph = defaultFooter.Blocks.AddParagraph();
                editor.MoveToParagraphStart(paragraph);

                editor.ParagraphFormatting.TextAlignment.LocalValue = Alignment.Center;
                editor.InsertText("Page ");
                editor.InsertField(PageFieldCode, "<<to be updated>>");
                editor.InsertText(" of ");
                editor.InsertField(NumPagesCode, "<<to be updated>>");
                editor.InsertBreak(BreakType.LineBreak);
                editor.InsertText("(in section ");
                editor.InsertField(SectionCode, "<<to be updated>>");
                editor.InsertText(" with total pages in the section - ");
                editor.InsertField(SectionPagesCode, "<<to be updated>>");
                editor.InsertText(")");
            }
        }

        private static void AppendSectionWithBookmars(RadFlowDocumentEditor editor)
        {
            List<Paragraph> headings = editor.Document.EnumerateChildrenOfType<Paragraph>().Where(p => p.OutlineLevel == OutlineLevel.Level1).ToList();

            Dictionary<string, Paragraph> bookmarkToHeadings = new Dictionary<string, Paragraph>();
            for (int i = 0; i < headings.Count; i++)
            {
                Paragraph heading = headings[i];
                string bookmarkName = string.Format("heading{0}", i);

                bool temp = editor.Document.EnumerateChildrenOfType<BookmarkRangeStart>().Any(b => b.Bookmark.Name == bookmarkName);
                if (temp)
                {
                    bookmarkName = bookmarkName + "_new";
                }
                editor.InsertBookmark(bookmarkName, heading.Inlines[0], heading.Inlines[heading.Inlines.Count - 1]);

                bookmarkToHeadings.Add(bookmarkName, heading);
            }

            Section bookmarkSection = editor.Document.Sections.AddSection();
            Paragraph bookmarksParagraph = bookmarkSection.Blocks.AddParagraph();

            editor.MoveToParagraphStart(bookmarksParagraph);

            editor.ParagraphFormatting.StyleId = BuiltInStyleNames.GetHeadingStyleIdByIndex(1);
            editor.ParagraphFormatting.TextAlignment.LocalValue = Alignment.Left;
            editor.InsertText("Bookmarks:");
            editor.InsertBreak(BreakType.LineBreak);

            editor.ParagraphFormatting.StyleId = BuiltInStyleNames.NormalStyleId;
            editor.InsertParagraph();
            foreach (KeyValuePair<string, Paragraph> bookmarkToHeading in bookmarkToHeadings)
            {
                Run run = (Run)bookmarkToHeading.Value.Inlines.FirstOrDefault(i => i is Run);
                if (run == null)
                {
                    continue;
                }
                editor.CharacterFormatting.FontWeight.LocalValue = FontWeights.Normal;
                editor.InsertText(run.Text);
                editor.CharacterFormatting.FontWeight.LocalValue = FontWeights.Bold;
                editor.InsertText(" - page ");
                editor.InsertField(string.Format("{0} {1}", PageRefCode, bookmarkToHeading.Key), "<<to be updated>>");
                editor.InsertBreak(BreakType.LineBreak);
            }

            if (!bookmarkToHeadings.Any())
            {
                editor.InsertText("The document does not contain any Level 1 Headings.");
            }
        }

        private void UpdateNumberingFields()
        {
            List<FieldCharacter> fields = this.document.EnumerateChildrenOfType<FieldCharacter>().Where(fc => fc.FieldCharacterType == FieldCharacterType.Start).ToList();
            foreach (FieldCharacter field in fields)
            {
                field.FieldInfo.UpdateField();
            }

            this.UpdateFooterPreview();
            this.UpdateBookmarksPreview();
        }

        private void UpdateFooterPreview()
        {
            Footer defaultFooter = this.document.Sections[0].Footers.Default;
            Paragraph paragraph = (Paragraph)defaultFooter.Blocks[0];

            StringBuilder sb = new StringBuilder();

            foreach (InlineBase inline in paragraph.Inlines)
            {
                Run run = inline as Run;
                bool isRunDifferenThanFieldCode = run != null && run.Text != PageFieldCode && run.Text != NumPagesCode && run.Text != SectionCode && run.Text != SectionPagesCode;
                if (isRunDifferenThanFieldCode)
                {
                    sb.Append(run.Text);
                }
                else if (inline is Break)
                {
                    sb.Append(Environment.NewLine);
                }
            }

            this.FooterPreview = sb.ToString();
        }

        private void UpdateBookmarksPreview()
        {
            Section bookmarkSection = this.document.Sections.Last();
            Paragraph paragraph = (Paragraph)bookmarkSection.Blocks.Last();

            StringBuilder sb = new StringBuilder();
            bool skipNext = false;

            foreach (InlineBase inline in paragraph.Inlines)
            {
                Run run = inline as Run;
                bool isPagRefCode = run != null && run.Text.StartsWith(PageRefCode);

                bool isRunDifferenThanFieldCode = run != null && !isPagRefCode && !skipNext;
                if (isRunDifferenThanFieldCode)
                {
                    sb.Append(run.Text);
                }
                else if (inline is Break)
                {
                    sb.Append(Environment.NewLine);
                }

                skipNext = isPagRefCode;
            }

            this.BookmarksPreview = sb.ToString();
        }
    }
}
