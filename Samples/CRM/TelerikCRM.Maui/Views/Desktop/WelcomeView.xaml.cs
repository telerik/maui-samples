using Telerik.Maui;
using TelerikCRM.Maui.Models;
using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Desktop;

public partial class WelcomeView
{
    private WelcomeViewModel viewModel = new WelcomeViewModel();

    public WelcomeView()
    {
        this.InitializeComponent();
        this.BindingContext = this.viewModel;
        this.collectionView.SelectedIndex = 0;
        this.defaultButton.Clicked += (s, e) => this.DefaultButtonClicked();
    }

    public event EventHandler BoardingCompleted;

    private bool IsCurrentCollectionViewItemLast()
    {
        return this.collectionView.SelectedItem == this.viewModel.WelcomeCards.Last();
    }

    private void WelcomeCardChanged(object sender, RadSelectionChangedEventArgs e)
    {
        this.defaultButton.Content = this.IsCurrentCollectionViewItemLast() ? "Get Started" : "Next";

        if (e.AddedItems.FirstOrDefault() is WelcomeCard card)
        {
            this.modalTitleLabel.Text = card.Title;
            this.image.Source = card.IconSource;
            this.infoLabel.Text = card.Subtitle.Replace("\n", " ");
        }
    }

    private void DefaultButtonClicked()
    {
        if (this.IsCurrentCollectionViewItemLast())
        {
            this.BoardingCompleted?.Invoke(this, new EventArgs());
        }
        else
        {
            this.collectionView.SelectedIndex += 1;
        }
    }

    private void CloseModalButtonClicked(object sender, EventArgs e)
    {
        this.BoardingCompleted?.Invoke(this, new EventArgs());
    }
}