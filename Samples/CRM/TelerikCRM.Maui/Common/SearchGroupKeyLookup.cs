using Telerik.Maui.Controls.Data;

namespace TelerikCRM.Maui.Common;

public class SearchGroupKeyLookup : IKeyLookup
{
    public object GetKey(object instance)
        => instance.GetType().Name;
}