using System.Collections.ObjectModel;
using Microsoft.Maui.Devices;
using QSF.Examples.TreeViewControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;

namespace QSF.Examples.TreeViewControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private Account account;
    private Folder folder;
    private Folder selectedItem;
    private bool areEmailsVisible;

    public FirstLookViewModel()
    {
        this.Account = new Account()
        {
            Name = "Kieran Wood",
            Icon = "person_2.png",
            Address = "kwood@progress.com",
            Folders = DataGenerator.GetItems<ObservableCollection<Folder>>(DataSourcePaths.AccountPath)
        };

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.SelectedItem = this.Account.Folders[0];
        }
    }

    public Account Account
    {
        get => this.account;
        private set => this.UpdateValue(ref this.account, value);
    }

    public Folder Folder
    {
        get => this.folder;
        set => this.UpdateValue(ref this.folder, value);
    }

    public Folder SelectedItem
    {
        get => this.selectedItem;
        set
        {
            this.UpdateValue(ref this.selectedItem, value);

            if (this.selectedItem == null)
            {
                return;
            }

            this.Folder = this.selectedItem;
            this.ShowOrNavigateToEmails();
        }
    }

    public bool AreEmailsVisible
    {
        get => this.areEmailsVisible;
        set => this.UpdateValue(ref this.areEmailsVisible, value);
    }

    private async void ShowOrNavigateToEmails()
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.AreEmailsVisible = this.folder != null && (this.folder.IsLeaf || this.folder.Emails.Count > 0);
        }
        else
        {
            await this.NavigationService.NavigateToAsync<EmailsViewModel>(this.Folder);
            this.SelectedItem = null;
        }
    }
}
