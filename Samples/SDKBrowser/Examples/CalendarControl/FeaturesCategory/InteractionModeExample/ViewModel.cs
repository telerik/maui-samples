using System;
using System.Collections.Generic;
using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Calendar;

namespace SDKBrowserMaui.Examples.CalendarControl.FeaturesCategory.InteractionModeExample;

public class ViewModel : NotifyPropertyChangedBase
{
    private CalendarInteractionMode interactionMode;
    private string interactionModeInfo;

    public ViewModel()
    {
        this.InteractionModes = (IEnumerable<CalendarInteractionMode>)Enum.GetValues(typeof(Telerik.Maui.Controls.Calendar.CalendarInteractionMode));

        if (DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            this.InteractionMode = CalendarInteractionMode.None;
        }
        else
        {
            this.InteractionMode = CalendarInteractionMode.Pan;
        }
    }

    public IEnumerable<CalendarInteractionMode> InteractionModes { get; set; }

    public CalendarInteractionMode InteractionMode
    {
        get { return this.interactionMode; }
        set
        {
            this.UpdateValue(ref this.interactionMode, value);

            if (this.InteractionMode == CalendarInteractionMode.Pan)
            {
                this.InteractionModeInfo = "Allows changing the current calendar view by using pan gesture AND the buttons.";
            }
            else
            {
                this.InteractionModeInfo = "Allows changing the current calendar view only by using the buttons.";
            }
        }
    }

    public string InteractionModeInfo
    { 
        get { return this.interactionModeInfo; }
        set { this.UpdateValue(ref this.interactionModeInfo, value); }
    }
}