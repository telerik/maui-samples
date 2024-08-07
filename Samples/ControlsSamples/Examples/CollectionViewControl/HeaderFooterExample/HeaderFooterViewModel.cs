using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace QSF.Examples.CollectionViewControl.HeaderFooterExample;

public class HeaderFooterViewModel : ExampleViewModel
{
    private string label;
    private double total;

    public HeaderFooterViewModel()
    {
        this.Label = "Pay Bills";

        this.Items = new ObservableCollection<ListItem>
        {
            new ListItem
            {
                Name = "Rent",
                Icon = "\uE85B",
                Company = "East Orange Flat",
                Amount = 1800
            },
            new ListItem
            {
                Name = "Electricity",
                Icon = "\uE85E",
                Company = "Energy United",
                Amount = 32
            },
            new ListItem
            {
                Name = "Heating",
                Icon = "\uE85C",
                Company = "National Fuel Gas",
                Amount = 98
            },
            new ListItem
            {
                Name = "Cell Phone",
                Icon = "\uE85D",
                Company = "T-Mobile US",
                Amount = 26
            },
            new ListItem
            {
                Name = "Spotify Subscription",
                Icon = "\uE85B",
                Company = "Spotify",
                Amount = 12
            },
            new ListItem
            {
                Name = "Auto Loan",
                Icon = "\uE832",
                Company = "Tesla",
                Amount = 320
            },
            new ListItem
            {
                Name = "Cable Company",
                Icon = "\uE85F",
                Company = "Spectrum",
                Amount = 20
            },
            new ListItem
            {
                Name = "Trash Pickup",
                Icon = "\uE827",
                Company = "Junk Removal NYC",
                Amount = 18
            },
            new ListItem
            {
                Name = "Netflix Subscription",
                Icon = "\uE833",
                Company = "Netflix",
                Amount = 10,
            },
            new ListItem
            {
                Name = "Office Rent",
                Icon = "\uE85B",
                Company = "NY, Hudson Square 24",
                Amount = 2600
            },
            new ListItem
            {
                Name = "Electricity",
                Icon = "\uE85E",
                Company = "Direct Energy",
                Amount = 82
            },
            new ListItem
            {
                Name = "Heating",
                Icon = "\uE85C",
                Company = "Constellation Gas",
                Amount = 112
            },
            new ListItem
            {
                Name = "Auto Leasing",
                Icon = "\uE832",
                Company = "Chevrolet Trax",
                Amount = 640
            },
            new ListItem
            {
                Name = "Sky Sports Subscription",
                Icon = "\uE833",
                Company = "Sky UK",
                Amount = 15,
            },
            new ListItem
            {
                Name = "Internet",
                Icon = "\uE85F",
                Company = "RCN",
                Amount = 24
            },
            new ListItem
            {
                Name = "iCloud Subscription",
                Icon = "\uE833",
                Company = "Apple",
                Amount = 3
            },
            new ListItem
            {
                Name = "Office Parking Lot Rent",
                Icon = "\uE85B",
                Company = "NY, Hudson Square 24",
                Amount = 2600
            },
            new ListItem
            {
                Name = "Car AVI",
                Icon = "\uE832",
                Company = "Tesla",
                Amount = 320
            },
            new ListItem
            {
                Name = "Disney Subscription",
                Icon = "\uE833",
                Company = "Disney",
                Amount = 10,
            },
            new ListItem
            {
                Name = "Garage Rent",
                Icon = "\uE85B",
                Company = "NJ, East Orange",
                Amount = 2600
            },
            new ListItem
            {
                Name = "Garage Electricity",
                Icon = "\uE85E",
                Company = "Energy United",
                Amount = 32
            },
            new ListItem
            {
                Name = "HBO Max Subscription",
                Icon = "\uE833",
                Company = "Warner Bros. Discovery, Inc.",
                Amount = 10,
            },
        };

        foreach (var item in this.Items)
        {
            item.PropertyChanged += this.OnPropertyChanged;
        }

        this.TapCommand = new Command<ListItem>(this.OnTapCommand);
        this.PayCommand = new Command(this.OnPayCommand, this.OnPayCanExecute);
    }

    public string Label
    {
        get => this.label;
        private set => this.UpdateValue(ref this.label, value);
    }

    public double Total
    {
        get => this.total;
        private set => this.UpdateValue(ref this.total, value);
    }

    public ObservableCollection<ListItem> Items { get; }
    public ICommand TapCommand { get; }
    public ICommand PayCommand { get; }

    private void OnTapCommand(ListItem item)
    {
        item.IsSelected = !item.IsSelected;
    }

    private bool OnPayCanExecute(object arg)
    {
        return this.Items.Where(p => p.IsSelected).Count() > 0;
    }

    private void OnPayCommand(object arg)
    {
        if (this.Total > 0)
        {
            foreach (var item in this.Items)
            {
                if (item.IsSelected)
                {
                    item.IsSelected = false;
                    item.Amount = 0;
                }
            }

            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("The bills have been paid.");
        }
    }

    private void OnItemSelected()
    {
        var value = 0d;

        foreach (var item in this.Items)
        {
            if (item.IsSelected)
            {
                value += item.Amount;
            }
        }

        this.Total = value;

        if (value > 0)
        {
            this.Label = $"Pay ${value}";
        }
        else
        {
            this.Label = "Pay Bills";
        }

        ((Command)this.PayCommand).ChangeCanExecute();
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
    {
        if (eventArgs.PropertyName == nameof(ListItem.IsSelected))
        {
            this.OnItemSelected();
        }
    }
}