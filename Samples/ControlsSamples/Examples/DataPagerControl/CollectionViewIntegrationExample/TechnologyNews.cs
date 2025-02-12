using QSF.ViewModels;

namespace QSF.Examples.DataPagerControl.CollectionViewIntegrationExample;

public class TechnologyNews : ViewModelBase
{
    private string image;
    private string title;
    private string description;

    public string Image
    {
        get => this.image;
        set => this.UpdateValue(ref this.image, value);
    }

    public string Title
    {
        get => this.title;
        set => this.UpdateValue(ref this.title, value);
    }

    public string Description
    {
        get => this.description;
        set => this.UpdateValue(ref this.description, value);
    }
}