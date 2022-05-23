using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace QSF.Examples.ZipLibraryControl.CreateArchiveExample.Converters
{
    public class FileExtensionToIconConverter : IValueConverter
    {
        private const string XlsxIcon = "xlsx_file.png";
        private const string DocxIcon = "docx_file.png";
        private const string PdfIcon = "pdf_file.png";
        private const string PngIcon = "png_file.png";

        private static readonly Dictionary<string, string> fileExtensionToIconPath;

        static FileExtensionToIconConverter()
        {
            fileExtensionToIconPath = new Dictionary<string, string>();
            fileExtensionToIconPath.Add(".xlsx", XlsxIcon);
            fileExtensionToIconPath.Add(".docx", DocxIcon);
            fileExtensionToIconPath.Add(".pdf", PdfIcon);
            fileExtensionToIconPath.Add(".png", PngIcon);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string extension = (string)value;

            string iconPath = fileExtensionToIconPath[extension.ToLower()];

            return iconPath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
