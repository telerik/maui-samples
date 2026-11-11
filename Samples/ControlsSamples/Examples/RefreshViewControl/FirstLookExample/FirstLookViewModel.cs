using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using QSF.ViewModels;
using Telerik.AppUtils.Services;
using Telerik.Maui;

namespace QSF.Examples.RefreshViewControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private readonly Random random;
    private readonly ITestingService testingService;
    private readonly (string Condition, string Icon, string IconFontFamily)[] conditions =
    {
        ("Sunny", TelerikFont.IconBrightness, TelerikFont.Name),
        ("Mostly sunny", TelerikFont.IconBrightnessContrast, TelerikFont.Name),
        ("Partly cloudy", "\u26C5", null),
    };

    private bool isRefreshing;
    private int temperature;
    private int high;
    private int low;
    private int wind;
    private int humidity;
    private int feelsLike;
    private int uvIndex;
    private string condition;
    private string conditionIcon;
    private string conditionIconFontFamily;
    private DateTime lastUpdated;

    public FirstLookViewModel()
    {
        this.testingService = DependencyService.Get<ITestingService>();
        this.random = this.testingService.Random(24);
        this.HourlyForecast = new ObservableCollection<HourlyForecastInfo>();

        this.condition = this.conditions[1].Condition;
        this.conditionIcon = this.conditions[1].Icon;
        this.conditionIconFontFamily = this.conditions[1].IconFontFamily;
        this.temperature = 24;
        this.high = 26;
        this.low = 17;
        this.wind = 14;
        this.humidity = 58;
        this.feelsLike = 27;
        this.uvIndex = 4;
        this.lastUpdated = this.testingService.DateTimeNow(new DateTime(2026, 7, 7, 10, 42, 0));
        this.UpdateHourlyForecast();

        this.RefreshCommand = new Command(async () => await this.RefreshAsync());
    }

    public ICommand RefreshCommand { get; }

    public ObservableCollection<HourlyForecastInfo> HourlyForecast { get; }

    public bool IsRefreshing
    {
        get => this.isRefreshing;
        set => this.UpdateValue(ref this.isRefreshing, value);
    }

    public string Location => "Sofia, Bulgaria";

    public string TemperatureText => $"{this.temperature}°";

    public string HighText => $"{this.high}°";

    public string LowText => $"{this.low}°";

    public string HighLowText => $"H: {this.high}°  L: {this.low}°";

    public string Condition => this.condition;

    public string ConditionIcon => this.conditionIcon;

    public string ConditionIconFontFamily => this.conditionIconFontFamily;

    public string WindText => $"{this.wind} km/h";

    public string HumidityText => $"{this.humidity}%";

    public string FeelsLikeText => $"{this.feelsLike}°";

    public int UVIndex => this.uvIndex;

    public string UVCategory => GetUVCategory(this.uvIndex);

    public string LastUpdatedText => $"Last updated at {this.lastUpdated:HH:mm}";

    private async Task RefreshAsync()
    {
        this.IsRefreshing = true;

        await Task.Delay(1500);

        var conditionInfo = this.conditions[this.random.Next(this.conditions.Length)];
        this.condition = conditionInfo.Condition;
        this.conditionIcon = conditionInfo.Icon;
        this.conditionIconFontFamily = conditionInfo.IconFontFamily;

        this.temperature = this.random.Next(21, 29);
        this.high = this.temperature + this.random.Next(1, 4);
        this.low = this.temperature - this.random.Next(6, 9);
        this.wind = this.random.Next(6, 23);
        this.humidity = this.random.Next(38, 66);
        this.feelsLike = this.temperature + this.random.Next(0, 4);
        this.uvIndex = this.random.Next(3, 9);
        this.lastUpdated = this.lastUpdated.AddMinutes(this.random.Next(5, 30));

        this.UpdateHourlyForecast();

        this.OnPropertyChanged(string.Empty);
        this.IsRefreshing = false;
    }

    private void UpdateHourlyForecast()
    {
        this.HourlyForecast.Clear();

        var hour = this.lastUpdated.Hour + 1;
        var hourTemperature = this.temperature;

        for (int i = 0; i < 4; i++)
        {
            var conditionInfo = this.conditions[this.random.Next(this.conditions.Length)];
            hourTemperature = Math.Min(this.high, Math.Max(this.low, hourTemperature + this.random.Next(-1, 2)));

            this.HourlyForecast.Add(new HourlyForecastInfo
            {
                Time = $"{(hour + i) % 24:00}:00",
                Icon = conditionInfo.Icon,
                IconFontFamily = conditionInfo.IconFontFamily,
                Temperature = $"{hourTemperature}°",
            });
        }
    }

    private static string GetUVCategory(int uvIndex)
    {
        return uvIndex switch
        {
            <= 2 => "Low",
            <= 5 => "Moderate",
            <= 7 => "High",
            <= 10 => "Very high",
            _ => "Extreme",
        };
    }
}

public class HourlyForecastInfo
{
    public string Time { get; set; }

    public string Icon { get; set; }

    public string IconFontFamily { get; set; }

    public string Temperature { get; set; }
}
