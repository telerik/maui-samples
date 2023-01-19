using QSF.Examples.WordsProcessingControl.Converters;
using Telerik.Maui.Controls;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Flow.Extensibility;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;

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