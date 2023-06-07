using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample;

public class ImportViewModel : ExampleViewModel
{
    public ImportViewModel()
    {
        this.SampleResources = new ObservableCollection<SampleResource>()
        {
            new SampleResource() { ResourceFileType = "DOCX", ResourceFileName = "RichTextEditor_Overview.docx", ResourceIconPath = "import_word.png" },
            new SampleResource() { ResourceFileType = "HTML", ResourceFileName = "RichTextEditor_Overview.html", ResourceIconPath = "import_html.png" },
            new SampleResource() { ResourceFileType = "RTF", ResourceFileName = "RichTextEditor_Overview.rtf", ResourceIconPath = "import_text.png" },
            new SampleResource() { ResourceFileType = "TXT", ResourceFileName = "RichTextEditor_Overview.txt", ResourceIconPath = "import_text.png" }
        };

        this.OpenCommand = new Command<string>(OpenSampleFile);
    }

    public ICommand OpenCommand { get; }

    public ObservableCollection<SampleResource> SampleResources { get; }

    private void OpenSampleFile(string resourceName)
    {
        NavigationHelper.Instance.NavigateToExportView(resourceName);
    }
}
