using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.MapControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private Telerik.Maui.Controls.Compatibility.ShapefileReader.IShape selectedShape;
        private bool isOpen;
        private string stateName;
        private string stateDensity;

        public FirstLookViewModel()
        {
            this.Densities = new ObservableCollection<DensityItem>
            {
                new DensityItem("1000+", Color.FromRgba("#14333D")),
                new DensityItem("50-100", Color.FromRgba("#3C99B7")),
                new DensityItem("500-1000", Color.FromRgba("#1E4C5C")),
                new DensityItem("20-50", Color.FromRgba("#46B2D6")),
                new DensityItem("200-500", Color.FromRgba("#28667A")),
                new DensityItem("0-20", Color.FromRgba("#95D3E8")),
                new DensityItem("100-200", Color.FromRgba("#327F99"))
            };
            this.ClosePopupCommand = new Command(OnClosePopupCommandExecuted);
        }

        public ObservableCollection<DensityItem> Densities { get; set; }

        public Command ClosePopupCommand { get; set; }

        public Telerik.Maui.Controls.Compatibility.ShapefileReader.IShape SelectedShape
        {
            get
            {
                return this.selectedShape;
            }
            set
            {
                this.UpdateValue(ref this.selectedShape, value);

                if (this.selectedShape != null)
                {
                    this.StateName = this.selectedShape.GetAttribute("STATE_NAME").ToString();
                    this.StateDensity = string.Format("Density: {0}", this.selectedShape.GetAttribute("POP_DENSIT").ToString());
                    this.IsOpen = true;
                }
            }
        }

        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }
            set
            {
                this.UpdateValue(ref this.isOpen, value);

                if (!this.isOpen && this.selectedShape != null)
                {
                    this.SelectedShape = null;
                }
            }
        }

        public string StateName
        {
            get
            {
                return this.stateName;
            }
            set
            {
                this.UpdateValue(ref this.stateName, value);
            }
        }

        public string StateDensity
        {
            get
            {
                return this.stateDensity;
            }
            set
            {
                this.UpdateValue(ref this.stateDensity, value);
            }
        }

        private void OnClosePopupCommandExecuted(object obj)
        {
            this.IsOpen = false;
        }
    }
}