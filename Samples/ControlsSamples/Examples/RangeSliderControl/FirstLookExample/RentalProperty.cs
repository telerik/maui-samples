using QSF.ViewModels;

namespace QSF.Examples.RangeSliderControl.FirstLookExample;

public class RentalProperty : ViewModelBase
{
    private string image;
    private int maxGuests;
    private string name;
    private string type;
    private double pricePerNight;

    public string Image
    {
        get => this.image;
        set => this.UpdateValue(ref this.image, value);
    }

    public int MaxGuests
    {
        get => this.maxGuests;
        set => this.UpdateValue(ref this.maxGuests, value);
    }

    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public string Type
    {
        get => this.type;
        set => this.UpdateValue(ref this.type, value);
    }

    public double PricePerNight
    {
        get => this.pricePerNight;
        set => this.UpdateValue(ref this.pricePerNight, value);
    }
}