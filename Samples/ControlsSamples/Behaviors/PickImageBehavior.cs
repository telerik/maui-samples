using Microsoft.Maui.Controls;
using QSF.Services;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
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
            var fileResult = await mediaPickerService.PickPhotoAsync();

            if (fileResult != null)
            {
#if NET10_0_OR_GREATER && (IOS || MACCATALYST)
                var imageType = GetImageType(fileResult.FileName);

                RichTextImageSource imageSource;
                if (imageType == null)
                {
                    var jpegBytes = await TranscodeToJpegAsync(fileResult);
                    imageSource = jpegBytes != null ? RichTextImageSource.FromStream(new MemoryStream(jpegBytes), RichTextImageType.Jpeg) : null;
                }
                else
                {
                    imageSource = RichTextImageSource.FromStream(() => fileResult.OpenReadAsync(), imageType.Value);
                }
#else
                var imageSource = RichTextImageSource.FromFile(fileResult.FullPath);
#endif
                if (imageSource != null)
                {
                    eventArgs.Accept(imageSource);
                }
                else
                {
                    eventArgs.Cancel();
                }
            }
            else
            {
                eventArgs.Cancel();
            }
        }

#if NET10_0_OR_GREATER && (IOS || MACCATALYST)
        private static bool IsImageExtension(string extension)
        {
            return extension?.ToLowerInvariant() is ".heic" or ".heif" or ".jpg" or ".jpeg" or ".png" or ".gif" or ".webp" or ".bmp" or ".tiff" or ".tif";
        }

        private static RichTextImageType? GetImageType(string fileName)
        {
            return Path.GetExtension(fileName)?.ToLowerInvariant() switch
            {
                ".gif" => RichTextImageType.Gif,
                ".jpg" or ".jpeg" => RichTextImageType.Jpeg,
                ".png" => RichTextImageType.Png,
                ".svg" => RichTextImageType.Svg,
                ".webp" => RichTextImageType.Webp,
                _ => null,
            };
        }

        private static async Task<byte[]> TranscodeToJpegAsync(Microsoft.Maui.Storage.FileResult fileResult)
        {
            try
            {
                using var stream = await fileResult.OpenReadAsync();

                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);

                byte[] imageData;

                // PHPicker on iOS returns .pvt files which are ZIP archives containing the image.
                if (Path.GetExtension(fileResult.FileName)?.Equals(".pvt", System.StringComparison.OrdinalIgnoreCase) == true)
                {
                    ms.Position = 0;
                    using var archive = new ZipArchive(ms, ZipArchiveMode.Read, leaveOpen: true);
                    var entry = archive.Entries.FirstOrDefault(e => IsImageExtension(Path.GetExtension(e.Name))) ?? archive.Entries.FirstOrDefault();
                    if (entry == null)
                    {
                        return null;
                    }

                    using var entryStream = entry.Open();
                    using var entryMs = new MemoryStream();
                    await entryStream.CopyToAsync(entryMs);
                    imageData = entryMs.ToArray();
                }
                else
                {
                    imageData = ms.ToArray();
                }

                using var nsData = Foundation.NSData.FromArray(imageData);
                using var uiImage = UIKit.UIImage.LoadFromData(nsData);
                if (uiImage == null)
                {
                    return null;
                }

                return uiImage.AsJPEG(0.9f)?.ToArray();
            }
            catch
            {
                return null;
            }
        }
#endif
    }
}
