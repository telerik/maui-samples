using QSF.ViewModels;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls.Compatibility.Common;
using Telerik.Maui.Controls;

namespace QSF.Examples.TemplatedPickerControl.FirstLookExample
{
    public class ColorViewModel : NotifyPropertyChangedBase
    {
        private string name;
        private Color color;
        private Color borderColor;

        public ColorViewModel(string name, string color)
            : this(name, Color.FromArgb(color))
        {
        }

        public ColorViewModel(string name, Color color)
        {
            this.name = name;
            this.color = color;
            this.borderColor = Colors.Transparent;
        }

        public string Name
        {
            get => this.name; 
            set => UpdateValue(ref this.name, value);
        }

        public Color Color
        {
            get => this.color; 
            set => UpdateValue(ref this.color, value);
        }

        public Color BorderColor
        {
            get => this.borderColor; 
            set => UpdateValue(ref this.borderColor, value);
        }
    }
}
