using Microsoft.Maui.Controls;
using Telerik.Maui.SpeechRecognizer;

namespace SDKBrowserMaui.Examples.SpeechToTextButtonControl.StylingCategory.StylingExample;

public partial class Styling : ContentView
{
	public Styling()
	{
		InitializeComponent();

#if WINDOWS
        this.speechToTextButton.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
#endif
	}

	private void OnSpeechRecognized(object sender, Telerik.Maui.SpeechRecognizer.SpeechRecognizerSpeechRecognizedEventArgs e)
	{
		this.editor.Text = e.FullText;
	}

	private void OnErrorOccurred(object sender, Telerik.Maui.SpeechRecognizer.SpeechRecognizerErrorOccurredEventArgs e)
	{
		e.Handled = true;
		var error = $"{e.Message}; {e.Exception}";
		Application.Current.Windows[0].Page.DisplayAlert("Error", error, "OK");
	}
}