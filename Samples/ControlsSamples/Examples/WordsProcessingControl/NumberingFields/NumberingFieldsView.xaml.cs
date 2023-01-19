using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.Examples.WordsProcessingControl.Converters;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Flow.Extensibility;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;

namespace QSF.Examples.WordsProcessingControl.NumberingFieldsExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumberingFieldsView : RadContentView
    {
        public NumberingFieldsView()
        {
            FlowExtensibilityManager.NumberingFieldsProvider = new NumberingFieldsProvider();
            FixedExtensibilityManager.FontsProvider = new FontsProvider();
            FixedExtensibilityManager.JpegImageConverter = new SkiaImageConverter();

            InitializeComponent();
        }
    }
}