using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class WelcomeView
{
    public WelcomeView()
    {
        this.InitializeComponent();
        this.BindingContext = new WelcomeViewModel();
        this.defaultButton.Clicked += (s, e) => this.DefaultButtonClicked();
    }

    public event EventHandler BoardingCompleted;

    private bool IsCurrentSlideViewItemLast()
        => this.slideView.CurrentIndex == this.slideView.Items.IndexOf(this.slideView.Items.Last());

    private void SlideViewItemChanged(object sender, Telerik.Maui.Controls.SlideView.CurrentItemChangedEventArgs e)
        => this.defaultButton.Content = this.IsCurrentSlideViewItemLast() ? "Get Started" : "Next";

    private void GetStarted()
        => this.BoardingCompleted?.Invoke(this, EventArgs.Empty);

    private void DefaultButtonClicked()
    {
        if (this.IsCurrentSlideViewItemLast())
        {
            this.GetStarted();
        }
        else
        {
            this.slideView.CurrentIndex += 1;
        }
    }

    private void SkipButtonClicked(object sender, EventArgs e) => this.GetStarted();
}