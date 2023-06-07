using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ImportExportView : RadContentView
{
	public ImportExportView()
	{
		InitializeComponent();
        NavigationHelper.Instance.ImportExport = this;
        NavigationHelper.Instance.NavigateToImportView();
    }
}