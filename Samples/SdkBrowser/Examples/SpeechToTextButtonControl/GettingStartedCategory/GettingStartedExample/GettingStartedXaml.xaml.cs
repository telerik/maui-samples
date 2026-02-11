using Microsoft.Maui.Controls;
using Telerik.Maui.SpeechRecognizer;

namespace SDKBrowserMaui.Examples.SpeechToTextButtonControl.GettingStartedCategory.GettingStartedExample;

public partial class GettingStartedXaml : ContentView
{
	public GettingStartedXaml()
	{
		InitializeComponent();
		this.speechToTextButton.AutomationId = "speechToTextButton";

#if WINDOWS
		this.speechToTextButton.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
#endif
	}
}