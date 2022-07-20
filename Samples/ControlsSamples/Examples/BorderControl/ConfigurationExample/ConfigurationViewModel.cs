using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui;
using QSF.ViewModels;

namespace QSF.Examples.BorderControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
		private string selectedBorderColor;

        private double leftCornerRadius = 75;
        private double topCornerRadius = 75;
        private double rightCornerRadius = 75;
        private double bottomCornerRadius = 75;

        private string leftCornerRadiusLabelText;
        private string topCornerRadiusLabelText;
        private string rightCornerRadiusLabelText;
        private string bottomCornerRadiusLabelText;

        private Thickness borderCornerRadius;
        private Thickness borderThickness = new Thickness(15);

        public string Avatar { get; set; }

        public ConfigurationViewModel()
        {
            this.BorderColors = new List<string>
                {
                    "Black",
                    "Red",
                    "Green",
                    "Blue",
                    "Cyan",
                    "Magenta",
                    "Yellow",
                    "White"
                };

            this.Avatar = "borderconfigurationavatar.png";
            this.SelectedBorderColor = this.BorderColors.First();
        }

        public IList<string> BorderColors { get; set; }

        public string SelectedBorderColor
        {
            get
            {
                return this.selectedBorderColor;
            }
            set
            {
                this.UpdateValue(ref this.selectedBorderColor, value);
            }
        }

        public double LeftCornerRadius
        {
            get
            {
                return this.leftCornerRadius;
            }
            set
            {
                this.UpdateValue(ref this.leftCornerRadius, value);
            }
        }

        public double TopCornerRadius
        {
            get
            {
                return this.topCornerRadius;
            }
            set
            {
                this.UpdateValue(ref this.topCornerRadius, value);
            }
        }

        public double RightCornerRadius
        {
            get
            {
                return this.rightCornerRadius;
            }
            set
            {
                this.UpdateValue(ref this.rightCornerRadius, value);
            }
        }

        public double BottomCornerRadius
        {
            get
            {
                return this.bottomCornerRadius;
            }
            set
            {
                this.UpdateValue(ref this.bottomCornerRadius, value);
            }
        }

        public string LeftCornerRadiusLabelText
        {
            get
            {
                return this.leftCornerRadiusLabelText;
            }
            set
            {
                this.UpdateValue(ref this.leftCornerRadiusLabelText, value);
            }
        }

        public string TopCornerRadiusLabelText
        {
            get
            {
                return this.topCornerRadiusLabelText;
            }
            set
            {
                this.UpdateValue(ref this.topCornerRadiusLabelText, value);
            }
        }

        public string RightCornerRadiusLabelText
        {
            get
            {
                return this.rightCornerRadiusLabelText;
            }
            set
            {
                this.UpdateValue(ref this.rightCornerRadiusLabelText, value);
            }
        }

        public string BottomCornerRadiusLabelText
        {
            get
            {
                return this.bottomCornerRadiusLabelText;
            }
            set
            {
                this.UpdateValue(ref this.bottomCornerRadiusLabelText, value);
            }
        }

        public Thickness BorderCornerRadius
        {
            get
            {
                return this.borderCornerRadius;
            }
            set
            {
                this.UpdateValue(ref this.borderCornerRadius, value);
            }
        }

        public Thickness BorderThickness
        {
            get
            {
                return this.borderThickness;
            }
            set
            {
                this.UpdateValue(ref this.borderThickness, value);
            }
        }
    }
}
