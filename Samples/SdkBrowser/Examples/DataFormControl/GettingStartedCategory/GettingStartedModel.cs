using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using System;
using System.ComponentModel.DataAnnotations;

namespace SDKBrowserMaui.Examples.DataFormControl.GettingStartedCategory
{
    // >> dataform-gettingstarted-model
    public class GettingStartedModel : NotifyPropertyChangedBase
    {
        private string firstName;
        private string lastName;
        private DateTime? startDate;
        private TimeSpan? startTime;
        private double? people = 1;
        private bool visited;
        private TimeSpan? duration;
        private EnumValue accommodation = EnumValue.Apartment;
        public enum EnumValue
        {
            SingleRoom,
            Apartment,
            House
        }

        [Display(Name = "Select accomodation")]
        public EnumValue Accommodation
        {
            get
            {
                return this.accommodation;
            }
            set
            {
                if (this.accommodation != value)
                {
                    this.accommodation = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName
        {
            get => this.firstName;
            set => this.UpdateValue(ref this.firstName, value);
        }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName
        {
            get => this.lastName;
            set => this.UpdateValue(ref this.lastName, value);
        }

        [Display(Name = "Star date")]
        public DateTime? StartDate
        {
            get => this.startDate;
            set => this.UpdateValue(ref this.startDate, value);
        }

        [Display(Name = "Time")]
        public TimeSpan? EndDate
        {
            get => this.startTime;
            set => this.UpdateValue(ref this.startTime, value);
        }

        [Display(Name = "Number of People")]
        public double? People
        {
            get => this.people;
            set => this.UpdateValue(ref this.people, value);
        }

        [Required]
        [Display(Name = "Visited before")]
        public bool Visited
        {
            get => this.visited;
            set => this.UpdateValue(ref this.visited, value);
        }

        [Required]
        [Display(Name = "Duration")]
        public TimeSpan? Duration
        {
            get => this.duration;
            set => this.UpdateValue(ref this.duration, value);
        }
    }
    // << dataform-gettingstarted-model
}
