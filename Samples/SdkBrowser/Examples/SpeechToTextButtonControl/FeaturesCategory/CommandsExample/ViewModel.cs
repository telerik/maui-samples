using Microsoft.Maui.Controls;
using System.Windows.Input;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.SpeechToTextButton;

namespace SDKBrowserMaui.Examples.SpeechToTextButtonControl.FeaturesCategory.CommandsExample;

// >> speechtotext-commands-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
	private string text;

	public ViewModel()
	{
		this.SpeechRecognizedCommand = new Command(this.OnSpeechRecognized);
		this.ErrorOccurredCommand = new Command(this.OnErrorOccurred);
	}

	public string Text
	{
		get => this.text;
		set => this.UpdateValue(ref this.text, value);
	}

	public ICommand SpeechRecognizedCommand { get; }

	public ICommand ErrorOccurredCommand { get; }

	private void OnSpeechRecognized(object obj)
	{
		SpeechToTextButtonSpeechRecognizedCommandContext context = (SpeechToTextButtonSpeechRecognizedCommandContext)obj;
		this.Text = context.FullText;
	}

	private void OnErrorOccurred(object obj)
	{
		SpeechToTextButtonErrorOccurredCommandContext context = (SpeechToTextButtonErrorOccurredCommandContext)obj;
		this.Text = $"Error: {context.Message}.\n{context.Exception}";
	}
}
// << speechtotext-commands-viewmodel