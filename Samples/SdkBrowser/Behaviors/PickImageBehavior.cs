using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.RichTextEditor;

namespace SDKBrowserMaui.Behaviors
{
    public class PickImageBehavior : Behavior<RadRichTextEditor>
    {
        protected override void OnAttachedTo(RadRichTextEditor richTextEditor)
        {
            base.OnAttachedTo(richTextEditor);

            richTextEditor.PickImage += OnPickImage;
        }

        protected override void OnDetachingFrom(RadRichTextEditor richTextEditor)
        {
            base.OnDetachingFrom(richTextEditor);

            richTextEditor.PickImage -= OnPickImage;
        }

        private static async void OnPickImage(object sender, PickImageEventArgs eventArgs)
        {
            var fileResult = await MediaPicker.Default.PickPhotoAsync();
            var filePath = fileResult?.FullPath;

            if (!string.IsNullOrEmpty(filePath))
            {
                var imageSource = RichTextImageSource.FromFile(filePath);

                eventArgs.Accept(imageSource);
            }
            else
            {
                eventArgs.Cancel();
            }
        }
    }
}
