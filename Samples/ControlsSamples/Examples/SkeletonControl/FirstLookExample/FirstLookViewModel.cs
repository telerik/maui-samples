using QSF.Examples.CollectionViewControl.ItemSwipeExample;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui;

namespace QSF.Examples.SkeletonControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private bool isLoading;
        private CancellationTokenSource loadCts;

        private readonly List<MailItem> seedItems = new()
        {
            new MailItem { Sender = "Phillip Anthropy", Image = "person_9.png", Title = "New components and features documentation", Description = "Could you please update me on the status of the .NET MAUI Chat documentation?", Date = "26 Jul", IsFavourite = true },
            new MailItem { Sender = "Spruce Springclean", Image = "person_3.png", Title = "Monthly OKRs Status Report", Description = "Proceed with enhancing the performance of Our Extensions. Additionally, continue the development of functional tests.", Date ="26 Jul" },
            new MailItem { Sender = "Norman Gordon", Image = "person_2.png", Title = "ComboBox Filtering API Proposal", Description = "Hi guys, I am sending over the API Proposal of ComboBox's new filtering feature. Feel free to share your thoughts and comments direcly in the document.", Date = "25 Jul" },
            new MailItem { Sender = "Spruce Springclean", Image = "person_3.png", Title = "Monthly OKRs Status Report", Description = "Proceed with enhancing the performance of Our Extensions. Additionally, continue the development of functional tests.", Date ="24 Jul" },
            new MailItem { Sender = "Jane Poe", Image = "person_8.png", Title = "Weekly Update", Description = "We showed innovation, covered interactive customer case studies, and highlighted our wins against competitors.", Date ="24 Jul", IsFavourite = true },
            new MailItem { Sender = "Karren Koe", Image = "person_1.png", Title = "[Design] MAUI - Control Samples - CollectionView", Description = "Hello, I am sending over some proposals for CollectionView's new features demos designs. I'll be more than happy to discuss them with you and make a decision.", Date = "24 Jul" },
            new MailItem { Sender = "Jane Poe", Image = "person_8.png", Title = "Social Hour", Description = "Hi all, if you are in the office, please join us for a social hour after the All Hands in the cafeteria. Food and drinks will be provided.", Date ="24 Jul" },
            new MailItem { Sender = "Phillip Anthropy", Image = "person_9.png", Title = "New Components Customization Examples", Description = "Showcase customisation capabilities of new features - e.g., defining custom CollectionView drag visual template.", Date = "23 Jul", IsFavourite = true },
            new MailItem { Sender = "Jane Poe", Image = "person_8.png", Title = "Quest for the best monthly e-mail", Description = "We have plenty of job openings available and we're trying to spread the word! See our current postings below. If someone you know could be a great fit, please submit a referral.", Date ="23 Jul" },
            new MailItem { Sender = "Karren Koe", Image = "person_1.png", Title = "[Design] Desktop Controls Samples main navigation icons sizes", Description = "Please find attached the updates regarding the icons used in the main navigation component for Desktop.", Date = "22 Jul" },
            new MailItem { Sender = "Hilary Ouse", Image = "person_4.png", Title = "Learning Hour - Emotional Intelligence Follow Up", Description = "Thank you so much for expressing interest in our Learning Hour on the topic of Emotional Intelligence! ", Date ="22 Jul", IsFavourite = true },
            new MailItem { Sender = "Karren Koe", Image = "person_1.png", Title = "Controls Samples App Redesign", Description = "Hi guys, I am happy to announce the new Controls Samples App design! If you have any questions or concerns, please don't hesitate to contact me.", Date = "22 Jul" },
            new MailItem { Sender = "Phillip Anthropy", Image = "person_9.png", Title = "[Action required] Team Building event - Sofia Comedy Club", Description = "Sending a reminder for the Team Building event for Innovation & UX teams.", Date ="18 Jul" },
            new MailItem { Sender = "Hilary Ouse", Image = "person_9.png", Title = "Sign Up for the Next Learning Hour", Description = "For our second Learning Hour, we're doing something that could be considered counter-intuitive: inviting you to a meeting to talk about meetings - how to plan and execute them effectively.", Date = "12 Jul", IsFavourite = true },
            new MailItem { Sender = "Spruce Springclean", Image = "person_3.png", Title = "Bi-Weekly OKR Status Report", Description = "I am sending the bi-Weekly OKR Status Report.", Date ="12 Jul" },
            new MailItem { Sender = "Norman Gordon", Image = "person_2.png", Title = "Proposal for a new property in AutoComplete", Description = "In the last few days I've been working on a bug regarding the AutoComplete control and came up with a proposal for a new property. Let me know what you think.", Date = "11 Jul" },
            new MailItem { Sender = "Spruce Springclean", Image = "person_3.png", Title = "Product OKRs Recap and Goals", Description = "Last week we had a mid-year strategic sync where we reviewed H1 progress on OKRs and H2 priorities and goals. Below you can find the details.", Date ="10 Jul", IsFavourite = true },
            new MailItem { Sender = "Jane Poe", Image = "person_8.png", Title = "Weekly Update - Marketing", Description = "Hello All, a lot happening on the Marketing front. A few highlights below:", Date ="9 Jul" },
            new MailItem { Sender = "Karren Koe", Image = "person_1.png", Title = "Modernize Visual Studio Extensions", Description = "Hello, as we continue to update our Visual Studio Extensions, I'm presenting the updated screens with the new design.", Date = "9 Jul" },
            new MailItem { Sender = "Jane Poe", Image = "person_8.png", Title = "Weekly Update - Sales", Description = "This update will be a bit longer one with lots of interesting/important announcements so please read on!", Date ="9 Jul" },
        };

        public FirstLookViewModel()
        {
            this.Mails = new ObservableItemCollection<MailItem>();
            this.ReloadCommand = new Microsoft.Maui.Controls.Command(async () => await this.ReloadAsync());
            _ = this.ReloadAsync();
        }

        public ObservableItemCollection<MailItem> Mails { get; }
        public ICommand ReloadCommand { get; }

        public bool IsLoading
        {
            get => this.isLoading;
            private set
            {
                if (this.isLoading != value)
                {
                    this.isLoading = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private async Task ReloadAsync()
        {
            this.loadCts?.Cancel();
            this.loadCts = new CancellationTokenSource();
            var token = this.loadCts.Token;

            this.IsLoading = true;

            try
            {
                this.Mails.Clear();
                await Task.Delay(2000, token);
                token.ThrowIfCancellationRequested();

                foreach (var item in this.seedItems)
                {
                    token.ThrowIfCancellationRequested();
                    this.Mails.Add(item);
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                if (!token.IsCancellationRequested)
                {
                    this.IsLoading = false;
                }
            }
        }
    }
}