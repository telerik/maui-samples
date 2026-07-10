using TelerikCRM.Maui.Common;

namespace TelerikCRM.Maui.Models;

public class PageModel
{
    public Type Type { get; set; }
    public string Title { get; set; }
    public string Icon => IconsHelper.GetPageIcon(this.Title);
}