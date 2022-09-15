using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace QSF.Examples.AutoCompleteControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private string description;
        private List<string> people;
        private bool isSendNotificationOpen;
        private CancellationTokenSource sendCancellation;
        private int recepientCount = 0;

        public FirstLookViewModel()
        {
            this.description = "Kickstart your cross-platform application development and modernize legacy projects with Telerik’s best-in-class UI suite for .NET MAUI! Code once and build native applications for Windows, macOS, Android and iOS.\n\nTelerik UI for .NET MAUI offers a wide range of 50+ controls to enable your cross-platform development of native Windows, macOS, Android and iOS applications. Plus, our regular releases ensure your .NET MAUI library will keep growing.";
            this.people = new List<string>()
            {
                "Joshua Price",
                "Reuben Holmes",
                "Eva Lawson",
                "Rory Baxter",
                "David Webb",
                "Howard Pittman",
                "Davis Anderson",
                "Cannon Puckett",
                "Xavi Vasquez",
                "Ronald Hatfield",
                "Freda Curtis",
                "Jeffery Francis",
                "Eva Lawson",
                "Emmett Santos",
                "Vada Davies",
                "Jenny Fuller",
                "Terrell Norris",
                "Vadim Saunders",
                "Nida Carty",
                "Niki Samaniego",
                "Valdex Mills",
                "Layton Buck",
                "Alex Ramos",
                "Alena Cline",
                "Joel Walsh",
                "Vadik Pearson",
                "Bob Carty",
                "Carol Samaniego",
                "Greg Nikolas",
                "Ivan Ivanov",
                "Konny Mills",
                "Matias Santos",
                "Peter Bence",
                "Quincy Sanchez",
            };
            this.sendCancellation = new CancellationTokenSource();
            this.SendCommand = new Command(this.OnSendExecuted, this.OnSendCanExecute);
        }

        public ICommand SendCommand { get; set; }

        public string Description
        {
            get { return this.description; }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int RecepientCount
        {
            get
            {
                return this.recepientCount;
            }
            set
            {
                if (this.recepientCount != value)
                {
                    this.recepientCount = value;
                    ((Command)this.SendCommand).ChangeCanExecute();
                    this.OnPropertyChanged();
                }
            }
        }

        public List<string> People
        {
            get => this.people;
        }

        public bool IsSendNotificationOpen
        {
            get
            {
                return this.isSendNotificationOpen;
            }
            set
            {
                if (this.isSendNotificationOpen != value)
                {
                    this.isSendNotificationOpen = value;
                    if (!this.IsSendNotificationOpen)
                    {
                        Interlocked.Exchange(ref this.sendCancellation, new CancellationTokenSource()).Cancel();
                    }

                    OnPropertyChanged();
                }
            }
        }

        private bool OnSendCanExecute(object arg)
        {
            return this.recepientCount > 0;
        }

        private void OnSendExecuted(object obj)
        {
            if (this.isSendNotificationOpen)
            {
                return;
            }

            CancellationTokenSource cts = this.sendCancellation;
            this.IsSendNotificationOpen = true;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (cts.IsCancellationRequested)
                {
                    return false;
                }

                this.IsSendNotificationOpen = false;
                return false;
            });
        }
    }
}
