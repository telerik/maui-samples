using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using Telerik.AppUtils.Services;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.SlideView;

namespace QSF.Behaviors;

/// <summary>
/// A behavior that automatically scrolls a RadSlideView at a configurable interval.
/// </summary>
public class SlideViewAutoScrollBehavior : Behavior<RadSlideView>
{
    /// <summary>
    /// Identifies the <see cref="Interval"/> bindable property.
    /// </summary>
    public static readonly BindableProperty IntervalProperty = BindableProperty.Create(
        nameof(Interval), typeof(TimeSpan), typeof(SlideViewAutoScrollBehavior), TimeSpan.FromSeconds(5));

    /// <summary>
    /// Identifies the <see cref="IsAutoScrollEnabled"/> bindable property.
    /// </summary>
    public static readonly BindableProperty IsAutoScrollEnabledProperty = BindableProperty.Create(
        nameof(IsAutoScrollEnabled), typeof(bool), typeof(SlideViewAutoScrollBehavior), true,
        propertyChanged: OnIsAutoScrollEnabledChanged);

    /// <summary>
    /// Identifies the <see cref="PauseOnInteraction"/> bindable property.
    /// </summary>
    public static readonly BindableProperty PauseOnInteractionProperty = BindableProperty.Create(
        nameof(PauseOnInteraction), typeof(bool), typeof(SlideViewAutoScrollBehavior), true);

    /// <summary>
    /// Identifies the <see cref="ResumeDelayAfterInteraction"/> bindable property.
    /// </summary>
    public static readonly BindableProperty ResumeDelayAfterInteractionProperty = BindableProperty.Create(
        nameof(ResumeDelayAfterInteraction), typeof(TimeSpan), typeof(SlideViewAutoScrollBehavior), TimeSpan.FromSeconds(3));

    private RadSlideView slideView;
    private IDispatcherTimer timer;
    private bool isPausedDueToInteraction;
    private CancellationTokenSource resumeCancellationTokenSource;

    /// <summary>
    /// Gets or sets the interval between automatic slide transitions.
    /// </summary>
    public TimeSpan Interval
    {
        get => (TimeSpan)this.GetValue(IntervalProperty);
        set => this.SetValue(IntervalProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether auto-scrolling is enabled.
    /// </summary>
    public bool IsAutoScrollEnabled
    {
        get => (bool)this.GetValue(IsAutoScrollEnabledProperty);
        set => this.SetValue(IsAutoScrollEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether auto-scrolling should pause when the user interacts with the SlideView.
    /// </summary>
    public bool PauseOnInteraction
    {
        get => (bool)this.GetValue(PauseOnInteractionProperty);
        set => this.SetValue(PauseOnInteractionProperty, value);
    }

    /// <summary>
    /// Gets or sets the delay before resuming auto-scroll after user interaction.
    /// </summary>
    public TimeSpan ResumeDelayAfterInteraction
    {
        get => (TimeSpan)this.GetValue(ResumeDelayAfterInteractionProperty);
        set => this.SetValue(ResumeDelayAfterInteractionProperty, value);
    }

    /// <inheritdoc/>
    protected override void OnAttachedTo(RadSlideView bindable)
    {
        base.OnAttachedTo(bindable);

        this.slideView = bindable;
        this.slideView.CurrentItemChanged += this.OnSlideViewCurrentItemChanged;

        this.SetupTimer();

        if (this.ShouldStartTimer())
        {
            this.StartTimer();
        }
    }

    /// <inheritdoc/>
    protected override void OnDetachingFrom(RadSlideView bindable)
    {
        base.OnDetachingFrom(bindable);

        this.StopTimer();
        this.CancelResumeDelay();

        if (this.slideView != null)
        {
            this.slideView.CurrentItemChanged -= this.OnSlideViewCurrentItemChanged;
            this.slideView = null;
        }

        this.timer = null;
    }

    private static void OnIsAutoScrollEnabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var behavior = (SlideViewAutoScrollBehavior)bindable;
        
        if (behavior.ShouldStartTimer())
        {
            behavior.StartTimer();
        }
        else
        {
            behavior.StopTimer();
        }
    }

    private void SetupTimer()
    {
        if (this.slideView?.Dispatcher == null)
        {
            return;
        }

        this.timer = this.slideView.Dispatcher.CreateTimer();
        this.timer.Interval = this.Interval;
        this.timer.Tick += this.OnTimerTick;
    }

    private void StartTimer()
    {
        if (this.timer == null)
        {
            this.SetupTimer();
        }

        if (this.timer != null)
        {
            this.timer.Interval = this.Interval;
            this.timer.Start();
        }
    }

    private void StopTimer()
    {
        this.timer?.Stop();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        if (this.slideView == null || !this.IsAutoScrollEnabled || this.isPausedDueToInteraction)
        {
            return;
        }

        this.slideView.NavigateToNextItemCommand?.Execute(null);
    }

    private void OnSlideViewCurrentItemChanged(object sender, Telerik.Maui.Controls.SlideView.CurrentItemChangedEventArgs e)
    {
        if (!this.PauseOnInteraction || !this.IsAutoScrollEnabled)
        {
            return;
        }

        // User manually changed the slide, pause auto-scroll temporarily
        this.PauseAndScheduleResume();
    }

    private void PauseAndScheduleResume()
    {
        this.isPausedDueToInteraction = true;
        this.StopTimer();
        this.CancelResumeDelay();

        this.resumeCancellationTokenSource = new CancellationTokenSource();
        var token = this.resumeCancellationTokenSource.Token;

        Task.Delay(this.ResumeDelayAfterInteraction, token).ContinueWith(t =>
        {
            if (!t.IsCanceled && this.slideView != null)
            {
                this.slideView.Dispatcher.Dispatch(() =>
                {
                    this.isPausedDueToInteraction = false;
                    if (this.ShouldStartTimer())
                    {
                        this.StartTimer();
                    }
                });
            }
        }, token);
    }

    private void CancelResumeDelay()
    {
        this.resumeCancellationTokenSource?.Cancel();
        this.resumeCancellationTokenSource?.Dispose();
        this.resumeCancellationTokenSource = null;
    }

    private bool ShouldStartTimer()
    {
        var isAppUnderTest = DependencyService.Get<ITestingService>().IsAppUnderTest;
        return this.IsAutoScrollEnabled && !isAppUnderTest;
    }
}
