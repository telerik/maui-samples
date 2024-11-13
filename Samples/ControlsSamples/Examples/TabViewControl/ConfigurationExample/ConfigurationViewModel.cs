using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using QSF.ViewModels;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.TabViewControl.ConfigurationExample;

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private TabViewHeaderPosition headerPosition;
    private double headerSpacing;
    private bool isHeaderScrollable;
    private bool isHeaderOverlaid;
    private bool isContentSwipeEnabled;

    public ConfigurationViewModel()
    {
        this.Data = new ObservableCollection<Location>()
        {
            new Location() 
            { 
                Name = "London",
                Customers = new ObservableCollection<Customer>
                {
                    new Customer{ Name = "Jane", Number = 221144},
                    new Customer{ Name = "Fred", Number = 432457},
                    new Customer{ Name = "Jack", Number = 2783344},
                    new Customer{ Name = "Maya", Number = 645664},
                    new Customer{ Name = "Gabriel", Number = 8675},
                }
            },
            new Location()
            {
                Name = "Madrid",
                Customers = new ObservableCollection<Customer>
                {
                    new Customer{ Name = "Joan", Number = 345435},
                    new Customer{ Name = "Alex", Number = 232146},
                    new Customer{ Name = "Jose", Number = 65678},
                }
            },
            new Location()
            {
                Name = "Milan",
                Customers = new ObservableCollection<Customer>
                {
                    new Customer{ Name = "Luca", Number = 8960},
                    new Customer{ Name = "Giovanni", Number = 9876},
                    new Customer{ Name = "Enzo", Number = 4365},
                    new Customer{ Name = "Enzo", Number = 64545},
                    new Customer{ Name = "Enzo", Number = 2134},
                }
            },
            new Location()
            {
                Name = "New York",
                Customers = new ObservableCollection<Customer>
                {
                    new Customer{ Name = "William", Number = 546},
                    new Customer{ Name = "James", Number = 276654},
                    new Customer{ Name = "Sebastian", Number = 09867},
                    new Customer{ Name = "Andrea", Number = 53376},
                    new Customer{ Name = "John", Number = 3624},
                    new Customer{ Name = "Jane", Number = 6768},
                }
            }
        };

        this.isHeaderScrollable = true;

#if ANDROID || (IOS && !MACCATALYST)
        this.isContentSwipeEnabled = true;
#endif

#if MACCATALYST
        this.isHeaderOverlaid = true;
#endif
    }

    public ObservableCollection<Location> Data { get; set; }

    public IEnumerable<TabViewHeaderPosition> HeaderPositions { get; } = Enum.GetValues(typeof(TabViewHeaderPosition)).Cast<TabViewHeaderPosition>();

    public TabViewHeaderPosition HeaderPosition
    {
        get => this.headerPosition;
        set => this.UpdateValue(ref this.headerPosition, value);
    }

    public double HeaderSpacing
    {
        get => this.headerSpacing;
        set => this.UpdateValue(ref this.headerSpacing, value);
    }

    public bool IsHeaderScrollable
    {
        get => this.isHeaderScrollable;
        set => this.UpdateValue(ref this.isHeaderScrollable, value);
    }

    public bool IsHeaderOverlaid
    {
        get => this.isHeaderOverlaid;
        set => this.UpdateValue(ref this.isHeaderOverlaid, value);
    }

    public bool IsContentSwipeEnabled
    {
        get => this.isContentSwipeEnabled;
        set => this.UpdateValue(ref this.isContentSwipeEnabled, value);
    }
}