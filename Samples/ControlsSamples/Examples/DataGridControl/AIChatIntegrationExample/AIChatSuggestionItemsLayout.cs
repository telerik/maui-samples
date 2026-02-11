using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;

namespace QSF.Examples.DataGridControl.AIChatIntegrationExample;

public class AIChatSuggestionItemsLayout : Layout
{
    public static readonly BindableProperty SpacingProperty =
        BindableProperty.Create(nameof(Spacing), typeof(double), typeof(AIChatSuggestionItemsLayout), 0.0,
            propertyChanged: (b, o, n) => ((IView)b).InvalidateMeasure());

    private Size actualDesiredSize;

    public double Spacing
    {
        get => (double)this.GetValue(SpacingProperty);
        set => this.SetValue(SpacingProperty, value);
    }

    internal Size ActualDesiredSize
    {
        get => this.actualDesiredSize;
        set
        {
            if (this.actualDesiredSize != value)
            {
                this.actualDesiredSize = value;
                this.OnPropertyChanged();
            }
        }
    }

    protected override ILayoutManager CreateLayoutManager()
        => new AIChatSuggestionItemsLayoutManager(this);

    class AIChatSuggestionItemsLayoutManager : LayoutManager
    {
        private readonly AIChatSuggestionItemsLayout layout;
        public AIChatSuggestionItemsLayoutManager(AIChatSuggestionItemsLayout layout)
            : base(layout)
        {
            this.layout = layout;
        }

        public override Size Measure(double widthConstraint, double heightConstraint)
        {
            double measuredWidth = 0;
            double maxHeight = 0;
            int spacingCount = 0;

            foreach (var child in this.layout)
            {
                if (child.Visibility != Visibility.Collapsed)
                {
                    spacingCount++;
                    Size desiredSize = child.Measure(double.PositiveInfinity, heightConstraint);
                    measuredWidth += desiredSize.Width;
                    maxHeight = Math.Max(maxHeight, desiredSize.Height);
                }
            }

            var totalSpacing = spacingCount > 1 ? (spacingCount - 1) * this.layout.Spacing : 0;
            measuredWidth += totalSpacing;

            Size finalSize = new Size(measuredWidth, maxHeight);
            this.layout.ActualDesiredSize = finalSize;
            return new Size(Math.Min(widthConstraint, finalSize.Width), Math.Min(heightConstraint, finalSize.Height));
        }

        public override Size ArrangeChildren(Rect bounds)
        {
            double x = bounds.X;
            double spacing = this.layout.Spacing;
            bool isFirstVisibleChild = true;

            foreach (var child in this.layout)
            {
                if (child.Visibility != Visibility.Collapsed)
                {
                    if (!isFirstVisibleChild)
                    {
                        x += spacing;
                    }

                    Size desiredSize = child.DesiredSize;
                    Rect childBounds = new Rect(x, bounds.Y, desiredSize.Width, bounds.Height);
                    child.Arrange(childBounds);

                    x += desiredSize.Width;
                    isFirstVisibleChild = false;
                }
            }

            return new Size(bounds.Width, bounds.Height);
        }
    }
}