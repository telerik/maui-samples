using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.RangeSliderControl.GettingStartedCategory.GettingStartedExample
{
    public class GettingStartedCSharp : ContentView
    {
        public GettingStartedCSharp()
        {
            var content = new Grid();

            // >> rangeslider-gettingstarted-csharp
            var rangeSlider = new RadRangeSlider();
            rangeSlider.Minimum = 0;
            rangeSlider.Maximum = 100;
            rangeSlider.RangeStart = 25;
            rangeSlider.RangeEnd = 95;
            // << rangeslider-gettingstarted-csharp

            content.Add(rangeSlider);
            this.Content = content;
        }
    }
}