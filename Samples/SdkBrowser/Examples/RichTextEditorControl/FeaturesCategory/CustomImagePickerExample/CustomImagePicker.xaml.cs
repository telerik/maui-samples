using Microsoft.Maui.Controls;
using SDKBrowserMaui.Behaviors;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.FeaturesCategory.CustomImagePickerExample;

public partial class CustomImagePicker : ContentView
{
    public CustomImagePicker()
    {
        InitializeComponent();

        this.BindingContext = new ViewModel();
        this.grid.Behaviors.Add(new AndroidKeyboardPaddingBehavior());
    }
}