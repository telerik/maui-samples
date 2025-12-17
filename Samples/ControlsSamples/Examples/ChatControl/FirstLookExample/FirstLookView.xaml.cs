using Telerik.Maui.Controls;

namespace QSF.Examples.ChatControl.FirstLookExample;

public partial class FirstLookView : RadContentView
{
    public FirstLookView()
    {
        this.InitializeComponent();
        this.Loaded += this.OnLoaded;
        this.Unloaded += this.OnUnloaded;
    }

    private void OnLoaded(object sender, EventArgs args)
    {
        if (this.BindingContext is FirstLookViewModel vm)
        {
            vm.IsOperational = true;
        }
    }

    private void OnUnloaded(object sender, EventArgs args)
    {
        if (this.BindingContext is FirstLookViewModel vm && !this.IsLoaded)
        {
            vm.IsOperational = false;
        }
    }
}