using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QSF.Examples.DataFormControl.CustomEditorsExample
{
    public class FeedBackFormViewModel : NotifyPropertyChangedBase
    {
        private string firstName;
        private string lastName;
        private string email;
        private string location;
        private ObservableCollection<string> locations = new ObservableCollection<string>();
        private double rating;
        private string comments;
        private bool? willJoinNextTrip = false;

        public FeedBackFormViewModel()
        {
            this.Locations = new ObservableCollection<string>()
            {
                "Costa Rica",
                "Maldives",
                "Bora Bora",
                "New York",
                "London",
                "Mexico",
                "Las Vegas",
                "Tokyo",
                "Madrid",
                "Bali",
                "Paris",
            };
            this.Location = this.Locations[3];
        }

        [Required]
        [Display(Name = "First Name", Prompt = "Enter Fist Name")]
        public string FirstName
        {
            get => this.firstName;
            set => this.UpdateValue(ref this.firstName, value);
        }

        [Required]
        [Display(Name = "Last Name", Prompt = "Enter Last Name")]
        public string LastName
        {
            get => this.lastName;
            set => this.UpdateValue(ref this.lastName, value);
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Enter Email")]
        public string Email
        {
            get => this.email;
            set => this.UpdateValue(ref this.email, value);
        }

        [Required]
        public string Location
        {
            get => this.location;
            set => this.UpdateValue(ref this.location, value);
        }

        [NotMapped]
        public ObservableCollection<string> Locations
        {
            get => this.locations;
            set => this.UpdateValue(ref this.locations, value);
        }


        [Required]
        [Display(Name = "Rate your experience")]
        public double Rating
        {
            get => this.rating;
            set => this.UpdateValue(ref this.rating, value);
        }

        [Display(Name = "Additional comments")]
        public string Comments
        {
            get => this.comments;
            set => this.UpdateValue(ref this.comments, value);
        }

        [Required]
        [Display(Name = "Will attend next trip")]
        public bool? WillJoinNextTrip
        {
            get => this.willJoinNextTrip;
            set => this.UpdateValue(ref this.willJoinNextTrip, value);
        }
    }
}
