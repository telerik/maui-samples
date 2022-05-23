using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui;

namespace SDKBrowserMaui.Examples.DateTimePickerControl.FeaturesCategory.LocalizationExample
{
    internal class CustomDateTimePickerLocalizationManager : TelerikLocalizationManager
    {
        public override string GetString(string key)
        {

            if (key == "DateTimePicker_Popup_HeaderLabelText")
                return "Datum und Uhrzeit Picker";
            if (key == "DateTimePicker_PlaceholderLabelText")
                return "Datum und Uhrzeit auswählen";
            if (key == "Picker_AmPmSpinnerHeaderLabelText")
                return "am/pm";
            if (key == "Picker_DaySpinnerHeaderLabelText")
                return "Tag";
            if (key == "Picker_HourSpinnerHeaderLabelText")
                return "Zeit";
            if (key == "Picker_MinuteSpinnerHeaderLabelText")
                return "Minute";
            if (key == "Picker_SecondSpinnerHeaderLabelText")
                return "Sekunde";
            if (key == "Picker_MonthSpinnerHeaderLabelText	")
                return "Monat";
            if (key == "Picker_YearSpinnerHeaderLabelText")
                return "Jahr";
            if (key == "Picker_Popup_AcceptButtonText")
                return "Akzeptieren";
            if (key == "Picker_Popup_CancelButtonText")
                return "Stornieren";

            return base.GetString(key);
        }
    }
    // << datetimepicker-custom-localization-manager
}
