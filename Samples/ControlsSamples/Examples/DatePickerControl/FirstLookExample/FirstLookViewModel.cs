using QSF.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.Services;
using QSF.ExampleUtilities;

namespace QSF.Examples.DatePickerControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private DateTime? startDate;
        private DateTime? endDate;

        public FirstLookViewModel()
        {
            this.SendRequestCommand = new Command(this.SendRequest, this.CanSendRequest);
            this.StartDate = DateTime.Now.AddDays(-1);
            this.EndDate = DateTime.Now;
            this.Cards = new List<CardItem>()
            {
                new CardItem()
                {
                    BackgroundColor = Color.FromArgb("#ECF6FE"),
                    BorderColor = Color.FromArgb("#D6EBFC"),
                    SelectedBorderColor = Color.FromArgb("#0E88F2"),
                    ImageSource = "remote_work.png",
                    Label = "WFH Request"
                },
                new CardItem()
                {
                    BackgroundColor = Color.FromArgb("#FEF2F1"),
                    BorderColor = Color.FromArgb("#FEE5E3"),
                    SelectedBorderColor = Color.FromArgb("#F85446"),
                    ImageSource = "sick_leave.png",
                    Label = "Sick Leave"
                },
                new CardItem()
                {
                    BackgroundColor = Color.FromArgb("#FFF4EB"),
                    BorderColor = Color.FromArgb("#FFE9D8"),
                    SelectedBorderColor = Color.FromArgb("#FFAC3E"),
                    ImageSource = "time_off.png",
                    Label = "PTO Request"
                }
            };
        }

        public IList<CardItem> Cards { get; }

        public DateTime? StartDate
        {
            get => this.startDate;
            set
            {
                if (this.UpdateValue(ref this.startDate, value))
                {
                    this.OnStartDateChanged();
                }
            }
        }

        public DateTime? EndDate
        {
            get => this.endDate;
            set
            {
                if (this.UpdateValue(ref this.endDate, value))
                {
                    this.OnEndDateChanged();
                }
            }
        }

        public Command SendRequestCommand { get; }

        private void OnStartDateChanged()
        {
            if (this.EndDate < this.StartDate)
            {
                this.EndDate = this.StartDate;
            }
            this.SendRequestCommand.ChangeCanExecute();
        }

        private void OnEndDateChanged()
        {
            if (this.StartDate > this.EndDate)
            {
                this.StartDate = this.EndDate;
            }
            this.SendRequestCommand.ChangeCanExecute();
        }

        private void SendRequest()
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Request sent");
            this.StartDate = null;
            this.EndDate = null;
        }

        private bool CanSendRequest()
        {
            return this.StartDate != null && this.EndDate != null;
        }
    }
}
