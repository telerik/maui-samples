using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.SpeechRecognizer;

namespace SDKBrowserMaui.Examples.SpeechToTextButtonControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
	public GettingStartedCSharp()
	{
		var stack = new VerticalStackLayout();

#if ANDROID || IOS
		stack.HorizontalOptions = LayoutOptions.Center;
#elif WINDOWS || MACCATALYST
		stack.HorizontalOptions = LayoutOptions.Start;
#endif
		// >> speechtotext-getting-started-csharp
		var speechToTextButton = new RadSpeechToTextButton();
		// << speechtotext-getting-started-csharp

		stack.Children.Add(speechToTextButton);
		this.Content = stack;

#if WINDOWS
		speechToTextButton.SpeechRecognizerCreator = () => new RadSpeechRecognizer();
#endif
	}
}