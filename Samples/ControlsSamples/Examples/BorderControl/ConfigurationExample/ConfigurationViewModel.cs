using Microsoft.Maui;
using QSF.ViewModels;
using System.Collections.Generic;

namespace QSF.Examples.BorderControl.ConfigurationExample;

public class ConfigurationViewModel : ExampleViewModel
{
    private string selectedBorderColor;

    private double leftCornerRadius = 60;
    private double topCornerRadius = 60;
    private double rightCornerRadius = 60;
    private double bottomCornerRadius = 60;
    private double borderThicknessValue = 15;

    private Thickness borderCornerRadius;
    private Thickness borderThickness = new Thickness(15);

    public string Avatar { get; set; }

    public ConfigurationViewModel()
    {
        this.BorderColors = new List<string>
            {
                "LightGray",
                "DarkGray",
                "LightPurple",
                "DarkPurple",
            };

        this.Avatar = "borderconfigurationavatar.png";
        this.SelectedBorderColor = this.BorderColors[0];

        this.UpdateCornerRadius();
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
            if (this.selectedBorderColor != value)
            {
                this.selectedBorderColor = value;
                this.OnPropertyChanged();
            }
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
            if (this.leftCornerRadius != value)
            {
                this.leftCornerRadius = value;
                this.OnPropertyChanged();

                this.UpdateCornerRadius();
            }
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
            if (this.topCornerRadius != value)
            {
                this.topCornerRadius = value;
                this.OnPropertyChanged();

                this.UpdateCornerRadius();
            }
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
            if (this.rightCornerRadius != value)
            {
                this.rightCornerRadius = value;
                this.OnPropertyChanged();

                this.UpdateCornerRadius();
            }
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
            if (this.bottomCornerRadius != value)
            {
                this.bottomCornerRadius = value;
                this.OnPropertyChanged();

                this.UpdateCornerRadius();
            }
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
            if (this.borderCornerRadius != value)
            {
                this.borderCornerRadius = value;
                this.OnPropertyChanged();
            }
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
            if (this.borderThickness != value)
            {
                this.borderThickness = value;
                this.OnPropertyChanged();
            }
        }
    }

    public double BorderThicknessValue
    {
        get
        {
            return this.borderThicknessValue;
        }
        set
        {
            if (this.borderThicknessValue != value)
            {
                this.borderThicknessValue = value;
                this.OnPropertyChanged();

                this.BorderThickness = new Thickness(this.borderThicknessValue);
            }
        }
    }

    private void UpdateCornerRadius()
        => this.BorderCornerRadius = new Thickness(this.leftCornerRadius, this.topCornerRadius, this.rightCornerRadius, this.bottomCornerRadius);
}
