using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Networking;
using QSF.Services;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Maui.SpeechRecognizer;

namespace QSF.Examples.SpeechToTextButtonControl.FirstLookExample;

public partial class FirstLookView : ContentView
{
    private static readonly string OfflineError = "Oops, you're offline. Turn on mobile data.";
    private static readonly string EventCreatedMessage = "Your event has been created.";
    private static readonly string ConnectivityErrorMessage = "Connectivity Error";

    public FirstLookView()
    {
        this.InitializeComponent();

        this.UpdateSpeechButtonsState();
        Connectivity.Current.ConnectivityChanged += this.OnConnectivityChanged;

#if WINDOWS
        this.speechToTextButtonNewEvent.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
        this.speechToTextButtonLocation.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
        this.speechToTextButtonDescription.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
#endif
    }

    private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        => this.UpdateSpeechButtonsState();

    private async void UpdateSpeechButtonsState()
    {
        var networkAccess = Connectivity.Current.NetworkAccess;
        bool isOffline = networkAccess == NetworkAccess.None || networkAccess == NetworkAccess.Unknown;

        this.speechToTextButtonNewEvent.IsEnabled = !isOffline;
        this.speechToTextButtonLocation.IsEnabled = !isOffline;
        this.speechToTextButtonDescription.IsEnabled = !isOffline;

        if (isOffline)
        {
            if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
            {
                var toastService = DependencyService.Get<IToastMessageService>();
                toastService?.ShortAlert(OfflineError);
            }
            else
            {
                await Application.Current.Windows
                    .FirstOrDefault()?
                    .Page
                    .DisplayAlert(ConnectivityErrorMessage, OfflineError, "OK");
            }
        }
    }

    private void OnSpeechToTextNewEventCompleted(object sender, SpeechRecognizerSpeechRecognizedEventArgs args)
        => this.eventNameEntry.Text = args.FullText;

    private void OnSpeechToTextLocationCompleted(object sender, SpeechRecognizerSpeechRecognizedEventArgs args)
        => this.locationEntry.Text = args.FullText;

    private void OnSpeechToTextDescriptionCompleted(object sender, SpeechRecognizerSpeechRecognizedEventArgs args)
        => this.descriptionEditor.Text = args.FullText;

    private void OnSpeechRecognitionError(object sender, SpeechRecognizerErrorOccurredEventArgs args)
    {
        args.Handled = true;

        string message = args.Message;
        var toastService = DependencyService.Get<IToastMessageService>();
        toastService?.ShortAlert(message);
    }

    private void OnFormFieldChanged(object sender, EventArgs e)
    {
        bool isFormValid = !string.IsNullOrWhiteSpace(this.eventNameEntry.Text)
            && !string.IsNullOrWhiteSpace(this.locationEntry.Text)
            && !string.IsNullOrWhiteSpace(this.descriptionEditor.Text)
            && this.datePicker.Date != default
            && this.timePicker.Time != default;

        this.addEventButton.IsEnabled = isFormValid;
    }

    private void OnAddEventButtonClicked(object sender, EventArgs e)
    {
        var toastService = DependencyService.Get<IToastMessageService>();
        toastService?.ShortAlert(EventCreatedMessage);

        this.eventNameEntry.Text = string.Empty;
        this.locationEntry.Text = string.Empty;
        this.datePicker.Date = default;
        this.timePicker.Time = default;
        this.descriptionEditor.Text = string.Empty;

        this.addEventButton.IsEnabled = false;
    }
}