using System.Collections.Generic;
using System.Globalization;
using QSF.ViewModels;

namespace QSF.Examples.CalendarControl.GlobalizationExample;

public class GlobalizationViewModel : ExampleViewModel
{
    private CultureInfo culture;
    private CultureData selectedCulture;

    public GlobalizationViewModel()
    {
        this.Cultures = new List<CultureData>()
        {
            new CultureData("Gregorian US", "en-US"),
            new CultureData("Gregorian BG", "bg-BG"),
            new CultureData("Hebrew", "he-IL"),
            new CultureData("Hijri", "ar-SA"),
            new CultureData("Japanese", "ja"),
            new CultureData("Persian", "fa"),
            new CultureData("Taiwan", "zh-TW"),
            new CultureData("Thai/Buddhist", "th"),
            new CultureData("Umm al-Qura", "ar-SA")
        };

        this.SelectedCulture = this.Cultures[0];
    }

    public List<CultureData> Cultures { get; }

    public CultureInfo Culture
    {
        get { return this.culture; }
        set { this.UpdateValue(ref this.culture, value ?? CultureInfo.CurrentCulture); }
    }

    public CultureData SelectedCulture
    {
        get { return this.selectedCulture; }
        set
        {
            if (this.selectedCulture != value && value != null)
            {
                this.selectedCulture = value;
                this.UpdateValue(ref this.selectedCulture, value);

                this.Culture = CreateCultureInfo(this.selectedCulture);
            }
        }
    }

    private static CultureInfo CreateCultureInfo(CultureData data)
    {
        if (data == null || string.IsNullOrEmpty(data.Name))
        {
            return CultureInfo.CurrentCulture;
        }

        try
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(data.Name);
            switch (data.DisplayName)
            {
                case "Hebrew":
                    cultureInfo.DateTimeFormat.Calendar = new HebrewCalendar();
                    break;
                case "Hijri":
                    cultureInfo.DateTimeFormat.Calendar = new HijriCalendar();
                    break;
                case "Japanese":
                    cultureInfo.DateTimeFormat.Calendar = new GregorianCalendar();
                    break;
                case "Persian":
                    cultureInfo.DateTimeFormat.Calendar = new PersianCalendar();
                    break;
                case "Taiwan":
                    cultureInfo.DateTimeFormat.Calendar = new TaiwanCalendar();
                    break;
                case "Thai/Buddhist":
                    cultureInfo.DateTimeFormat.Calendar = new ThaiBuddhistCalendar();
                    break;
                case "Umm al-Qura":
                    cultureInfo.DateTimeFormat.Calendar = new UmAlQuraCalendar();
                    break;
            }

            return cultureInfo;
        }
        catch (Exception)
        {
            return CultureInfo.CurrentCulture;
        }
    }
}