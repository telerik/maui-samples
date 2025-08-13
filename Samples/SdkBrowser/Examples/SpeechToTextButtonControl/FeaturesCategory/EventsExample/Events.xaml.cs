using Microsoft.Maui.Controls;
using Telerik.Maui.SpeechRecognizer;

namespace SDKBrowserMaui.Examples.SpeechToTextButtonControl.FeaturesCategory.EventsExample;

public partial class Events : ContentView
{
	public Events()
	{
		InitializeComponent();

#if WINDOWS
		this.speechToTextButton.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
#endif
	}

	// >> speechtotext-events-speech-recognized
	private void OnSpeechRecognized(object sender, Telerik.Maui.SpeechRecognizer.SpeechRecognizerSpeechRecognizedEventArgs e)
	{
		this.editor.Text = e.FullText;
	}
	// << speechtotext-events-speech-recognized

	// >> speechtotext-events-error-occured
	private void OnErrorOccurred(object sender, Telerik.Maui.SpeechRecognizer.SpeechRecognizerErrorOccurredEventArgs e)
	{
		e.Handled = true;
		var error = $"{e.Message}; {e.Exception}";
		Application.Current.Windows[0].Page.DisplayAlert("Error", error, "OK");
	}
	// << speechtotext-events-error-occured
}