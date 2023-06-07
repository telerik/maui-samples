using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Fixed.Model.Actions;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace SDKBrowserMaui.Examples.PdfViewerControl.FeaturesCategory.LinkAnnotationExample;

public partial class LinkAnnotation : ContentView
{
	public LinkAnnotation()
	{
		InitializeComponent();

        // >> pdfviewer-annotations-setsource
        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(LinkAnnotation).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("pdf-overview.pdf"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });
        this.pdfViewer.Source = streamFunc;
        // << pdfviewer-annotations-setsource         

    }

    // >> pdfviewer-annotations-event
    private void LinkTapped(object sender, Telerik.Maui.Controls.PdfViewer.Annotations.LinkAnnotationTappedEventArgs e)
    {
        if (e.LinkAnnotation.Action is UriAction uriAction)
        {
            e.Handled = true;

            Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure you want to navigate", "Yes", "No").ContinueWith(t =>
            {
                bool shouldNavigateAway = t.Status == TaskStatus.RanToCompletion ? t.Result : false;
                if (shouldNavigateAway)
                {
                    Dispatcher.Dispatch(() =>
                    {
                        Launcher.OpenAsync(uriAction.Uri);
                    });
                }
            });
        }
    }
    // << pdfviewer-annotations-event
}