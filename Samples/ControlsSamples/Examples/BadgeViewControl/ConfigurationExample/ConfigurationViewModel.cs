using QSF.ViewModels;
using System;
using Telerik.Maui.Controls.Compatibility.Primitives;
using Microsoft.Maui;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;

namespace QSF.Examples.BadgeViewControl.ConfigurationExample
{
    public class ConfigurationViewModel : ExampleViewModel
    {
        private string badgeBackgroundColorName;
        private Color badgeBackgroundColor;
        private string badgeText = "2";
        private string badgeTextColorName;
        private Color badgeTextColor;
        private BadgePosition badgeHorizontalPosition = BadgePosition.End;
        private BadgePosition badgeVerticalPosition = BadgePosition.Start;
        private BadgeAlignment badgeHorizontalAlignment = BadgeAlignment.End;
        private BadgeAlignment badgeVerticalAlignment = BadgeAlignment.Center;
        private double offSetX = 0;
        private double offSetY = 0;
        private double textMarginLeft = 5;
        private double textMarginTop = 0;
        private double textMarginRight = 5;
        private double textMarginBottom = 0;
        private BadgeAnimationType badgeAnimationType = BadgeAnimationType.Scale;
        private Easing badgeAnimationEasing = Easing.SinInOut;
        private double badgeAnimationDuration = 300;
        private Dictionary<string, Color> nameToColorDictionary;

        public ConfigurationViewModel()
        {
            this.nameToColorDictionary = new Dictionary<string, Color>();

            this.nameToColorDictionary.Add("Red", Colors.Red);
            this.nameToColorDictionary.Add("Blue", Colors.Blue);
            this.nameToColorDictionary.Add("Yellow", Colors.Yellow);
            this.nameToColorDictionary.Add("Pink", Colors.Pink);
            this.nameToColorDictionary.Add("Orange", Colors.Orange);
            this.nameToColorDictionary.Add("White", Colors.White);
            this.nameToColorDictionary.Add("Black", Colors.Black);

            this.BadgeBackgroundColorName = "Red";
            this.BadgeTextColorName = "White";
        }

        public ObservableCollection<string> BadgeBackgroundColors { get; } = new ObservableCollection<string>()
        {
            "Red", "Blue", "Yellow", "Pink", "Orange"
        };
        public ObservableCollection<string> BadgeTextColors { get; } = new ObservableCollection<string>()
        {
            "White", "Black", "Yellow", "Pink", "Orange"
        };
        public ObservableCollection<BadgePosition> BadgePositions { get; } = new ObservableCollection<BadgePosition>()
        {
            BadgePosition.Start, BadgePosition.Center, BadgePosition.End
        };
        public ObservableCollection<BadgeAlignment> BadgeAlignments { get; } = new ObservableCollection<BadgeAlignment>()
        {
            BadgeAlignment.Start, BadgeAlignment.Center, BadgeAlignment.End
        };
        public ObservableCollection<BadgeAnimationType> BadgeAnimationTypes { get; } = new ObservableCollection<BadgeAnimationType>()
        {
            BadgeAnimationType.None, BadgeAnimationType.Scale
        };
        public ObservableCollection<string> BadgeAnimationEasings { get; } = new ObservableCollection<string>()
        {
            "Linear", "SinOut", "SinIn", "SinInOut", "CubicIn", "CubicOut", "CubicInOut", "BounceOut", "BounceIn", "SpringIn", "SpringOut"
        };

        public string BadgeBackgroundColorName
        {
            get
            {
                return this.badgeBackgroundColorName;
            }
            set
            {
                if (this.badgeBackgroundColorName != value)
                {
                    this.badgeBackgroundColorName = value;
                    this.BadgeBackgroundColor = this.nameToColorDictionary[value];
                    this.OnPropertyChanged();
                }
            }
        }

        public Color BadgeBackgroundColor
        {
            get
            {
                return this.badgeBackgroundColor;
            }
            set
            {
                if (this.badgeBackgroundColor != value)
                {
                    this.badgeBackgroundColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string BadgeTextColorName
        {
            get
            {
                return this.badgeTextColorName;
            }
            set
            {
                if (this.badgeTextColorName != value)
                {
                    this.badgeTextColorName = value;
                    this.BadgeTextColor = this.nameToColorDictionary[value];
                    this.OnPropertyChanged();
                }
            }
        }

        public Color BadgeTextColor
        {
            get
            {
                return this.badgeTextColor;
            }
            set
            {
                if (this.badgeTextColor != value)
                {
                    this.badgeTextColor = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string BadgeText
        {
            get
            {
                return this.badgeText;
            }
            set
            {
                if (this.badgeText != value)
                {
                    this.badgeText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public BadgePosition BadgeHorizontalPosition
        {
            get
            {
                return this.badgeHorizontalPosition;
            }
            set
            {
                if (this.badgeHorizontalPosition != value)
                {

                    this.badgeHorizontalPosition = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public BadgePosition BadgeVerticalPosition
        {
            get
            {
                return this.badgeVerticalPosition;
            }
            set
            {
                if (this.badgeVerticalPosition != value)
                {

                    this.badgeVerticalPosition = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public BadgeAlignment BadgeHorizontalAlignment
        {
            get
            {
                return this.badgeHorizontalAlignment;
            }
            set
            {
                if (this.badgeHorizontalAlignment != value)
                {

                    this.badgeHorizontalAlignment = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public BadgeAlignment BadgeVerticalAlignment
        {
            get
            {
                return this.badgeVerticalAlignment;
            }
            set
            {
                if (this.badgeVerticalAlignment != value)
                {

                    this.badgeVerticalAlignment = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double OffSetX
        {
            get
            {
                return this.offSetX;
            }
            set
            {
                if (this.offSetX != value)
                {
                    this.offSetX = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double OffSetY
        {
            get
            {
                return this.offSetY;
            }
            set
            {
                if (this.offSetY != value)
                {
                    this.offSetY = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double TextMarginLeft
        {
            get
            {
                return this.textMarginLeft;
            }
            set
            {
                if (this.textMarginLeft != value)
                {
                    this.textMarginLeft = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(BadgeTextMargin));
                }
            }
        }

        public double TextMarginTop
        {
            get
            {
                return this.textMarginTop;
            }
            set
            {
                if (this.textMarginTop != value)
                {
                    this.textMarginTop = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(BadgeTextMargin));
                }
            }
        }

        public double TextMarginRight
        {
            get
            {
                return this.textMarginRight;
            }
            set
            {
                if (this.textMarginRight != value)
                {
                    this.textMarginRight = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(BadgeTextMargin));
                }
            }
        }

        public double TextMarginBottom
        {
            get
            {
                return this.textMarginBottom;
            }
            set
            {
                if (this.textMarginBottom != value)
                {
                    this.textMarginBottom = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(BadgeTextMargin));
                }
            }
        }

        public Thickness BadgeTextMargin
        {
            get
            {
                return new Thickness(this.TextMarginLeft, this.TextMarginTop, this.TextMarginRight, this.TextMarginBottom);
            }
        }

        public BadgeAnimationType BadgeAnimationType
        {
            get
            {
                return this.badgeAnimationType;
            }
            set
            {
                if (this.badgeAnimationType != value)
                {
                    this.badgeAnimationType = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Easing BadgeAnimationEasing
        {
            get
            {
                return this.badgeAnimationEasing;
            }
            set
            {
                if (this.badgeAnimationEasing != value)
                {
                    this.badgeAnimationEasing = value;
                    this.BadgeStartAnimationCommand.Execute(null);
                    this.OnPropertyChanged();
                }
            }
        }

        public double BadgeAnimationDuration
        {
            get
            {
                return this.badgeAnimationDuration;
            }
            set
            {
                if (this.badgeAnimationDuration != value)
                {
                    this.badgeAnimationDuration = value;
                    this.BadgeStartAnimationCommand.Execute(null);
                    this.OnPropertyChanged();
                }
            }
        }

        public ICommand BadgeStartAnimationCommand { get; set; }
    }
}
