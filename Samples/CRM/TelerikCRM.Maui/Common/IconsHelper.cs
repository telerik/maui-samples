namespace TelerikCRM.Maui.Common;

internal static class IconsHelper
{
    private static readonly Dictionary<string, string> PagesIcons = new()
    {
        { "Home", char.ConvertFromUtf32(0xe85b) },
        { "Employees", char.ConvertFromUtf32(0xe8a3) },
        { "Customers", char.ConvertFromUtf32(0xe8a8) },
        { "Products", char.ConvertFromUtf32(0xe8a7) },
        { "Orders", char.ConvertFromUtf32(0xe826) },
        { "Shipping", char.ConvertFromUtf32(0xe8a6) },
        { "About", char.ConvertFromUtf32(0xe8ac) },
        { "More", char.ConvertFromUtf32(0xe807) }
    };

    public static string GetPageIcon(string name)
    {
        return PagesIcons.TryGetValue(name, out var icon) 
            ? icon 
            : string.Empty;
    }
}
