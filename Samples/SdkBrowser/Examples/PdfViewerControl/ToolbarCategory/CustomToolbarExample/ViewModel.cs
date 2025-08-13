using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Windows.Input;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;

namespace SDKBrowserMaui.Examples.PdfViewerControl.ToolbarCategory.CustomToolbarExample
{
    // >> pdfviewer-toolbar-customcommand-vm
    public class ViewModel
    {
        public ViewModel()
        {
            this.DisplayFileSizeCommand = new Command(this.DisplayFileSizeCommandExecute);
        }

        public RadFixedDocument Document { get; set; }
        public ICommand DisplayFileSizeCommand { get; set; }

        protected void DisplayFileSizeCommandExecute(object para)
        {
            RadFixedDocument document = this.Document;
            if (document != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    PdfFormatProvider formatProvider = new PdfFormatProvider();
                    formatProvider.Export(document, stream, TimeSpan.FromMinutes(1));
                    double megabytes = ToKiloBytes(stream.Length);
                    Application.Current.Windows[0].Page.DisplayAlert("", "File Size: " + megabytes.ToString("N0") + " KB", "OK");
                }
            }
        }

        private static double ToKiloBytes(long bytesCount)
        {
            return (double)bytesCount / 1024;
        }
    }
    // << pdfviewer-toolbar-customcommand-vm
}
