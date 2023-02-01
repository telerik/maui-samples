using QSF.ViewModels;
using System.Collections.Generic;

namespace QSF.Examples.EntryControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private List<string> keyboards;
        private List<string> textColors;
        private List<string> fontSizes;
        private string selectedKeyboard;
        private string textColor;
        private string selectedColor;
        private string selectedFontSize;

        public ConfigurationViewModel()
        {
            this.keyboards = new List<string>()
            {
                "Default",
                "Email",
                "Numeric",
                "Telephone",
                "Url"
            };

            this.textColors = new List<string>()
            {
                "Default",
                "Gray",
                "Orange",
                "Blue",
                "Pink"
            };

            this.fontSizes = new List<string>()
            {
                "Default",
                "Large",
                "Small"
            };

            this.SelectedKeyboard = this.keyboards[0];
            this.SelectedTextColor = this.textColors[0];
            this.SelectedFontSize = this.fontSizes[0];
        }

        public string SelectedKeyboard
        {
            get
            {
                return this.selectedKeyboard;
            }
            set
            {
                this.UpdateValue(ref this.selectedKeyboard, value);
            }
        }

        public string TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                if (value == "Default")
                {
                    this.UpdateValue(ref this.textColor, null);
                }
                else
                {
                    this.UpdateValue(ref this.textColor, value);
                }
            }
        }

        public string SelectedTextColor
        {
            get
            {
                return this.selectedColor;
            }
            set
            {
                this.UpdateValue(ref this.selectedColor, value);
                this.TextColor = value;
            }
        }

        public string SelectedFontSize
        {
            get
            {
                return this.selectedFontSize;
            }
            set
            {
                this.UpdateValue(ref this.selectedFontSize, value);
            }
        }
        public List<string> Keyboards
        {
            get
            {
                return this.keyboards;
            }
        }

        public List<string> TextColors
        {
            get
            {
                return this.textColors;
            }
        }

        public List<string> FontSizes
        {
            get
            {
                return this.fontSizes;
            }
        }
    }
}
