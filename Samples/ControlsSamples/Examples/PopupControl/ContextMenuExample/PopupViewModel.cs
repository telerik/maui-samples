using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System;
using System.Windows.Input;

namespace QSF.Examples.PopupControl.ContextMenuExample
{
    public class PopupViewModel : ViewModelBase
    {
        private ICommand addPeople;
        private ICommand linkShare;
        private ICommand copyLink;
        private ICommand rename;
        private ICommand remove;
        private IToastMessageService toastService;

        public event EventHandler ClosePopup;

        public PopupViewModel()
        {
            this.toastService = DependencyService.Get<IToastMessageService>();

            Command closePopupAndShowToastCommand = new Command(p =>
            {
                toastService.ShortAlert((string)p);
                this.ClosePopup?.Invoke(this, EventArgs.Empty);
            });

            this.AddPeople = closePopupAndShowToastCommand;
            this.LinkShare = closePopupAndShowToastCommand;
            this.CopyLink = closePopupAndShowToastCommand;
            this.Rename = closePopupAndShowToastCommand;
            this.Remove = closePopupAndShowToastCommand;
        }

        public ICommand AddPeople
        {
            get => this.addPeople;
            private set => this.UpdateValue(ref this.addPeople, value);
        }

        public ICommand LinkShare
        {
            get => this.linkShare;
            private set => this.UpdateValue(ref this.linkShare, value);
        }

        public ICommand CopyLink
        {
            get => this.copyLink;
            private set => this.UpdateValue(ref this.copyLink, value);
        }

        public ICommand Rename
        {
            get => this.rename;
            private set => this.UpdateValue(ref this.rename, value);
        }

        public ICommand Remove
        {
            get => this.remove;
            private set => this.UpdateValue(ref this.remove, value);
        }
    }
}
