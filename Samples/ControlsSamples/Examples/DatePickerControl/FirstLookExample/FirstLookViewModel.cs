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
                    Color = Color.FromArgb("#ECF6FE"),
                    ImageSource = "remote_work.png"
                },
                new CardItem()
                {
                    Color = Color.FromArgb("#FEF2F1"),
                    ImageSource = "expenses.png"
                },
                new CardItem()
                {
                    Color = Color.FromArgb("#FFF4EB"),
                    ImageSource = "time_off.png"
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
