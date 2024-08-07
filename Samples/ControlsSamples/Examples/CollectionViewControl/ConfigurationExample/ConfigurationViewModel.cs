using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui;
using Telerik.Maui.Controls;
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
    private EmptyContentDisplayMode emptyContentDisplayMode;
    private bool isDragDropEnabled;
    private bool isItemSwipeEnabled;
    private string selectedSourceType;
    public ObservableCollection<Contact> itemsSource;

    public ConfigurationViewModel()
    {
        this.SelectedSourceType = this.SourceTypes[0];

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

    public IEnumerable<Orientation> LayoutOrientations { get; } = new List<Orientation> { Orientation.Vertical, Orientation.Horizontal };

    public IEnumerable<CollectionViewSelectionMode> SelectionModes { get; } = Enum.GetValues(typeof(CollectionViewSelectionMode)).Cast<CollectionViewSelectionMode>();

    public IEnumerable<EmptyContentDisplayMode> EmptyContentDisplayModes { get; } = Enum.GetValues(typeof(EmptyContentDisplayMode)).Cast<EmptyContentDisplayMode>();

    public List<string> SourceTypes { get; } = new List<string>() { "Contacts", "Null", "Empty" };

    public ObservableCollection<Contact> ItemsSource
    {
        get => this.itemsSource;
        set => this.UpdateValue(ref this.itemsSource, value);
    }

    public string SelectedSourceType
    {
        get => this.selectedSourceType;
        set
        {
            if (this.UpdateValue(ref this.selectedSourceType, value))
            {
                switch (this.selectedSourceType)
                {
                    case "Contacts":
                        this.ItemsSource = new ObservableCollection<Contact>(ContactsProvider.GetContacts());
                        break;
                    case "Null":
                        this.ItemsSource = null;
                        break;
                    case "Empty":
                        this.ItemsSource = new ObservableCollection<Contact>();
                        break;
                    default:
                        break;
                }
            }
        }
    }

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

    public EmptyContentDisplayMode EmptyContentDisplayMode
    {
        get => this.emptyContentDisplayMode;
        set => this.UpdateValue(ref this.emptyContentDisplayMode, value);
    }

    public bool IsDragDropEnabled
    {
        get => this.isDragDropEnabled;
        set => this.UpdateValue(ref this.isDragDropEnabled, value);
    }

    public bool IsItemSwipeEnabled
    {
        get => this.isItemSwipeEnabled;
        set => this.UpdateValue(ref this.isItemSwipeEnabled, value);
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