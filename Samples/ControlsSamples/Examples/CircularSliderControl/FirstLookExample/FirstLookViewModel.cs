using Microsoft.Maui.Graphics;
using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui;
using Telerik.Maui.Controls;

namespace QSF.Examples.CircularSliderControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private double temperature = 22;
    private bool isSystemOn = true;
    private string selectedMode = "Heat";
    private ModeOption selectedModeItem;
    private string selectedFanSpeed = "High";
    private FanSpeedOption selectedFanSpeedOption;

    public FirstLookViewModel()
    {
        this.ModeOptions = new ObservableCollection<ModeOption>
        {
            new("Dry", TelerikFont.IconBlur, TelerikFont.Name),
            new("Cool","\xe8c3", "TelerikFontExamples"),
            new("Heat", TelerikFont.IconBrightness, TelerikFont.Name),
            new("Fan", "\xe8c2", "TelerikFontExamples"),
            new("Auto", TelerikFont.IconFontFamily, TelerikFont.Name),
        };
        this.selectedModeItem = this.ModeOptions.FirstOrDefault(x => x.Name == this.selectedMode);

        this.FanSpeedOptions = new ObservableCollection<FanSpeedOption>
        {
            new("Low"),
            new("Medium"),
            new("High"),
            new("Auto"),
        };
        this.selectedFanSpeedOption = this.FanSpeedOptions.FirstOrDefault(x => x.Name == this.selectedFanSpeed);

        this.StatCards = new ObservableCollection<StatCardInfo>
        {
            new("\ue85c", "TelerikFontExamples", Color.FromArgb("#1F81FE"), "Current", this.CurrentTemperature),
            new(TelerikFont.IconBrightnessContrast, TelerikFont.Name, Color.FromArgb("#FDC000"), "Outdoor", this.OutdoorTemperature),
            new(TelerikFont.IconBlur, TelerikFont.Name, Color.FromArgb("#1F81FE"), "Humidity", this.HumidityText),
        };
    }

    public ObservableCollection<ModeOption> ModeOptions { get; }

    public ObservableCollection<FanSpeedOption> FanSpeedOptions { get; }

    public ObservableCollection<StatCardInfo> StatCards { get; }

    public double Temperature
    {
        get => this.temperature;
        set
        {
            this.UpdateValue(ref this.temperature, value);
            this.OnPropertyChanged(nameof(this.TemperatureText));
            this.OnPropertyChanged(nameof(this.HeatingStatusText));
            this.OnPropertyChanged(nameof(this.ModeStatusText));
        }
    }

    public string TemperatureText => $"{Math.Round(this.temperature, MidpointRounding.AwayFromZero):0}°C";

    public string HeatingStatusText => this.ModeStatusText;

    public string ModeStatusText
    {
        get
        {
            return this.selectedMode switch
            {
                "Dry" => $"Drying",
                "Cool" => $"Cooling",
                "Fan" => $"Fan",
                "Auto" => $"Auto",
                _ => $"Heating",
            };
        }
    }

    public Color ModeStatusColor
    {
        get => this.ModeAccentColor.WithAlpha(0.9f);
    }

    public Color ModeAccentColor
    {
        get
        {
            return this.selectedMode switch
            {
                // 14B8A6, 6B7A9E
                "Dry" => Color.FromArgb("#2BAEB4"),
                "Cool" => Color.FromArgb("#3F88D8"),
                "Fan" => Color.FromArgb("#64748B"),
                "Auto" => Color.FromArgb("#5A6ED4"),
                _ => Color.FromArgb("#F08A2B")
            };
        }
    }

    public Color ModeAccentMutedColor
    {
        get
        {
            return this.selectedMode switch
            {
                "Dry" => ThemingViewModel.IsDarkMode ? Color.FromArgb("#273A3B") : Color.FromArgb("#D1ECE9"),
                "Cool" => ThemingViewModel.IsDarkMode ? Color.FromArgb("#28384D") : Color.FromArgb("#D8EBFF"),
                "Fan" => ThemingViewModel.IsDarkMode ? Color.FromArgb("#303843") : Color.FromArgb("#E4E7EB"),
                "Auto" => ThemingViewModel.IsDarkMode ? Color.FromArgb("#252D45") : Color.FromArgb("#E8E7FF"),
                _ => ThemingViewModel.IsDarkMode ? Color.FromArgb("#3D3228") : Color.FromArgb("#F5DFC8"),
            };
        }
    }

    public bool IsSystemOn
    {
        get => this.isSystemOn;
        set => this.UpdateValue(ref this.isSystemOn, value);
    }

    public string OutdoorTemperature => "18°C";

    public string CurrentTemperature => "21.4°C";

    public string HumidityText => "48%";

    public string OutdoorForecast => "12°C";

    public string SelectedMode
    {
        get => this.selectedMode;
        set
        {
            this.UpdateValue(ref this.selectedMode, value);
            var modeOption = this.ModeOptions.FirstOrDefault(x => x.Name == value);
            if (!ReferenceEquals(this.selectedModeItem, modeOption))
            {
                this.selectedModeItem = modeOption;
                this.OnPropertyChanged(nameof(this.SelectedModeItem));
            }
            this.OnPropertyChanged(nameof(this.ModeStatusText));
            this.OnPropertyChanged(nameof(this.ModeStatusColor));
            this.OnPropertyChanged(nameof(this.ModeAccentColor));
            this.OnPropertyChanged(nameof(this.ModeAccentMutedColor));
            this.OnPropertyChanged(nameof(this.HeatingStatusText));
        }
    }

    public ModeOption SelectedModeItem
    {
        get => this.selectedModeItem;
        set
        {
            if (this.UpdateValue(ref this.selectedModeItem, value) && value != null)
            {
                this.SelectedMode = value.Name;
            }
        }
    }

    public string SelectedFanSpeed
    {
        get => this.selectedFanSpeed;
        set
        {
            if (this.UpdateValue(ref this.selectedFanSpeed, value))
            {
                var fanSpeedOption = this.FanSpeedOptions.FirstOrDefault(x => x.Name == value);
                if (!ReferenceEquals(this.selectedFanSpeedOption, fanSpeedOption))
                {
                    this.selectedFanSpeedOption = fanSpeedOption;
                    this.OnPropertyChanged(nameof(this.SelectedFanSpeedOption));
                }

                this.OnPropertyChanged(nameof(this.IsLowSpeed));
                this.OnPropertyChanged(nameof(this.IsMediumSpeed));
                this.OnPropertyChanged(nameof(this.IsHighSpeed));
                this.OnPropertyChanged(nameof(this.IsAutoSpeed));
            }
        }
    }

    public FanSpeedOption SelectedFanSpeedOption
    {
        get => this.selectedFanSpeedOption;
        set
        {
            if (this.UpdateValue(ref this.selectedFanSpeedOption, value) && value != null)
            {
                this.SelectedFanSpeed = value.Name;
            }
        }
    }

    public bool IsLowSpeed => this.selectedFanSpeed == "Low";
    public bool IsMediumSpeed => this.selectedFanSpeed == "Medium";
    public bool IsHighSpeed => this.selectedFanSpeed == "High";
    public bool IsAutoSpeed => this.selectedFanSpeed == "Auto";
}

public class ModeOption
{
    public ModeOption(string name, string icon, string fontFamily = "TelerikFontExamples")
    {
        this.Name = name;
        this.Icon = icon;
        this.FontFamily = fontFamily;
    }

    public string Name { get; }

    public string Icon { get; }

    public string FontFamily { get; }
}

public class FanSpeedOption
{
    public FanSpeedOption(string name)
    {
        this.Name = name;
    }

    public string Name { get; }
}

public class StatCardInfo
{
    public StatCardInfo(string icon, string iconFontFamily, Color iconColor, string label, string value)
    {
        this.Icon = icon;
        this.IconFontFamily = iconFontFamily;
        this.IconColor = iconColor;
        this.Label = label;
        this.Value = value;
    }

    public string Icon { get; }

    public string IconFontFamily { get; }

    public Color IconColor { get; }

    public string Label { get; }

    public string Value { get; }
}
