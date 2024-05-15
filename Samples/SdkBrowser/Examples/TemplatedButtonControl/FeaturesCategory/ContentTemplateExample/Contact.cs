using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TemplatedButtonControl.FeaturesCategory.ContentTemplateExample;

public class Contact : NotifyPropertyChangedBase
{
    private bool isFavorite;

    public string Name { get; set; }

    public bool IsFavorite
    {
        get => this.isFavorite;
        set => this.UpdateValue(ref this.isFavorite, value);
    }

    public Microsoft.Maui.Controls.Command UpdateIsFavoriteCommand => new Microsoft.Maui.Controls.Command((object param) =>
    {
        if (param is Contact contact)
        {
            contact.IsFavorite ^= true;
        }
    });
}
