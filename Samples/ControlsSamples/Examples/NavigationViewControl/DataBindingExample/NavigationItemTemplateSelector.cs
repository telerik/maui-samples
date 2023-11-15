using Microsoft.Maui.Controls;

namespace QSF.Examples.NavigationViewControl.DataBindingExample;

public class NavigationItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate CountTemplate { get; set; }

    public DataTemplate DefaultTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return ((MailBox)item).HasMail ? CountTemplate : DefaultTemplate;
    }
}
