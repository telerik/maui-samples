using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Networking;
using QSF.Services;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.SpeechRecognizer;

namespace QSF.Examples.SpeechToTextButtonControl.ThemingExample;

public partial class ThemingView : RadContentView
{
    private static readonly string OfflineError = "Oops, you're offline. Turn on mobile data.";
    private static readonly string ConnectivityErrorMessage = "Connectivity Error";

    public ThemingView()
    {
        InitializeComponent();

        this.UpdateSpeechButtonsState();
        Connectivity.Current.ConnectivityChanged += this.OnConnectivityChanged;

#if WINDOWS
        this.speechToTextButton.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
#endif
    }

    private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs args)
       => this.UpdateSpeechButtonsState();

    private async void UpdateSpeechButtonsState()
    {
        var networkAccess = Connectivity.Current.NetworkAccess;
        bool isOffline = networkAccess == NetworkAccess.None || networkAccess == NetworkAccess.Unknown;

        this.speechToTextButton.IsEnabled = !isOffline;

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

    private void OnSpeechToTextCompleted(object sender, SpeechRecognizerSpeechRecognizedEventArgs args)
        => this.entry.Text = args.FullText;

    private void OnSpeechRecognitionError(object sender, SpeechRecognizerErrorOccurredEventArgs args)
    {
        args.Handled = true;

        var toastService = DependencyService.Get<IToastMessageService>();
        toastService?.ShortAlert(args.Message);
    }
}