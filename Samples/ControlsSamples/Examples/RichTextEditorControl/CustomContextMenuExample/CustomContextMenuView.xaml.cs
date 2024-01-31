using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.Services;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.RichTextEditor;

namespace QSF.Examples.RichTextEditorControl.CustomContextMenuExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomContextMenuView : RadContentView
{
    public CustomContextMenuView()
    {
        this.InitializeComponent();

        var resourceService = DependencyService.Get<IResourceService>();
        this.richTextEditor.Source = RichTextSource.FromStream(() => resourceService.GetResourceStream("BarcelonaAndTenerife.html"));
    }
}
