using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls;

namespace QSF.Examples.DataGridControl.AIChatIntegrationExample;

public partial class AIChatSuggestionsView : ContentView
{
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(AIChatSuggestionsView), null,
            propertyChanged: (b, o, n) => ((AIChatSuggestionsView)b).ResetScrollPosition());

    public static readonly BindableProperty ItemTapCommandProperty =
        BindableProperty.Create(nameof(ItemTapCommand), typeof(ICommand), typeof(AIChatSuggestionsView), null);

    private const uint AnimationDuration = 250;
    private RadTemplatedButton prevButton;
    private RadTemplatedButton nextButton;
    private AIChatSuggestionItemsLayout itemsContainer;
    private double currentOffset = 0;
    private double maxOffset = 0;
    private double stepSize = 0;
    private bool isAnimating = false;

    public AIChatSuggestionsView()
    {
        this.InitializeComponent();
    }

    public IEnumerable ItemsSource
    {
        get => (IEnumerable)this.GetValue(ItemsSourceProperty);
        set => this.SetValue(ItemsSourceProperty, value);
    }

    public ICommand ItemTapCommand
    {
        get => (ICommand)this.GetValue(ItemTapCommandProperty);
        set => this.SetValue(ItemTapCommandProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        if (this.prevButton != null)
        {
            this.prevButton.Clicked -= this.OnPrevButtonClicked;
        }

        if (this.nextButton != null)
        {
            this.nextButton.Clicked -= this.OnNextButtonClicked;
        }

        if (this.itemsContainer != null)
        {
            this.itemsContainer.PropertyChanged -= this.OnItemsContainerPropertyChanged;
        }
        
        base.OnApplyTemplate();

        this.prevButton = this.GetTemplateChild("PART_PreviousButton") as RadTemplatedButton;
        this.nextButton = this.GetTemplateChild("PART_NextButton") as RadTemplatedButton;
        this.itemsContainer = this.GetTemplateChild("PART_ItemsContainer") as AIChatSuggestionItemsLayout;

        if (this.prevButton != null)
        {
            this.prevButton.Clicked += this.OnPrevButtonClicked;
        }

        if (this.nextButton != null)
        {
            this.nextButton.Clicked += this.OnNextButtonClicked;
        }

        if (this.itemsContainer != null)
        {
            this.itemsContainer.PropertyChanged += this.OnItemsContainerPropertyChanged;
        }

        this.ResetScrollPosition();
    }

    private async void OnPrevButtonClicked(object sender, EventArgs e)
    {
        if (this.isAnimating || this.itemsContainer == null)
        {
            return;
        }

        double newOffset = Math.Min(this.currentOffset + this.stepSize, 0);
        await this.AnimateToOffsetAsync(newOffset);
    }

    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        if (this.isAnimating || this.itemsContainer == null)
        {
            return;
        }

        double newOffset = Math.Max(this.currentOffset - this.stepSize, -this.maxOffset);
        await this.AnimateToOffsetAsync(newOffset);
    }

    private void OnItemsContainerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AIChatSuggestionItemsLayout.ActualDesiredSize) || e.PropertyName == nameof(Width))
        {
            this.MeasureContent();
            this.UpdateButtonStates();
        }
    }

    private void ResetScrollPosition()
    {
        if (this.itemsContainer == null || !this.IsLoaded)
        {
            return;
        }

        this.currentOffset = 0;
        this.itemsContainer.TranslationX = 0;
        this.maxOffset = 0;
        this.stepSize = 0;
        this.UpdateButtonStates();
    }

    private void MeasureContent()
    {
        if (this.itemsContainer == null || !this.IsLoaded)
        {
            return;
        }

        this.maxOffset = Math.Max(0, this.itemsContainer.ActualDesiredSize.Width - this.itemsContainer.Width);
        this.stepSize = this.itemsContainer.Width * 0.33;
    }

    private async Task AnimateToOffsetAsync(double newOffset)
    {
        if (this.itemsContainer == null)
        {
            return;
        }

        this.isAnimating = true;

#if NET10_0_OR_GREATER
        await this.itemsContainer.TranslateToAsync(newOffset, 0, AnimationDuration, Easing.CubicOut);
#else
        await this.itemsContainer.TranslateTo(newOffset, 0, AnimationDuration, Easing.CubicOut);
#endif
        
        this.currentOffset = newOffset;
        this.isAnimating = false;
        
        this.UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        if (this.prevButton != null)
        {
            this.prevButton.IsEnabled = this.currentOffset < 0;
        }

        if (this.nextButton != null)
        {
            this.nextButton.IsEnabled = this.maxOffset > 0 && this.currentOffset > -this.maxOffset;
        }
    }
}