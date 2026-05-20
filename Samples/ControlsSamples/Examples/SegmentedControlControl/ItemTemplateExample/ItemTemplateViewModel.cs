using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.Common;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace QSF.Examples.SegmentedControlControl.ItemTemplateExample;

public class ItemTemplateViewModel : ExampleViewModel
{
    private readonly static Dictionary<string, string> TextAndIcons = new Dictionary<string, string>()
    {
        { "Button", TelerikControlsIcons.GetIcon("Button") },
        { "Slider", TelerikControlsIcons.GetIcon("Slider") },
        { "RangeSlider", TelerikControlsIcons.GetIcon("RangeSlider") },
    };

    private readonly static Dictionary<string, string> TextAndImagePaths = new Dictionary<string, string>()
    {
        { "Germany", "flag_6.png" },
        { "France", "flag_5.png" },
        { "USA", "flag_4.png" },
    };

    public ItemTemplateViewModel()
    {
        this.TextItems = new ObservableCollection<ItemModel>(TextAndIcons.Select(x => new ItemModel() { Text = x.Key }));
        this.IconItems = new ObservableCollection<ItemModel>(TextAndIcons.Select(x => new ItemModel() { ImageSource = this.GetFontImageSource(x.Value) }));
        this.IconAndTextItems = new ObservableCollection<ItemModel>(TextAndIcons.Select(x => new ItemModel() { Text = x.Key, ImageSource = this.GetFontImageSource(x.Value) }));
        this.ImageItems = new ObservableCollection<ItemModel>(TextAndImagePaths.Select(x => new ItemModel() { ImageSource = ImageSource.FromFile(x.Value) }));
        this.ImageAndTextItems = new ObservableCollection<ItemModel>(TextAndImagePaths.Select(x => new ItemModel() { Text = x.Key, ImageSource = ImageSource.FromFile(x.Value) }));
    }

    public ObservableCollection<ItemModel> TextItems { get; set; }

    public ObservableCollection<ItemModel> IconItems { get; set; }

    public ObservableCollection<ItemModel> IconAndTextItems { get; set; }

    public ObservableCollection<ItemModel> ImageItems { get; set; }

    public ObservableCollection<ItemModel> ImageAndTextItems { get; set; }

    private FontImageSource GetFontImageSource(string glyph)
    {
        return new FontImageSource
        {
            Glyph = glyph,
            FontFamily = "TelerikControlsIcons"
        };
    }
}
