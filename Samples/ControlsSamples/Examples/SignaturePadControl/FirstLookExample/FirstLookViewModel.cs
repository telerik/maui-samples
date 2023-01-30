using QSF.ViewModels;
using System.Collections.Generic;

namespace QSF.Examples.SignaturePadControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private bool isSigned = false;
        private string selectedTip;

        public FirstLookViewModel()
        {
            this.Tips = new List<string>() { "No Tip", "5$", "15$", "20$" };
            this.SelectedTip = this.Tips[1];
        }

        public bool IsSigned
        {
            get { return this.isSigned; }
            set
            {
                if (this.isSigned != value)
                {
                    this.isSigned = value;
                    OnPropertyChanged();
                }

            }
        }

        public string SelectedTip
        {
            get { return this.selectedTip; }
            set
            {
                if (this.selectedTip != value)
                {
                    this.selectedTip = value;
                    OnPropertyChanged();
                }

            }
        }
        public List<string> Tips { get; set; }
    }
}
