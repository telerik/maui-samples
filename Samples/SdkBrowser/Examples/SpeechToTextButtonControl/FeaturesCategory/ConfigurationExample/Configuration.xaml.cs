using Microsoft.Maui.Controls;
using Telerik.Maui.SpeechRecognizer;

namespace SDKBrowserMaui.Examples.SpeechToTextButtonControl.FeaturesCategory.ConfigurationExample;

public partial class Configuration : ContentView
{
	public Configuration()
	{
		InitializeComponent();

#if WINDOWS
		this.speechContinuousButton.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
		this.speechToTextButton.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
#endif
	}

	private void OnSpeechRecognized(object sender, Telerik.Maui.SpeechRecognizer.SpeechRecognizerSpeechRecognizedEventArgs e)
	{
		this.editor.Text = e.FullText;
	}

	private void OnLanguageSpeechRecognized(object sender, Telerik.Maui.SpeechRecognizer.SpeechRecognizerSpeechRecognizedEventArgs e)
	{
		this.languageEditor.Text = e.FullText;
	}
}