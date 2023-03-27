using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ExpanderControl.GettingStartedCategory.GettingStartedExample
{
    public class ExpanderGettingStartedCSharp : RadContentView
    {
        public ExpanderGettingStartedCSharp()
        {
            // >> expander-gettingstarted-csharp
            var mainStack = new VerticalStackLayout { Margin = new Thickness(10) };
            var expander = new RadExpander { HeaderText = "More Options" };

            var stackContainer = new VerticalStackLayout { Margin = new Thickness(5) };
            var firstCheckboxStack = new HorizontalStackLayout { Spacing = 10 };
            firstCheckboxStack.Children.Add(new RadCheckBox() { VerticalOptions = LayoutOptions.Center });
            firstCheckboxStack.Children.Add(new Label { Text = "Make my profile private", HeightRequest = 60, VerticalTextAlignment = TextAlignment.Center });
            stackContainer.Children.Add(firstCheckboxStack);

            var secondCheckboxStack = new HorizontalStackLayout { Spacing = 10 };
            secondCheckboxStack.Children.Add(new RadCheckBox() { VerticalOptions = LayoutOptions.Center });
            secondCheckboxStack.Children.Add(new Label { Text = "Only show my posts to people who follow me", HeightRequest = 60, VerticalTextAlignment = TextAlignment.Center });
            stackContainer.Children.Add(secondCheckboxStack);

            expander.Content = stackContainer;
            mainStack.Children.Add(expander);
            // << expander-gettingstarted-csharp
            this.Content = mainStack;
        }
    }
}
