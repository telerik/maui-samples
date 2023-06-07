using Microsoft.Maui.Controls;
using QSF.Services;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.RichTextEditor;

namespace QSF.Behaviors
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
            var mediaPickerService = DependencyService.Get<IMediaPickerService>();
            var filePath = await mediaPickerService.PickPhotoAsync();

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
