using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Telerik.Documents.Fixed.Model;
using Telerik.Documents.Fixed.Model.Actions;
using Telerik.Documents.Fixed.Model.Annotations;
using Telerik.Documents.Fixed.Model.Editing;
using Telerik.Documents.Primitives;

namespace SDKBrowserMaui.Examples.PdfViewerControl.FeaturesCategory.LinkAnnotationCustomSchemeExample;

public partial class LinkAnnotationCustomScheme : ContentView
{
    public LinkAnnotationCustomScheme()
    {
        InitializeComponent();

        this.pdfViewer.Source = CreateDocument();
    }

    // >> pdfviewer-annotations-customscheme-event
    // By default, the PdfViewer launches only http, https, mailto and tel links.
    // Handle the event to take over links with other schemes, such as the app's own deep links.
    private async void LinkTapped(object sender, Telerik.Maui.Controls.PdfViewer.Annotations.LinkAnnotationTappedEventArgs e)
    {
        var action = e.LinkAnnotation.Actions?.FirstOrDefault();
        if (action is UriAction { Uri: { IsAbsoluteUri: true } uri } && uri.Scheme == "myapp")
        {
            e.Handled = true;

            await Application.Current.Windows[0].Page.DisplayAlertAsync("Custom link", $"The app handles: {uri}", "OK");
        }
    }
    // << pdfviewer-annotations-customscheme-event

    private static RadFixedDocument CreateDocument()
    {
        RadFixedDocument document = new RadFixedDocument();
        RadFixedPage page = document.Pages.AddPage();
        page.Size = new Size(600, 400);

        FixedContentEditor editor = new FixedContentEditor(page);
        editor.Position.Translate(50, 50);
        editor.DrawText("Tap this page: myapp://open (handled by the app).");

        Link link = page.Annotations.AddLink(new UriAction(new Uri("myapp://open")));
        link.Rect = new Rect(50, 40, 500, 40);

        return document;
    }
}
