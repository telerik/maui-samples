using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using QSF.ViewModels;
using Telerik.Maui.Controls.RefreshView;

namespace QSF.Examples.RefreshViewControl.ConfigurationExample;

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private const int ItemsCount = 4;

    private readonly (string Title, string Description)[] messages =
    {
        ("Order shipped", "Your order #10248 is on its way."),
        ("New comment", "Anna replied to your post."),
        ("Meeting reminder", "Design review starts in 15 minutes."),
        ("Payment received", "Invoice #2041 has been paid."),
        ("Build succeeded", "The nightly build completed successfully."),
        ("New follower", "Michael started following you."),
    };

    private bool isRefreshing;
    private bool isRefreshingPinned;
    private RefreshViewIndicatorType indicatorType;
    private RefreshViewTransitionMode transitionMode;
    private RefreshViewPullDirection pullDirection = RefreshViewPullDirection.TopToBottom;
    private double pullingThreshold = 80;
    private double indicatorThreshold;
    private int nextMessageIndex;

    public ConfigurationViewModel()
    {
        this.indicatorType = RefreshViewIndicatorType.Circular;
        this.transitionMode = RefreshViewTransitionMode.Slide;
        this.Items = new ObservableCollection<NotificationItem>();
        for (int i = 0; i < ItemsCount; i++)
        {
            this.Items.Add(this.CreateNextItem());
        }

        this.RefreshCommand = new Command(async () => await this.RefreshAsync());
    }

    public ObservableCollection<NotificationItem> Items { get; }

    public ICommand RefreshCommand { get; }

    public IEnumerable<RefreshViewIndicatorType> IndicatorTypes { get; } = Enum.GetValues(typeof(RefreshViewIndicatorType)).Cast<RefreshViewIndicatorType>();

    public IEnumerable<RefreshViewTransitionMode> TransitionModes { get; } = Enum.GetValues(typeof(RefreshViewTransitionMode)).Cast<RefreshViewTransitionMode>();

    public IEnumerable<RefreshViewPullDirection> PullDirections { get; } = Enum.GetValues(typeof(RefreshViewPullDirection)).Cast<RefreshViewPullDirection>();

    public bool IsRefreshing
    {
        get => this.isRefreshing;
        set
        {
            if (this.UpdateValue(ref this.isRefreshing, value))
            {
                this.OnPropertyChanged(nameof(this.IsRefreshingPinned));
            }
        }
    }

    public bool IsRefreshingPinned
    {
        get => this.isRefreshing;
        set
        {
            this.isRefreshingPinned = value;
            this.IsRefreshing = value;
        }
    }

    public RefreshViewIndicatorType IndicatorType
    {
        get => this.indicatorType;
        set => this.UpdateValue(ref this.indicatorType, value);
    }

    public RefreshViewTransitionMode TransitionMode
    {
        get => this.transitionMode;
        set => this.UpdateValue(ref this.transitionMode, value);
    }

    public RefreshViewPullDirection PullDirection
    {
        get => this.pullDirection;
        set => this.UpdateValue(ref this.pullDirection, value);
    }

    public double PullingThreshold
    {
        get => this.pullingThreshold;
        set => this.UpdateValue(ref this.pullingThreshold, value);
    }

    public double IndicatorThreshold
    {
        get => this.indicatorThreshold;
        set => this.UpdateValue(ref this.indicatorThreshold, value);
    }

    private async Task RefreshAsync()
    {
        this.IsRefreshing = true;

        await Task.Delay(2000);

        this.Items.Clear();
        for (int i = 0; i < ItemsCount; i++)
        {
            this.Items.Add(this.CreateNextItem());
        }

        this.IsRefreshing = false;
    }

    private NotificationItem CreateNextItem()
    {
        var message = this.messages[this.nextMessageIndex % this.messages.Length];
        this.nextMessageIndex++;
        return new NotificationItem(message.Title, message.Description);
    }
}

public class NotificationItem
{
    public NotificationItem(string title, string description)
    {
        this.Title = title;
        this.Description = description;
    }

    public string Title { get; }

    public string Description { get; }
}
