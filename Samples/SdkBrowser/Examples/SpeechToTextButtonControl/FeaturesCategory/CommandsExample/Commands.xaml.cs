using Microsoft.Maui.Controls;
using Telerik.Maui.SpeechRecognizer;

namespace SDKBrowserMaui.Examples.SpeechToTextButtonControl.FeaturesCategory.CommandsExample;

public partial class Commands : ContentView
{
	public Commands()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();

#if WINDOWS
		this.speechToTextButton.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
#endif
	}
}