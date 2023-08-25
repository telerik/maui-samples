using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.SlideViewControl.TemplatesCategory.ItemTemplateExample;

public class ItemTemplateViewModel
{
    public ObservableCollection<CustomItem> Items { get; set; }

    public ItemTemplateViewModel()
    {
        this.Items = new ObservableCollection<CustomItem>()
        {
            new CustomItem() { IconUnicode = "\ue84a", DescriptionText = "The .NET MAUI SlideView has so much more to show than just images." },
            new CustomItem() { IconUnicode = "\ue848", DescriptionText = "Create stunning designs for your data's visual representation." },
            new CustomItem() { IconUnicode = "\ue84c", DescriptionText = "Bring your masterpieces to life through the slide view's ItemTemplate." },
        };
    }
}

public class CustomItem
{
    public string IconUnicode { get; set;}
    public string DescriptionText { get; set;}
    
}