using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;

namespace QSF.Examples.ComboBoxControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private int selectedSkillIndex = -1;
        private int selectedJobTypeIndex = -1;
        private int selectedTimeIndex = -1;
        private bool isJobSearchNotificationOpen;
        private CancellationTokenSource cancellation;

        public FirstLookViewModel()
        {
            Skills = new ObservableCollection<string>()
            {
                ".NET MAUI",
                "Xamarin",
                "Blazor",
                "WPF",
                "WinUI",
                "Fiddler",
                "Sitefinity",
                "Test Studio",
                "Open Edge",
                "Kinvey",
                "DataRPM",
                "Corticon"
            };

            JobTypes = new ObservableCollection<string>()
            {
                "Full-time",
                "Part-time",
                "Internship",
                "Temporary"
            };

            Times = new ObservableCollection<string>()
            {
                "Posted Any Time",
                "Last Day",
                "Last 3 Days",
                "Last Week",
                "Last 2 Weeks"
            };

            SearchJobButtonCommand = new Command(OnSearchJobButtonCommandExecuted, OnSearchJobButtonCommandCanExecute);

            cancellation = new CancellationTokenSource();
        }

        public ObservableCollection<string> Skills { get; set; }
        public ObservableCollection<string> JobTypes { get; set; }
        public ObservableCollection<string> Times { get; set; }
        public ICommand SearchJobButtonCommand { get; set; }

        public int SelectedSkillIndex
        {
            get
            {
                return selectedSkillIndex;
            }
            set
            {
                if (selectedSkillIndex != value)
                {
                    selectedSkillIndex = value;
                    ((Command)SearchJobButtonCommand).ChangeCanExecute();
                    OnPropertyChanged();
                }
            }
        }

        public int SelectedJobTypeIndex
        {
            get
            {
                return selectedJobTypeIndex;
            }
            set
            {
                if (selectedJobTypeIndex != value)
                {
                    selectedJobTypeIndex = value;
                    ((Command)SearchJobButtonCommand).ChangeCanExecute();
                    OnPropertyChanged();
                }
            }
        }

        public int SelectedTimeIndex
        {
            get
            {
                return selectedTimeIndex;
            }
            set
            {
                if (selectedTimeIndex != value)
                {
                    selectedTimeIndex = value;
                    ((Command)SearchJobButtonCommand).ChangeCanExecute();
                    OnPropertyChanged();
                }
            }
        }

        public bool IsJobSearchNotificationOpen
        {
            get
            {
                return isJobSearchNotificationOpen;
            }
            set
            {
                if (isJobSearchNotificationOpen != value)
                {
                    isJobSearchNotificationOpen = value;
                    if (!isJobSearchNotificationOpen)
                    {
                        Interlocked.Exchange(ref cancellation, new CancellationTokenSource()).Cancel();
                    }

                    OnPropertyChanged();
                }
            }
        }

        private void OnSearchJobButtonCommandExecuted(object obj)
        {
            if (isJobSearchNotificationOpen)
            {
                return;
            }

            CancellationTokenSource cts = cancellation;
            IsJobSearchNotificationOpen = true;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (cts.IsCancellationRequested)
                {
                    return false;
                }

                IsJobSearchNotificationOpen = false;
                return false;
            });
        }

        private bool OnSearchJobButtonCommandCanExecute(object arg)
        {
            return selectedSkillIndex != -1
                && selectedJobTypeIndex != -1
                && selectedTimeIndex != -1;
        }
    }
}
