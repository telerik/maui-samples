using Microsoft.Maui.Devices;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace QSF.Examples.NavigationViewControl.ConfigurationExample
{
    public class ConfigurationViewModel : ConfigurationExampleViewModel
    {
        private string header;
        private NavigationViewDisplayMode displayMode;
        private double compactWidth;
        private double expandedWidth;
        private bool autoChangeDisplayMode;
        private double compactModeThresholdWidth = 641d;
        private double expandedModeThresholdWidth = 1008d;

        public ConfigurationViewModel()
        {
            this.DisplayModes = (IEnumerable<NavigationViewDisplayMode>)Enum.GetValues(typeof(NavigationViewDisplayMode));
            this.header = "Explore";
            this.displayMode = NavigationViewDisplayMode.Compact;

#if ANDROID
            this.compactWidth = 56;
            this.expandedWidth = 320;

#elif MACCATALYST
            this.compactWidth = 38;
            this.expandedWidth = 280;
#elif IOS
            this.compactWidth = 52;
            this.expandedWidth = 320;
#elif WINDOWS
            this.compactWidth = 48;
            this.expandedWidth = 260;
#endif

            this.autoChangeDisplayMode = DeviceInfo.Idiom == DeviceIdiom.Desktop || DeviceInfo.Idiom == DeviceIdiom.Tablet;
        }

        public IEnumerable<NavigationViewDisplayMode> DisplayModes { get; }

        public string Header
        {
            get { return this.header; }
            set { this.UpdateValue(ref this.header, value); }
        }

        public NavigationViewDisplayMode DisplayMode
        {
            get { return this.displayMode; }
            set { this.UpdateValue(ref this.displayMode, value); }
        }

        public double CompactPaneWidth
        {
            get { return this.compactWidth; }
            set { this.UpdateValue(ref this.compactWidth, value); }
        }

        public double ExpandedPaneWidth
        {
            get { return this.expandedWidth; }
            set { this.UpdateValue(ref this.expandedWidth, value); }
        }

        public bool AutoChangeDisplayMode
        {
            get { return this.autoChangeDisplayMode; }
            set { this.UpdateValue(ref this.autoChangeDisplayMode, value); }
        }

        public double CompactModeThresholdWidth
        {
            get { return this.compactModeThresholdWidth; }
            set { this.UpdateValue(ref this.compactModeThresholdWidth, value); }
        }

        public double ExpandedModeThresholdWidth
        {
            get { return this.expandedModeThresholdWidth; }
            set { this.UpdateValue(ref this.expandedModeThresholdWidth, value); }
        }
    }
}
