using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;

namespace QSF.Examples.CheckBoxControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private bool? receiveEmailsChecked = false;
        private bool someoneFollowsChecked = false;
        private bool someoneSendsMessagesChecked = false;
        private bool? privateProfileChecked = false;
        private bool onlyFriendsChecked = false;
        private bool onlyShowToFollowersChecked = false;
        private bool propagate;

        public FirstLookViewModel()
        {
            this.SaveSettingsCommand = new Command(this.SaveRequest);
        }

        public Command SaveSettingsCommand { get; }

        public bool? ReceiveEmailsChecked
        {
            get
            {
                return this.receiveEmailsChecked;
            }
            set
            {
                if (this.receiveEmailsChecked != value)
                {
                    this.receiveEmailsChecked = value;
                    if (this.receiveEmailsChecked != null)
                    {
                        this.propagate = true;
                        this.SomeoneFollowsChecked = this.receiveEmailsChecked.Value;
                        this.SomeoneSendsMessagesChecked = this.receiveEmailsChecked.Value;
                        this.propagate = false;
                    }
                    this.OnPropertyChanged();
                }
            }
        }

        public bool SomeoneFollowsChecked
        {
            get
            {
                return this.someoneFollowsChecked;
            }
            set
            {
                if (this.someoneFollowsChecked != value)
                {
                    this.someoneFollowsChecked = value;
                    if (!this.propagate)
                    {
                        this.UpdateReceiveEmailsChecked();
                    }
                    this.OnPropertyChanged();
                }
            }
        }

        public bool SomeoneSendsMessagesChecked
        {
            get
            {
                return this.someoneSendsMessagesChecked;
            }
            set
            {
                if (this.someoneSendsMessagesChecked != value)
                {
                    this.someoneSendsMessagesChecked = value;
                    if (!this.propagate)
                    {
                        this.UpdateReceiveEmailsChecked();
                    }
                    this.OnPropertyChanged();
                }
            }
        }

        public bool? PrivateProfileChecked
        {
            get
            {
                return this.privateProfileChecked;
            }
            set
            {
                if (this.privateProfileChecked != value)
                {
                    this.privateProfileChecked = value;
                    if (this.privateProfileChecked != null)
                    {
                        this.propagate = true;
                        this.OnlyFriendsChecked = this.privateProfileChecked.Value;
                        this.OnlyShowToFollowersChecked = this.privateProfileChecked.Value;
                        this.propagate = false;
                    }
                    this.OnPropertyChanged();
                }
            }
        }

        public bool OnlyFriendsChecked
        {
            get
            {
                return this.onlyFriendsChecked;
            }
            set
            {
                if (this.onlyFriendsChecked != value)
                {
                    this.onlyFriendsChecked = value;
                    if (!this.propagate)
                    {
                        this.UpdatePrivateProfileChecked();
                    }
                    this.OnPropertyChanged();
                }
            }
        }

        public bool OnlyShowToFollowersChecked
        {
            get
            {
                return this.onlyShowToFollowersChecked;
            }
            set
            {
                if (this.onlyShowToFollowersChecked != value)
                {
                    this.onlyShowToFollowersChecked = value;
                    if (!this.propagate)
                    {
                        this.UpdatePrivateProfileChecked();
                    }
                    this.OnPropertyChanged();
                }
            }
        }

        private void UpdateReceiveEmailsChecked()
        {
            if (this.someoneFollowsChecked != this.someoneSendsMessagesChecked)
            {
                this.ReceiveEmailsChecked = null;
            }
            else
            {
                this.ReceiveEmailsChecked = this.someoneFollowsChecked;
            }
        }

        private void UpdatePrivateProfileChecked()
        {
            if (this.onlyFriendsChecked != this.onlyShowToFollowersChecked)
            {
                this.PrivateProfileChecked = null;
            }
            else
            {
                this.PrivateProfileChecked = this.onlyFriendsChecked;
            }
        }

        private void SaveRequest()
        {
            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert("Your profile has been updated");
        }
    }
}
