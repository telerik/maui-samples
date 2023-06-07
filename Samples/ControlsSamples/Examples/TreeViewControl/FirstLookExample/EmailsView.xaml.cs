using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;

namespace QSF.Examples.TreeViewControl.FirstLookExample;

public partial class EmailsView : RadContentView
{
    public static readonly BindableProperty AreEmailsVisibleProperty =
        BindableProperty.Create(nameof(AreEmailsVisible), typeof(bool), typeof(EmailsView), false,
            propertyChanged: (b, o, n) => ((EmailsView)b).OnAreEmailsVisiblePropertyChanged());

    public EmailsView()
    {
        this.InitializeComponent();
        this.UpdateElementsVisibility();
    }

    public bool AreEmailsVisible
    {
        get => (bool)this.GetValue(AreEmailsVisibleProperty);
        set => this.SetValue(AreEmailsVisibleProperty, value);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            return;
        }

        var context = this.BindingContext as EmailsViewModel;
        if (context != null)
        {
            var folder = context.Folder;
            this.AreEmailsVisible = folder != null && (folder.IsLeaf || folder.Emails.Count > 0);
        }
    }

    private void UpdateElementsVisibility()
    {
        this.emailsList.IsVisible = this.AreEmailsVisible;
        this.infoPanel.IsVisible = !this.AreEmailsVisible;
    }

    private void OnAreEmailsVisiblePropertyChanged()
    {
        this.UpdateElementsVisibility();
    }
}
