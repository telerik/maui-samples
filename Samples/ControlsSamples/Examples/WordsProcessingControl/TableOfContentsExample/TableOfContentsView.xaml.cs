using QSF.Examples.WordsProcessingControl.Converters;
using Telerik.Maui.Controls;
using Telerik.Documents.Extensibility;
using Telerik.Documents.Flow.Extensibility;
using Telerik.Documents.Flow.FormatProviders.Pdf;

namespace QSF.Examples.WordsProcessingControl.TableOfContentsExample;

public partial class TableOfContentsView : RadContentView
{
    public TableOfContentsView()
    {
        FlowExtensibilityManager.NumberingFieldsProvider = new NumberingFieldsProvider();
        FixedExtensibilityManager.JpegImageConverter = new SkiaImageConverter();

        InitializeComponent();
    }
}