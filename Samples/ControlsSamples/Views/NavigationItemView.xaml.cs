using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.Common;
using System.Windows.Input;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class NavigationItemView : ContentView
{
    public static readonly BindableProperty HeaderProperty = BindableProperty.Create(
        nameof(Header), typeof(string), typeof(NavigationItemView));

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon), typeof(string), typeof(NavigationItemView));

    public static readonly BindableProperty StatusProperty = BindableProperty.Create(
        nameof(Status), typeof(StatusType), typeof(NavigationItemView));

    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
        nameof(TapCommand), typeof(ICommand), typeof(NavigationItemView), propertyChanged: (b, o, n) => ((NavigationItemView)b).OnTapCommandChanged());

    private TapGestureRecognizer tap;

    public NavigationItemView()
    {
        this.InitializeComponent();
    }

    public string Header
    {
        get { return (string)this.GetValue(HeaderProperty); }
        set { this.SetValue(HeaderProperty, value); }
    }

    public string Icon
    {
        get { return (string)this.GetValue(IconProperty); }
        set { this.SetValue(IconProperty, value); }
    }

    public StatusType Status
    {
        get { return (StatusType)this.GetValue(StatusProperty); }
        set { this.SetValue(StatusProperty, value); }
    }

    public ICommand TapCommand
    {
        get { return (ICommand)this.GetValue(TapCommandProperty); }
        set { this.SetValue(TapCommandProperty, value); }
    }

    private void OnTapCommandChanged()
    {
        if (this.tap != null)
        {
            this.GestureRecognizers.Remove(this.tap);
            this.tap = null;
        }

        if (this.TapCommand != null)
        {
            this.tap = new TapGestureRecognizer();
            this.tap.Tapped += this.Tap_Tapped;
            this.GestureRecognizers.Add(this.tap);
        }
    }

    private void Tap_Tapped(object sender, System.EventArgs e)
    {
        ICommand command = this.TapCommand;
        object param = this.BindingContext;

        if (command != null && command.CanExecute(param))
        {
            command?.Execute(param);
        }
    }
}