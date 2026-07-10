using Microsoft.Datasync.Client;
using TelerikCRM.Maui.Models.DataService;

namespace TelerikCRM.Maui.Selectors;

public class SearchResultItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate EmployeeTemplate { get; set; }

    public DataTemplate CustomerTemplate { get; set; }

    public DataTemplate ProductTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var result = (DatasyncClientData)item;

        return result switch
        {
            Employee => this.EmployeeTemplate,
            Customer => this.CustomerTemplate,
            Product => this.ProductTemplate,
            _ => base.SelectTemplate(item, container)
        };
    }
}
