using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Input;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.AccordionControl.GettingStartedCategory.GettingStartedExample;

public class AccordionGettingStartedCSharp : ContentView
{
    public AccordionGettingStartedCSharp()
    {
        // >> accordion-gettingstarted-csharp
        var accordion = new RadAccordion();

        var attachmentsSection = new AccordionItem { HeaderText = "Attachments" };
        attachmentsSection.Content = new Label { Text = "Attach files", Margin = 30 };
        accordion.Children.Add(attachmentsSection);

        var commentsSection = new AccordionItem { HeaderText = "Comments" };
        commentsSection.Content = new Label { Text = "Add your comment here", Margin = 30 };
        accordion.Children.Add(commentsSection);

        var ratingSection = new AccordionItem { IsExpanded = true, HeaderText = "Rating" };
        ratingSection.Content = new RadShapeRating { Margin = 20 };
        accordion.Children.Add(ratingSection);
        // << accordion-gettingstarted-csharp
        Content = accordion;
    }
}
