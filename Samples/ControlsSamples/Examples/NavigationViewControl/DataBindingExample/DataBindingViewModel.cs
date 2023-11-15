using QSF.ViewModels;
using System;
using Telerik.Maui;

namespace QSF.Examples.NavigationViewControl.DataBindingExample;

public class DataBindingViewModel : ExampleViewModel
{
    private MailBox selectedItem;

    private Func<object, bool> itemFilter;

    public DataBindingViewModel()
    {
        this.MailBoxes = new ObservableItemCollection<MailBox>
        {
            new MailBox{ Text = "Inbox", Icon = "\ue8a2" },
            new MailBox{ Text = "Social", Icon = "\ue8a3" },
            new MailBox{ Text = "Starred", Icon = "\ue801" },
            new MailBox{ Text = "Snoozed", Icon = "\ue878" },
            new MailBox{ Text = "Important", Icon = "\ue83c" },
            new MailBox{ Text = "Sent", Icon = "\ue82d"  },
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
            new MailItem { Sender = "Post Master", Title = "You have new held messages", Description = "You can release all of your held messages and permit or block future emails from the senders, or manage messages individually.", Date = "12 Oct", IsSpam = true },
            new MailItem { Sender = "Jordan L. Jarrett", Title = "[Action required] Team Building event - Sofia Comedy Club", Description = "Sending a reminder for the Team Building event for Innovation & UX teams.", Date ="12 Oct", IsPromotion = true },
            new MailItem { Sender = "Workplace Operations", Title = "Workplace Operations Newsletter October", Description = "The new server room and R&D lab rebuild was completed on August 25th. It was a great cross-team effort to have this project completed.", Date ="12 Oct" },
            new MailItem { Sender = "Ralph Anderson", Title = "Updates from All Company and other communities", Description = "Our Madison teams enjoyed some friendly pinball competition at an arcade establishment.", Date ="12 Oct", IsSpam = true },
            new MailItem { Sender = "[Aha!] ", Title = "[Aha!] Your weekly summary", Description = "Your work at a glance", Date ="12 Oct", IsSpam = true },
            new MailItem { Sender = "Azure DevOps", Title = "Design for NavigationViewExample", Description = "Michel Roots created Task 72152 ControlsSamples: Design for NavigationView DataBinding example", Date ="12 Oct", IsSpam = true, IsPromotion = true },
            new MailItem { Sender = "Workplace Operations", Title = "INFORMATIONAL: Fire Evacuation Drill", Description = "We would like to inform you that on Friday, the Landlord will perform planned fire evacuation of the building.", Date ="10 Oct", IsPromotion = true },
            new MailItem { Sender = "Tableau", Title = "Tableau Server Upgrade", Description = "On Saturday, October 21, we will upgrade our Tableau server to the latest version as we prepare for year-end processing. ", Date ="12 Oct", IsPromotion = true },
            new MailItem { Sender = "Claudette Potter", Title = "Updates from All Company and other communities", Description = "My name is James Johnes. I am a Customer Success Manager", Date ="12 Oct" },
            new MailItem { Sender = "Leadership Team", Title = "Leadership talks - Demystifying M&A", Description = "We are happy to invite you to a “Demystifying M&A” session with really cool guests", Date ="12 Oct", IsSpam = true },
            new MailItem { Sender = "Information Security", Title = "Security Reminder: Reporting Suspicious Emails with Mimecast", Description = "We’ve had a great start to Cybersecurity Awareness Month and thank you all for your participation!", Date ="12 Oct" },
            new MailItem { Sender = "Praise", Title = "Your Praise Excel Award", Description = "Thank you  for your great contribution to the new release success. Your efforts, devotion, experience…", Date ="12 Oct", IsPromotion = true },
            new MailItem { Sender = "Gene Houlihan", Title = "October 16th OKRs Status Report", Description = "Proceed with enhancing the performance of Our Extensions. Additionally, continue the development of functional tests.", Date ="12 Oct", },
            new MailItem { Sender = "Ronnie J. Edwards", Title = "DX Weekly Update ", Description = "We showed innovation, covered interactive customer case studies, and highlighted our wins against competitors. ", Date ="09 Oct"},
            new MailItem { Sender = "Workplace Operations", Title = "New group activities", Description = "We are thrilled to announce new group activities at the gym (Playground), where you can enjoy the magic of the folk dances!", Date ="12 Oct" },
            new MailItem { Sender = "IT HelpDesk", Title = "VPN Maintenance ", Description = "We would like to inform you of an upcoming software upgrade of the VPN gateways servicing our VPN.", Date ="11 Oct" },
            new MailItem { Sender = "Sharon Murray", Title = "Quest for the best monthly e-mail", Description = "We have plenty of job openings available and we’re trying to spread the word! See our current postings below. If someone you know could be a great fit, please submit a referral.", Date ="12 Oct" },
            new MailItem { Sender = "People Team", Title = "Q4 Charitable Giving Submissions Are Open", Description = "Thank you to those that participated in the Q3 Charitable Giving Program. It was great to see the many organizations that you support!", Date ="10 Oct" },
            new MailItem { Sender = "Karen West", Title = "Learning Hour - Emotional Intelligence Follow Up", Description = "Thank you so much for expressing interest in our Learning Hour on the topic of Emotional Intelligence! ", Date ="12 Oct" },
            new MailItem { Sender = "Workplace Operations", Title = "NFORMATIONAL: Planned maintenance", Description = "We would like to inform you that on Friday, the landlord will perform a planned maintenance of the building’s electrical substation.", Date ="11 Oct", IsPromotion = true },
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

    public Func<object, bool> ItemFilter
    {
        get
        {
            return this.itemFilter;
        }
        private set
        {
            if (this.itemFilter != value)
            {
                this.itemFilter = value;
                this.OnPropertyChanged();
            }
        }
    }

    private void OnFilterChanged()
    {
        var item = this.SelectedItem.Text;

        if (item == "Spam")
        {
            this.ItemFilter = item => ((MailItem)item).IsSpam == true;
        }
        else if (item == "Promotions")
        {
            this.ItemFilter = item => ((MailItem)item).IsPromotion == true;
        }
        else
        {
            this.ItemFilter = item => true;
        }
    }
}
