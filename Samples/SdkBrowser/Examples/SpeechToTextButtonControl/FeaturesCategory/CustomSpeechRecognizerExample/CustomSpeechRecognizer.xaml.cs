using Microsoft.Maui.Controls;
using Telerik.Maui.SpeechRecognizer;

namespace SDKBrowserMaui.Examples.SpeechToTextButtonControl.FeaturesCategory.CustomSpeechRecognizerExample;

public partial class CustomSpeechRecognizer : ContentView
{
	public CustomSpeechRecognizer()
	{
		InitializeComponent();

		// >> speechtotext-speech-recognizer-creator
		this.speechToTextButton.SpeechRecognizerCreator = () => new MyCustomSpeechRecognizer();
		// << speechtotext-speech-recognizer-creator
	}

	private void OnSpeechRecognized(object sender, SpeechRecognizerSpeechRecognizedEventArgs e)
	{
		this.editor.Text = e.FullText;
	}
}