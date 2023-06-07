using System.Threading.Tasks;

namespace QSF.Examples.RichTextEditorControl.ImportExportExample;

public interface IRichTextContext
{
    Task<string> GetHtmlAsync();
}
