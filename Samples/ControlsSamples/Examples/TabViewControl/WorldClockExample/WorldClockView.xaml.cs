using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;

namespace QSF.Examples.TabViewControl.WorldClockExample
{
    public partial class WorldClockView : ContentView
    {
        private const string sofiaZoneId = "Europe/Sofia";
        private const string sofiaZoneIdUWP = "FLE Standard Time";
        private const string aucklandZoneId = "Pacific/Auckland";
        private const string aucklandZoneIdUWP = "New Zealand Standard Time";
        private const string newYorkZoneId = "America/New_York";
        private const string newYorkZoneIdUWP = "US Eastern Standard Time";
        private const string brusselsZoneId = "Europe/Brussels";
        private const string brusselsZoneIdUWP = "Romance Standard Time";
        private const string moscowZoneId = "Europe/Moscow";
        private const string moscowZoneIdUWP = "Russian Standard Time";

        public WorldClockView()
        {
            InitializeComponent();

            TimeZoneInfo sofiaZone = this.GetTimeZoneByPlatform(sofiaZoneId, sofiaZoneIdUWP);
            TimeZoneInfo aucklandZone = this.GetTimeZoneByPlatform(aucklandZoneId, aucklandZoneIdUWP);
            TimeZoneInfo newYorkZone = this.GetTimeZoneByPlatform(newYorkZoneId, newYorkZoneIdUWP);
            TimeZoneInfo brusselsZone = this.GetTimeZoneByPlatform(brusselsZoneId, brusselsZoneIdUWP);
            TimeZoneInfo moscowZone = this.GetTimeZoneByPlatform(moscowZoneId, moscowZoneIdUWP);

            var currentTime = new DateTime(2022, 05, 16, 15, 00, 00);
            DateTime sofiaCurrentTime = TimeZoneInfo.ConvertTimeFromUtc(currentTime, sofiaZone);
            DateTime aucklandCurrentTime = TimeZoneInfo.ConvertTimeFromUtc(currentTime, aucklandZone);
            DateTime newYorkCurrentTime = TimeZoneInfo.ConvertTimeFromUtc(currentTime, newYorkZone);
            DateTime brusselsCurrentTime = TimeZoneInfo.ConvertTimeFromUtc(currentTime, brusselsZone);
            DateTime moscowCurrentTime = TimeZoneInfo.ConvertTimeFromUtc(currentTime, moscowZone);

            this.sofia.BindingContext = sofiaCurrentTime;
            this.auckland.BindingContext = aucklandCurrentTime;
            this.newYork.BindingContext = newYorkCurrentTime;
            this.brussels.BindingContext = brusselsCurrentTime;
            this.moscow.BindingContext = moscowCurrentTime;
        }

        private TimeZoneInfo GetTimeZoneByPlatform(string id, string uwpId)
        {
           return DeviceInfo.Platform == DevicePlatform.WinUI
                ? TimeZoneInfo.FindSystemTimeZoneById(uwpId)
                : TimeZoneInfo.FindSystemTimeZoneById(id);
        }
    }
}