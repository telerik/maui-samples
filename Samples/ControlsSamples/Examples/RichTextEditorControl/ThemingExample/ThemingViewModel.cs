using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using Telerik.Maui.Controls.RichTextEditor;

namespace QSF.Examples.RichTextEditorControl.ThemingExample;

public class ThemingViewModel : ExampleViewModel
{
    private readonly IResourceService resourceService;

    public ThemingViewModel()
    {
        this.resourceService = DependencyService.Get<IResourceService>();
        this.Source = RichTextSource.FromStream(() => this.resourceService.GetResourceStream("PickYourHoliday.html"));
    }

    public RichTextSource Source { get; set; }
}
