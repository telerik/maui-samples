using QSF.ViewModels;
using System;
using Telerik.Maui;
using Telerik.Maui.Controls.Data;

namespace QSF.Examples.NavigationViewControl.DataBindingExample;

public class DataBindingViewModel : ExampleViewModel, IFilter
{
    private MailBox selectedItem;

    public DataBindingViewModel()
    {
        this.MailBoxes = new ObservableItemCollection<MailBox>
        {
            new MailBox{ Text = "Inbox", Icon = "\ue8a2" },
            new MailBox{ Text = "Social", Icon = "\ue8a3" },
            new MailBox{ Text = "Starred", Icon = "\ue801" },
            new MailBox{ Text = "Snoozed", Icon = "\ue878" },
            new MailBox{ Text = "Important", Icon = "\ue83c" },
            new MailBox{ Text = "Sent", Icon = "\ue82d" },
            new MailBox{ Text = "Scheduled", Icon = "\ue8a0" },
            new MailBox{ Text = "Drafts", Icon = "\ue828" },
            new MailBox{ Text = "Spam", Icon = "\ue82e", HasMail = true, Number = 5 },
            new MailBox{ Text = "Bin", Icon = "\ue827" },
            new MailBox{ Text = "Archive", Icon = "\ue826" },
            new MailBox{ Text = "Promotions", Icon = "\ue89f", HasMail = true, Number = 6 },
            new MailBox{ Text = "Chats", Icon = "\ue83b" },
            new MailBox{ Text = "All Mail", Icon = "\ue89e" },
            new MailBox{ Text = "Birthdays", Icon = "\ue83a" },
            new MailBox{ Text = "Custom Folder", Icon = "\ue8a1" },
            new MailBox{ Text = "Custom Folder", Icon = "\ue8a1" },
            new MailBox{ Text = "Custom Folder", Icon = "\ue8a1" },
        };

        this.Mails = new ObservableItemCollection<MailItem>
        {
            new MailItem { Sender = "Phillip Anthropy", Title = "New components and features documentation", Description = "Could you please update me on the status of the .NET MAUI Chat documentation?", Date = "26 Jul" },
            new MailItem { Sender = "Spruce Springclean", Title = "Monthly OKRs Status Report", Description = "Proceed with enhancing the performance of Our Extensions. Additionally, continue the development of functional tests.", Date ="26 Jul", },
            new MailItem { Sender = "Norman Gordon", Title = "ComboBox Filtering API Proposal", Description = "Hi guys, I am sending over the API Proposal of ComboBox's new filtering feature. Feel free to share your thoughts and comments direcly in the document.", Date = "25 Jul" },
            new MailItem { Sender = "Spruce Springclean", Title = "Monthly OKRs Status Report", Description = "Proceed with enhancing the performance of Our Extensions. Additionally, continue the development of functional tests.", Date ="24 Jul", IsSpam = true },
            new MailItem { Sender = "Jane Poe", Title = "Weekly Update", Description = "We showed innovation, covered interactive customer case studies, and highlighted our wins against competitors.", Date ="24 Jul", IsSpam = true },
            new MailItem { Sender = "Karren Koe", Title = "[Design] MAUI - Control Samples - CollectionView", Description = "Hello, I am sending over some proposals for CollectionView's new features demos designs. I'll be more than happy to discuss them with you and make a decision.", Date = "24 Jul" },
            new MailItem { Sender = "Jane Poe", Title = "Social Hour", Description = "Hi all, if you are in the office, please join us for a social hour after the All Hands in the cafeteria. Food and drinks will be provided.", Date ="24 Jul", IsPromotion = true },
            new MailItem { Sender = "Phillip Anthropy", Title = "New Components Customisation Examples", Description = "Showcase customisation capabilities of new features - e.g., defining custom CollectionView drag visual template.", Date = "23 Jul" },
            new MailItem { Sender = "Jane Poe", Title = "Quest for the best monthly e-mail", Description = "We have plenty of job openings available and we're trying to spread the word! See our current postings below. If someone you know could be a great fit, please submit a referral.", Date ="23 Jul", IsPromotion = true },
            new MailItem { Sender = "Karren Koe", Title = "[Design] Desktop Controls Samples main navigation icons sizes", Description = "Please find attached the updates regarding the icons used in the main navigation component for Desktop.", Date = "22 Jul" },
            new MailItem { Sender = "Hilary Ouse", Title = "Learning Hour - Emotional Intelligence Follow Up", Description = "Thank you so much for expressing interest in our Learning Hour on the topic of Emotional Intelligence! ", Date ="22 Jul", IsSpam = true },
            new MailItem { Sender = "Karren Koe", Title = "Controls Samples App Redesign", Description = "Hi guys, I am happy to announce the new Controls Samples App design! If you have any questions or concerns, please don't hesitate to contact me.", Date = "22 Jul" },
            new MailItem { Sender = "Phillip Anthropy", Title = "[Action required] Team Building event - Sofia Comedy Club", Description = "Sending a reminder for the Team Building event for Innovation & UX teams.", Date ="18 Jul", IsPromotion = true },
            new MailItem { Sender = "Hilary Ouse", Title = "Sign Up for the Next Learning Hour", Description = "For our second Learning Hour, we're doing something that could be considered counter-intuitive: inviting you to a meeting to talk about meetings - how to plan and execute them effectively.", Date = "12 Jul", IsPromotion = true },
            new MailItem { Sender = "Spruce Springclean", Title = "Bi-Weekly OKR Status Report", Description = "I am sending the bi-Weekly OKR Status Report.", Date ="12 Jul", },
            new MailItem { Sender = "Norman Gordon", Title = "Proposal for a new property in AutoComplete", Description = "In the last few days I've been working on a bug regarding the AutoComplete control and came up with a proposal for a new property. Let me know what you think.", Date = "11 Jul" },
            new MailItem { Sender = "Spruce Springclean", Title = "Product OKRs Recap and Goals", Description = "Last week we had a mid-year strategic sync where we reviewed H1 progress on OKRs and H2 priorities and goals. Below you can find the details.", Date ="10 Jul" },
            new MailItem { Sender = "Jane Poe", Title = "Weekly Update - Marketing", Description = "Hello All, a lot happening on the Marketing front. A few highlights below:", Date ="9 Jul", IsSpam = true },
            new MailItem { Sender = "Karren Koe", Title = "Modernize Visual Studio Extensions", Description = "Hello, as we continue to update our Visual Studio Extensions, I'm presenting the updated screens with the new design.", Date = "9 Jul" },
            new MailItem { Sender = "Jane Poe", Title = "Weekly Update - Sales", Description = "This update will be a bit longer one with lots of interesting/important announcements so please read on!", Date ="9 Jul", IsSpam = true },
        };

        this.SelectedItem = this.MailBoxes[0];
    }

    public MailBox SelectedItem
    {
        get => this.selectedItem;
        set
        {
            if (this.selectedItem != value)
            {
                this.selectedItem = value;
                this.OnPropertyChanged();
                this.OnFilterChanged();
            }
        }
    }

    public ObservableItemCollection<MailBox> MailBoxes { get; set; }

    public ObservableItemCollection<MailItem> Mails { get; set; }

    public FilterDescriptorCollection FilterDescriptors { get; set; }

    public bool PassesFilter(object item)
    {
        var mailItem = ((MailItem)item);
        var text = this.SelectedItem.Text;

        if (text == "Spam")
        {
            return mailItem.IsSpam;
        }

        if (text == "Promotions")
        {
            return mailItem.IsPromotion;
        }

        return true;
    }

    private void OnFilterChanged()
    {
        var descriptors = this.FilterDescriptors;
        if (descriptors == null)
        {
            return;
        }

        descriptors.Clear();

        var descriptor = new DelegateFilterDescriptor();
        descriptor.Filter = this;
        descriptors.Add(descriptor);
    }
}
