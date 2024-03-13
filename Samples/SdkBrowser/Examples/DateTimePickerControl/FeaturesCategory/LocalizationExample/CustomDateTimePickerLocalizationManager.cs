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
            switch (key)
            {
                case "DateTimePicker_Popup_HeaderLabelText":
                    return "Datum und Uhrzeit Picker";
                case "DateTimePicker_PlaceholderLabelText":
                    return "Datum und Uhrzeit auswählen";
                case "Picker_AmPmSpinnerHeaderLabelText":
                    return "am/pm";
                case "Picker_DaySpinnerHeaderLabelText":
                    return "Tag";
                case "Picker_HourSpinnerHeaderLabelText":
                    return "Zeit";
                case "Picker_MinuteSpinnerHeaderLabelText":
                    return "Minute";
                case "Picker_SecondSpinnerHeaderLabelText":
                    return "Sekunde";
                case "Picker_MonthSpinnerHeaderLabelText":
                    return "Monat";
                case "Picker_YearSpinnerHeaderLabelText":
                    return "Jahr";
                case "Picker_Popup_AcceptButtonText":
                    return "Akzeptieren";
                case "Picker_Popup_CancelButtonText":
                    return "Stornieren";
                case "Picker_DropDown_AcceptButtonText":
                    return "Akzeptieren";
                case "Picker_DropDown_CancelButtonText":
                    return "Stornieren";
            }

            return base.GetString(key);
        }
    }
    // << datetimepicker-custom-localization-manager
}
