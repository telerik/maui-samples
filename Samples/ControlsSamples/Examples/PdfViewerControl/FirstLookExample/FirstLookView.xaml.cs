using Microsoft.Maui.Controls.Xaml;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Maui.Controls;

namespace QSF.Examples.PdfViewerControl.FirstLookExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLookView : RadContentView
    {
        public FirstLookView()
        {
            InitializeComponent();
            Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
            {
                Assembly assembly = typeof(FirstLookView).Assembly;
                string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdfviewer-firstlook.pdf"));
                Stream stream = assembly.GetManifestResourceStream(fileName);
                return stream;
            });
            this.pdfViewer.Source = streamFunc;
        }
    }
}