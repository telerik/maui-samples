using Telerik.Maui.Controls;

namespace QSF.Examples.CollectionViewControl.HeaderFooterExample;

public class ListItem  :NotifyPropertyChangedBase
{
    private string name;
    private string icon;
    private string company;
    private double amount;
    private bool isSelected;

    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public string Icon
    {
        get => this.icon;
        set => this.UpdateValue(ref this.icon, value);
    }

    public string Company
    {
        get => this.company;
        set => this.UpdateValue(ref this.company, value);
    }

    public double Amount
    {
        get => this.amount; 
        set => this.UpdateValue(ref this.amount, value);
    }

    public bool IsSelected
    {
        get => this.isSelected; 
        set => this.UpdateValue(ref this.isSelected, value);
    }
}