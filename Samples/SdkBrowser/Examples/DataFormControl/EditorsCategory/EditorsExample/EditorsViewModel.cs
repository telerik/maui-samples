using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using System;
using System.ComponentModel.DataAnnotations;

namespace SDKBrowserMaui.Examples.DataFormControl.EditorsCategory.EditorsExample
{
    // >> dataform-editors-model
    public class EditorsViewModel : NotifyPropertyChangedBase
    {
        private string name;
        private DateTime? startDate;
        private DateTime? endDate;
        private double? people;
        private bool? visited;
        private TimeSpan? duration;
        private EnumValue accommodation = EnumValue.Apartment;
        public enum EnumValue
        {
            SingleRoom,
            Apartment,
            House
        }

        [Required]
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
            get => this.name;
            set => this.UpdateValue(ref this.name, value);
        }

        [Required]
        [Display(Name = "Start date")]
        public DateTime? StartDate
        {
            get => this.startDate;
            set => this.UpdateValue(ref this.startDate, value);
        }

        [Required]
        [Display(Name = "End Date")]
        public DateTime? EndDate
        {
            get => this.endDate;
            set => this.UpdateValue(ref this.endDate, value);
        }

        [Required]
        [Display(Name = "Number of People")]
        [Range(1, 10)]
        public double? People
        {
            get => this.people;
            set => this.UpdateValue(ref this.people, value);
        }

        [Display(Name = "Visited before")]
        public bool? Visited
        {
            get => this.visited;
            set => this.UpdateValue(ref this.visited, value);
        }

        [Display(Name = "Duration")]
        public TimeSpan? Duration
        {
            get => this.duration;
            set => this.UpdateValue(ref this.duration, value);
        }
    }
    // << dataform-editors-model
}
