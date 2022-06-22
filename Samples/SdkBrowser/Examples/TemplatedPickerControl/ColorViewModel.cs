using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Common;

namespace SDKBrowserMaui.Examples.TemplatedPickerControl
{
    // >> templatedpicker-color-viewmodel
    public class ColorViewModel : NotifyPropertyChangedBase
    {
        private Color selectedColor;
        private string selectedValue;

        public ColorViewModel()
        {
            this.Colors = new ObservableCollection<Color>
            {
                Color.FromArgb("#FFFFFF"),
                Color.FromArgb("#EE534F"),
                Color.FromArgb("#AB47BC"),
                Color.FromArgb("#7E57C2"),
                Color.FromArgb("#5D6BC0"),
                Color.FromArgb("#42A5F5"),
                Color.FromArgb("#26C5DA"),
                Color.FromArgb("#24A79A"),
                Color.FromArgb("#66BB6A"),
                Color.FromArgb("#9CCC65"),
                Color.FromArgb("#D4E157"),
                Color.FromArgb("#FFEE58"),
                Color.FromArgb("#FFCA29"),
                Color.FromArgb("#FFA726"),
                Color.FromArgb("#FF7043"),
                Color.FromArgb("#8D6E63"),
                Color.FromArgb("#BDBDBD"),
                Color.FromArgb("#78909C"),
                Color.FromArgb("#3C3C3C"),
                Color.FromArgb("#000000")
            };
        }

        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                if (this.UpdateValue(ref this.selectedColor, value))
                {
                    this.SelectedValue = this.selectedColor?.ToHex();
                }
            }
        }

        public string SelectedValue
        {
            get => selectedValue;
            private set => this.UpdateValue(ref this.selectedValue, value);
        }

        public ObservableCollection<Color> Colors { get; }
    }
    // << templatedpicker-color-viewmodel
}
