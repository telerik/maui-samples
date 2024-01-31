using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Examples.PdfViewerControl.SelectionCategory.TextSelectionExample;

public partial class TextSelection : ContentView
{
    public TextSelection()
    {
        InitializeComponent();
        // >> pdfviewer-textselection-setvm
        this.BindingContext = new ViewModel();
        // << pdfviewer-textselection-setvm

        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(TextSelection).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf-overview.pdf"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });

        this.pdfViewer.Source = streamFunc;
    }
}