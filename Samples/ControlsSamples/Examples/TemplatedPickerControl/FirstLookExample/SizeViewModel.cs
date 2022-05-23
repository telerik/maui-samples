using QSF.ViewModels;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;

namespace QSF.Examples.TemplatedPickerControl.FirstLookExample
{
    public class SizeViewModel : NotifyPropertyChangedBase
    {
        private string name;
        private Color backgroundColor;
        private Color textColor;

        public SizeViewModel(string name, string backgroundColor, string textColor)
            : this(name, Color.FromArgb(backgroundColor), Color.FromArgb(textColor))
        {
        }

        public SizeViewModel(string name, Color backgroundColor, Color textColor)
        {
            this.name = name;
            this.backgroundColor = backgroundColor;
            this.textColor = textColor;
        }

        public SizeViewModel(string name)
        {
            this.name = name;
            this.backgroundColor = Color.FromArgb("#EAEAEA");
            this.textColor = Colors.Black;
        }

        public string Name
        {
            get => this.name; 
            set => UpdateValue(ref this.name, value);
        }

        public Color BackgroundColor
        {
            get => this.backgroundColor; 
            set => UpdateValue(ref this.backgroundColor, value);
        }

        public Color TextColor
        {
            get => this.textColor;
            set => UpdateValue(ref this.textColor, value);
        }
    }
}
