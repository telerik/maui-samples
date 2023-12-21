using QSF.ViewModels;

namespace QSF.Examples.SliderControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private double atmLimit = 2000;

    private double posLimit = 4000;

    private double totalLimit = 10000;

    public double ATMLimit
    {
        get => this.atmLimit;
        set => this.UpdateValue(ref this.atmLimit, value);
    }

    public double POSLimit
    {
        get => this.posLimit;
        set => this.UpdateValue(ref this.posLimit, value);
    }

    public double TotalLimit
    {
        get => this.totalLimit;
        set => this.UpdateValue(ref this.totalLimit, value);
    }
}