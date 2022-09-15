using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;
using System;
using System.ComponentModel.DataAnnotations;

namespace SDKBrowserMaui.Examples.DataFormControl.GroupingCategory.GroupingExample
{
    // >> dataform-grouping-model
    public class GroupingModel : NotifyPropertyChangedBase
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
        [Display(Name = "Select accomodation", GroupName = "Personal Information")]
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
        [Display(Name = "First Name", GroupName="Personal Information")]
        public string FirstName
        {
            get => this.name;
            set => this.UpdateValue(ref this.name, value);
        }

        [Required]
        [Display(Name = "Start date", GroupName="Select Dates")]
        public DateTime? StartDate
        {
            get => this.startDate;
            set => this.UpdateValue(ref this.startDate, value);
        }

        [Required]
        [Display(Name = "End Date", GroupName = "Select Dates")]
        public DateTime? EndDate
        {
            get => this.endDate;
            set => this.UpdateValue(ref this.endDate, value);
        }

        [Required]
        [Display(Name = "Number of People", GroupName = "Personal Information")]
        public double? People
        {
            get => this.people;
            set => this.UpdateValue(ref this.people, value);
        }

        
        [Display(Name = "Visited before", GroupName = "Additional Information")]
        public bool? Visited
        {
            get => this.visited;
            set => this.UpdateValue(ref this.visited, value);
        }

        
        [Display(Name = "Duration", GroupName = "Additional Information")]
        public TimeSpan? Duration
        {
            get => this.duration;
            set => this.UpdateValue(ref this.duration, value);
        }
    }
    // << dataform-grouping-model

    // >> dataform-group-model
    public class GroupModel : NotifyPropertyChangedBase
    {
        private string name;
        private DateTime? startDate;
        private DateTime? endDate;
        private double? people;
        private bool? visited;
        private TimeSpan? duration;

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
        [Display(Name = "Number of People", GroupName = "Additional Information")]
        public double? People
        {
            get => this.people;
            set => this.UpdateValue(ref this.people, value);
        }


        [Display(Name = "Visited before", GroupName = "Additional Information")]
        public bool? Visited
        {
            get => this.visited;
            set => this.UpdateValue(ref this.visited, value);
        }


        [Display(Name = "Duration", GroupName = "Additional Information")]
        public TimeSpan? Duration
        {
            get => this.duration;
            set => this.UpdateValue(ref this.duration, value);
        }
    }
    // << dataform-group-model
}
