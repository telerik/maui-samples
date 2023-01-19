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

namespace QSF.Examples.WordsProcessingControl.TableOfContentsExample
{
    public class TableOfContentsViewModel : ExampleViewModel
    {
        const string sampleDocument = "TableOfContents.docx";
        private readonly IFileViewerService fileViewerService;
        private RadFlowDocument document;
        private string fileName;
        private bool isDocumentLoaded;

        private readonly Command loadSampleCommand;
        private readonly Command exportCommand;
        private readonly Command insertAndUpdateTocFieldsCommand;
        private readonly Command insertAndUpdateToaFieldsCommand;

        private readonly ObservableCollection<string> exportFormats = new ObservableCollection<string> { "DOCX files(*.docx)", "PDF files(*.pdf)", "RTF files(*.rtf)" };
        private string selectedExportFormat;

        private bool includeHeadings;
        private string headingLevel;
        private bool includeTcFields;
        private string tcLevel;
        private bool includeCaptions;
        private string caption;
        private int categoryLevel;
        private bool includeCategoryHeader;

        public TableOfContentsViewModel()
        {
            this.fileViewerService = DependencyService.Get<IFileViewerService>();
            this.SampleDocumentFile = sampleDocument;
            this.SelectedExportFormat = this.exportFormats.ElementAt(0);

            this.loadSampleCommand = new Command(o => this.LoadSample());
            this.exportCommand = new Command(o => this.Export(), s => this.CanExecuteDocumentCommand());
            this.insertAndUpdateTocFieldsCommand = new Command(o => this.InsertAndUpdateTocField(), s => this.CanExecuteDocumentCommand());
            this.insertAndUpdateToaFieldsCommand = new Command(o => this.InsertAndUpdateToaField(), s => this.CanExecuteDocumentCommand());

            this.includeHeadings = true;
            this.includeCategoryHeader = true;
            this.headingLevel = "1-9";
            this.tcLevel = "1-9";
            this.caption = "Image";
            this.categoryLevel = 1;
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
                this.ExportCommand.ChangeCanExecute();
                this.InsertAndUpdateToaFieldsCommand.ChangeCanExecute();
                this.InsertAndUpdateTocFieldsCommand.ChangeCanExecute();
            }
        }

        public Command LoadSampleCommand
        {
            get
            {
                return this.loadSampleCommand;
            }
        }

        public Command ExportCommand
        {
            get
            {
                return this.exportCommand;
            }
        }

        public Command InsertAndUpdateTocFieldsCommand
        {
            get
            {
                return this.insertAndUpdateTocFieldsCommand;
            }
        }

        public Command InsertAndUpdateToaFieldsCommand
        {
            get
            {
                return this.insertAndUpdateToaFieldsCommand;
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

        public bool IncludeHeadings
        {
            get
            {
                return this.includeHeadings;
            }
            set
            {
                this.UpdateValue(ref this.includeHeadings, value);
            }
        }

        public string HeadingLevel
        {
            get
            {
                return this.headingLevel;
            }
            set
            {
                this.UpdateValue(ref this.headingLevel, value);
            }
        }

        public bool IncludeTcFields
        {
            get
            {
                return this.includeTcFields;
            }
            set
            {
                this.UpdateValue(ref this.includeTcFields, value);
            }
        }

        public string TcLevel
        {
            get
            {
                return this.tcLevel;
            }
            set
            {
                this.UpdateValue(ref this.tcLevel, value);
            }
        }

        public bool IncludeCaptions
        {
            get
            {
                return this.includeCaptions;
            }
            set
            {
                this.UpdateValue(ref this.includeCaptions, value);
            }
        }

        public string Caption
        {
            get
            {
                return this.caption;
            }
            set
            {
                this.UpdateValue(ref this.caption, value);
            }
        }

        public int CategoryLevel
        {
            get
            {
                return this.categoryLevel;
            }
            set
            {
                this.UpdateValue(ref this.categoryLevel, value);
            }
        }

        public bool IncludeCategoryHeader
        {
            get
            {
                return this.includeCategoryHeader;
            }
            set
            {
                this.UpdateValue(ref this.includeCategoryHeader, value);
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
                default:
                    break;
            }
        }

        private void LoadSample()
        {
            this.Document = FindAndReplaceViewModel.OpenSample(this.SampleDocumentFile);
            this.FileName = this.SampleDocumentFile;
        }

        private async void Export()
        {
            string selectedFormat = this.SelectedExportFormat;
            await this.SaveDocument(this.Document, selectedFormat);
        }

        private bool CanExecuteDocumentCommand()
        {
            return this.IsDocumentLoaded;
        }

        private void InsertAndUpdateTocField()
        {
            if (this.Document != null)
            {
                RadFlowDocumentEditor editor = new RadFlowDocumentEditor(this.Document);

                string code = "TOC";

                if (this.IncludeHeadings)
                {
                    code += string.Format(" \\o \"{0}\"", this.HeadingLevel);
                }

                if (this.IncludeTcFields)
                {
                    code += string.Format(" \\l {0}", this.TcLevel);
                }

                if (this.IncludeCaptions)
                {
                    code += string.Format(" \\c \"{0}\"", this.Caption);
                }

                Paragraph firstParagraph = this.document.EnumerateChildrenOfType<Paragraph>().FirstOrDefault();
                editor.MoveToParagraphStart(firstParagraph);
                Run title = editor.InsertText("Table of Contents:");
                Telerik.Windows.Documents.Flow.Model.Styles.Style style = this.Document.StyleRepository.AddBuiltInStyle(BuiltInStyleNames.TocHeadingStyleId);
                title.Paragraph.StyleId = style.Id;

                editor.InsertParagraph();
                FieldInfo fieldInfo = editor.InsertField(code);

                editor.InsertParagraph();
                editor.InsertBreak(BreakType.PageBreak);

                fieldInfo.UpdateField();
            }
        }

        private void InsertAndUpdateToaField()
        {
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(this.document);

            string code = string.Format("TOA \\c \"{0}\"", this.CategoryLevel);

            if (this.IncludeCategoryHeader)
            {
                code += string.Format(" \\h");
            }

            Paragraph firstParagraph = this.document.EnumerateChildrenOfType<Paragraph>().FirstOrDefault();
            editor.MoveToParagraphStart(firstParagraph);
            editor.InsertParagraph();
            FieldInfo fieldInfo = editor.InsertField(code);

            editor.InsertParagraph();
            editor.InsertBreak(BreakType.PageBreak);

            fieldInfo.UpdateField();
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
    }
}
