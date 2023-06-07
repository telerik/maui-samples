using Telerik.Maui.Controls;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample;

internal class NavigationHelper
{
    public static NavigationHelper Instance { get; private set; }

    static NavigationHelper()
    {
        Instance = new NavigationHelper();
    }

    private NavigationHelper()
    {
    }

    public RadContentView ImportExport { get; set; }

    public void NavigateToImportView()
    {
        if (this.ImportExport == null)
        {
            return;
        }

        this.ImportExport.Content = new ImportView() { BindingContext = new ImportViewModel() };
    }

    public void NavigateToExportView(string documentPath)
    {
        if (this.ImportExport == null)
        {
            return;
        }

        this.ImportExport.Content = new ExportView() { BindingContext = new ExportViewModel(documentPath) };
    }
}
