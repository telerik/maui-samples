using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace QSF.Examples.AutoCompleteControl.FirstLookExample
{
    public partial class FirstLookView : Grid
    {
        public FirstLookView()
        {
            this.InitializeComponent();

            this.autoComplete.Tokens.CollectionChanged += Tokens_CollectionChanged;
        }

        private void Tokens_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.BindingContext is FirstLookViewModel vm)
            {
                vm.RecepientCount = this.autoComplete.Tokens.Count;
            }
        }
    }
}
