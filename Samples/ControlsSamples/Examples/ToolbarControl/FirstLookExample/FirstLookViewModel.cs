using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.Generic;

namespace QSF.Examples.ToolbarControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private string emailText;
        private ImageSource avatarImage;
        private string selectedPriority;
        private string title;
        private string recepient;

        public FirstLookViewModel()
        {
            this.emailText = @"Dear Laura,

Thank you so much for coming out to celebrate my birthday! 

There’s no doubt this is going to be my best decade yet, and part of that is because I rang it in with you.

I hope you enjoyed your time at the party! You'll forever hold a special place in my heart and I can’t thank you enough for making the drive.

Thank you for making my birthday meaningful. Talk soon!

Your loving friend,
Gaya";
            this.avatarImage = ImageSource.FromFile("border_user1.png");
            this.title = "Thank you!";
            this.recepient = "Laura.Bell@progress.com";
            this.Priorities = new List<string>() { "High", "Normal", "Low" };
            this.SelectedPriority = this.Priorities[1];

            this.SendEmailCommand = new Command(this.SendEmail, this.CanSendEmail);
            this.SaveDraftCommand = new Command(this.SaveDraft, this.CanDraftOrArchiveEmail);
            this.ArchiveEmailCommand = new Command(this.ArchiveEmail, this.CanDraftOrArchiveEmail);
            this.ClearEmailCommand = new Command(this.ClearEmail);
        }

        public string EmailText
        {
            get { return this.emailText; }
            set
            {
                if (this.emailText != value)
                {
                    this.emailText = value;
                    OnPropertyChanged();
                    this.ChangeCommandsCanExecute();
                }
            }
        }

        public string Title
        {
            get { return this.title; }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    OnPropertyChanged();
                    this.ChangeCommandsCanExecute();
                }
            }
        }

        public string Recepient
        {
            get { return this.recepient; }
            set
            {
                if (this.recepient != value)
                {
                    this.recepient = value;
                    OnPropertyChanged();
                    this.ChangeCommandsCanExecute();
                }
            }
        }

        public ImageSource AvatarImage
        {
            get => this.avatarImage;
        }

        public List<string> Priorities { get; }

        public string SelectedPriority
        {
            get { return this.selectedPriority; }
            set
            {
                if (this.selectedPriority != value)
                {
                    this.selectedPriority = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command SendEmailCommand { get; }

        public Command SaveDraftCommand { get; }

        public Command ArchiveEmailCommand { get; }

        public Command ClearEmailCommand { get; }

        private void SendEmail()
        {
            this.SubmitOperation("Message sent.");
        }

        private bool CanSendEmail()
        {
            return !string.IsNullOrEmpty(this.Title) && !string.IsNullOrEmpty(this.Recepient) && !string.IsNullOrEmpty(this.EmailText);
        }

        private void SaveDraft()
        {
            this.SubmitOperation("Draft saved.");
        }

        private void ArchiveEmail()
        {
            this.SubmitOperation("Message archived.");
        }

        private bool CanDraftOrArchiveEmail()
        {
            return !string.IsNullOrEmpty(this.Title) || !string.IsNullOrEmpty(this.Recepient) || !string.IsNullOrEmpty(this.EmailText);
        }

        public void ClearEmail()
        {
            this.Title = this.Recepient = this.EmailText = string.Empty;
            this.SubmitOperation("Message cleared.");
            this.ChangeCommandsCanExecute();
        }

        private void SubmitOperation(string message)
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert(message);
        }

        private void ChangeCommandsCanExecute()
        {
            this.SendEmailCommand.ChangeCanExecute();
            this.SaveDraftCommand.ChangeCanExecute();
            this.ArchiveEmailCommand.ChangeCanExecute();
            this.ClearEmailCommand.ChangeCanExecute();
        }
    }
}
