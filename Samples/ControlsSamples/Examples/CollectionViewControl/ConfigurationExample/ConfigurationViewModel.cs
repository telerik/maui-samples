using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Maui;
using Telerik.Maui.Controls.CollectionView;

namespace QSF.Examples.CollectionViewControl.ConfigurationExample;

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private readonly double defaultHorizontalItemLength = 90;
    private readonly double defaultVerticalItemLength;
    private Orientation layoutOrientation = Orientation.Vertical;
    private double horizontalItemLength;
    private double verticalItemLength;
    private double itemLength;
    private double minimumItemLength;
    private double itemSpacing;
    private CollectionViewSelectionMode selectionMode = CollectionViewSelectionMode.Single;

    public ConfigurationViewModel()
    {
        this.Contacts = new List<string>
        {
            "Eva Lawson",
            "Layton Buck",
            "Chester Harvey",
            "Jenny Fuller",
            "Ashley Robertson",
            "Niki Samaniego",
            "Armstrong Robbie",
            "Coby Ryan",
            "Davis Anderson",
            "Quincy Sanchez",
            "Konny Mills",
            "Casper Fletcher",
            "Barnes Ashton",
            "Bob Carty",
            "Angus Barnes",
            "Vada Davies",
            "David Webb",
            "Nida Carty",
            "Jeffery Francis",
            "Terrell Norris",
            "Greg Nikolas",
            "Barnaby Hunter",
            "Peter Bence",
            "Peter Bence",
            "Howard Pittman",
            "Joel Walsh",
            "Matias Santos",
            "Xavi Vasquez",
        };

        this.horizontalItemLength = this.defaultHorizontalItemLength;

#if ANDROID
        this.defaultVerticalItemLength = 48;
#elif MACCATALYST
        this.defaultVerticalItemLength = 24;
#elif IOS
        this.defaultVerticalItemLength = 44;
#elif WINDOWS
        this.defaultVerticalItemLength = 40;
#endif

        this.verticalItemLength = this.defaultVerticalItemLength;

        this.RefreshMinimumItemLength();
        this.RefreshItemLength();
    }

    public List<string> Contacts { get; private set; }

    public IEnumerable<Orientation> LayoutOrientations { get; } = new List<Orientation> { Orientation.Vertical, Orientation.Horizontal };

    public IEnumerable<CollectionViewSelectionMode> SelectionModes { get; } = Enum.GetValues(typeof(CollectionViewSelectionMode)).Cast<CollectionViewSelectionMode>();

    public Orientation LayoutOrientation
    {
        get => this.layoutOrientation;
        set
        {
            if (this.layoutOrientation != value)
            {
                if (this.layoutOrientation == Orientation.Vertical)
                {
                    this.verticalItemLength = this.itemLength;
                }
                else
                {
                    this.horizontalItemLength = this.itemLength;
                }

                this.layoutOrientation = value;
                this.OnPropertyChanged();
                this.RefreshMinimumItemLength();
                this.RefreshItemLength();
            }
        }
    }

    public double ItemLength
    {
        get => this.itemLength;
        set => this.UpdateValue(ref this.itemLength, value);
    }

    public double MinimumItemLength
    {
        get => this.minimumItemLength;
        set => this.UpdateValue(ref this.minimumItemLength, value);
    }

    public double ItemSpacing
    {
        get => this.itemSpacing;
        set => this.UpdateValue(ref this.itemSpacing, value);
    }

    public CollectionViewSelectionMode SelectionMode
    {
        get => this.selectionMode;
        set => this.UpdateValue(ref this.selectionMode, value);
    }

    private void RefreshItemLength()
    {
        this.ItemLength = this.LayoutOrientation == Orientation.Horizontal
            ? this.horizontalItemLength 
            : this.verticalItemLength;
    }

    private void RefreshMinimumItemLength()
    {
        this.MinimumItemLength = this.LayoutOrientation == Orientation.Horizontal
            ? this.defaultHorizontalItemLength 
            : this.defaultVerticalItemLength;
    }
}