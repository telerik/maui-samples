using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QSF.Examples.GridSplitterControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private ObservableCollection<ImageMetadataPropertyItem> imageDetails;

    public FirstLookViewModel()
    {
        this.CreateImages();
        this.CreateImageDetails();
        this.SelectedImage = this.Images[1];
    }

    public ObservableCollection<ImageMetadataPropertyItem> ImageDetails
    {
        get => this.imageDetails;
        set => this.UpdateValue(ref this.imageDetails, value);
    }

    public List<PropertyItemBaseInfo> Images { get; private set; }

    public Dictionary<PropertyItemBaseInfo, ObservableCollection<ImageMetadataPropertyItem>> ImageDetailsDictionary { get; private set; }

    public PropertyItemBaseInfo SelectedImage { get; set; }

    private void CreateImages()
    {
        this.Images = new List<PropertyItemBaseInfo>
        {
            new PropertyItemBaseInfo() { Title = "property_item_1.png", Size = "131", Tags = "Pampanga, Philippines" },
            new PropertyItemBaseInfo() { Title = "property_item_2.png", Size = "104", Tags = "Mykonos, Greece" },
            new PropertyItemBaseInfo() { Title = "property_item_3.png", Size = "126", Tags = "Rovinj, Croatia" },
            new PropertyItemBaseInfo() { Title = "property_item_4.png", Size = "91", Tags = "Paris, France" },
            new PropertyItemBaseInfo() { Title = "property_item_5.png", Size = "111", Tags = "Limbe, Cameroon" },
            new PropertyItemBaseInfo() { Title = "property_item_6.png", Size = "136", Tags = "Phuket, Thailand" },
            new PropertyItemBaseInfo() { Title = "property_item_7.png", Size = "115", Tags = "California, USA" },
            new PropertyItemBaseInfo() { Title = "property_item_8.png", Size = "136", Tags = "Piacenza, Italy" },
            new PropertyItemBaseInfo() { Title = "property_item_9.png", Size = "155", Tags = "San Blas, Panama" },
            new PropertyItemBaseInfo() { Title = "property_item_10.png", Size = "124", Tags = "California, USA" }
        };
    }

    private void CreateImageDetails()
    {
        this.ImageDetailsDictionary = new Dictionary<PropertyItemBaseInfo, ObservableCollection<ImageMetadataPropertyItem>>();

        foreach (var image in this.Images)
        {
            ObservableCollection<ImageMetadataPropertyItem> imageMetadata = new ObservableCollection<ImageMetadataPropertyItem>();

            imageMetadata.Add(new ImageMetadataPropertyItem() { Title = "Name", Value = image.Title } );
            imageMetadata.Add(new ImageMetadataPropertyItem() { Title = "Created", Value = "24 July 2024 at 18:04" } );
            imageMetadata.Add(new ImageMetadataPropertyItem() { Title = "Modified", Value = "24 July 2024 at 18:04" } );
            imageMetadata.Add(new ImageMetadataPropertyItem() { Title = "Last Opened", Value = "Today 12:36 pm" } );
            imageMetadata.Add(new ImageMetadataPropertyItem() { Title = "Dimensions", Value = "300x200" } );
            imageMetadata.Add(new ImageMetadataPropertyItem() { Title = "Size", Value = $"{image.Size}KB" } );
            imageMetadata.Add(new ImageMetadataPropertyItem() { Title = "Resolution", Value = "72x72" } );
            imageMetadata.Add(new ImageMetadataPropertyItem() { Title = "Color Space", Value = "RGB" } );
            imageMetadata.Add(new ImageMetadataPropertyItem() { Title = "Tags", Value = image.Tags } );
            imageMetadata.Add(new ImageMetadataPropertyItem() { Title = "Where", Value = "Resources/Images" } );

            this.ImageDetailsDictionary.Add(image, imageMetadata);
        };
    }
}