using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataFormControl.GettingStartedCategory.CustomGenerateExample
{
    // >> dataform-custom-generate-viewmodel
    public class CustomGenerateViewModel : NotifyPropertyChangedBase
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
        public string FirstName
        {
            get => this.firstName;
            set => this.UpdateValue(ref this.firstName, value);
        }

        [Required]
        public string LastName
        {
            get => this.lastName;
            set => this.UpdateValue(ref this.lastName, value);
        }

        public DateTime? StartDate
        {
            get => this.startDate;
            set => this.UpdateValue(ref this.startDate, value);
        }

        [Category("Details")]
        public TimeSpan? EndDate
        {
            get => this.startTime;
            set => this.UpdateValue(ref this.startTime, value);
        }

        [Category("Details")]
        public double? People
        {
            get => this.people;
            set => this.UpdateValue(ref this.people, value);
        }

        [Required]
        [Category("Ignored")]
        public bool Visited
        {
            get => this.visited;
            set => this.UpdateValue(ref this.visited, value);
        }

        [Required]
        [Category("Ignored")]
        public TimeSpan? Duration
        {
            get => this.duration;
            set => this.UpdateValue(ref this.duration, value);
        }
    }
    // << dataform-custom-generate-viewmodel
}
