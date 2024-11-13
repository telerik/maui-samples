using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Reflection;
using Telerik.Maui.Controls.RichTextEditor;

namespace QSF.Examples.RichTextEditorControl.CustomToolbarExample;

public class CustomToolbarViewModel : ExampleViewModel
{
    private readonly IResourceService resourceService;
    private List<RichTextImageSource> emojiSource;

    public CustomToolbarViewModel()
    {
        this.resourceService = DependencyService.Get<IResourceService>();
        this.emojiSource = new List<RichTextImageSource>();
        this.RTSource = RichTextSource.FromStream(() => this.resourceService.GetResourceStream("ShareYourFeedback.html"));
        this.InitializeImages();
    }

    public List<RichTextImageSource> EmojiSource
    {
        get => this.emojiSource;
        set => this.UpdateValue(ref this.emojiSource, value);
    }

    public RichTextSource RTSource { get; private set; }

    private void InitializeImages()
    {
        var assembly = typeof(CustomToolbarViewModel).GetTypeInfo().Assembly;
        var resourceNames = assembly.GetManifestResourceNames();
        var imageSources = new List<RichTextImageSource>();

        foreach (var resourceName in resourceNames)
        {
            if (resourceName.Contains("emoji"))
            {
                var imageSource = RichTextImageSource.FromStream(() =>
                    assembly.GetManifestResourceStream(resourceName), RichTextImageType.Png);

                imageSources.Add(imageSource);
            }
        }

        this.EmojiSource = imageSources;
    }
}