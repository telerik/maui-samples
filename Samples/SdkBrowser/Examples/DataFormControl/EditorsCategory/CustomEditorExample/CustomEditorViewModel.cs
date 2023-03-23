using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDKBrowserMaui.Examples.DataFormControl.EditorsCategory.CustomEditorExample
{
    // >> dataform-custom-editor-viewmodel
    public class CustomEditorViewModel : NotifyPropertyChangedBase
    {
        private string name;
        private double rating;
        private string accommodation;

        public CustomEditorViewModel()
        {
            this.AvailableAccommodations = new[]
            {
                "Single Room",
                "Double Room",
                "Appartment",
                "House"
            };
        }

        [Required]
        [Display(Name = "Name", Prompt="Enter first name")]
        public string Name
        {
            get => this.name;
            set => this.UpdateValue(ref this.name, value);
        }

        [Required]
        public double Rating
        {
            get => this.rating;
            set => this.UpdateValue(ref this.rating, value);
        }


        [Required]
        public string Accommodation
        {
            get => this.accommodation;
            set => this.UpdateValue(ref this.accommodation, value);
        }

        [NotMapped]
        public IList<string> AvailableAccommodations { get; }
    }
    // << dataform-custom-editor-viewmodel
}
