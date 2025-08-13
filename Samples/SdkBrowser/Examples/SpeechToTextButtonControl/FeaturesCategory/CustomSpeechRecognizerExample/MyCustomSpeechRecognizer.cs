using System;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Maui.SpeechRecognizer;

namespace SDKBrowserMaui.Examples.SpeechToTextButtonControl.FeaturesCategory.CustomSpeechRecognizerExample;

// >> speechtotext-mycustomrecognizer
public class MyCustomSpeechRecognizer : IRadSpeechRecognizer
{
	private const string MockText = "This is a mocked speech recognizer response for testing purposes and it will not really do a voice transcription in your system.";
	
	private SpeechRecognizerState state;
	private int reportingSessionId;

	public SpeechRecognizerState State
	{
		get => this.state;
		private set
		{
			if (this.state != value)
			{
				this.state = value;
				this.StateChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public event EventHandler StateChanged;
	public event EventHandler<SpeechRecognizerErrorOccurredEventArgs> ErrorOccurred;
	public event EventHandler<SpeechRecognizerSpeechRecognizedEventArgs> SpeechRecognized;

	public Task Init(SpeechRecognizerInitializationContext context)
	{
		this.State = SpeechRecognizerState.Ready;
		this.reportingSessionId++;
		return Task.CompletedTask;
	}

	public Task StartListening()
	{
		this.State = SpeechRecognizerState.StartingListening;
		this.reportingSessionId++;
		int localSessionId = this.reportingSessionId;

		Task.Run(() =>
		{
			this.State = SpeechRecognizerState.Listening;
			int i = 0;
			string[] words = MockText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			string fullText = string.Empty;

			while (true)
			{
				string word = words[i % words.Length];
				fullText += $" {word}";
				i++;

				Thread.Sleep(333);

				if (localSessionId != this.reportingSessionId)
				{
					break;
				}

				this.SpeechRecognized?.Invoke(this, new SpeechRecognizerSpeechRecognizedEventArgs(fullText));
			}
		});

		return Task.CompletedTask;
	}

	public async Task StopListening()
	{
		this.State = SpeechRecognizerState.Ready;
		this.reportingSessionId++;
		await Task.Yield();
	}

	public Task Reset()
	{
		this.State = SpeechRecognizerState.NotInitialized;
		this.reportingSessionId++;
		return Task.CompletedTask;
	}

	public ValueTask DisposeAsync()
	{
		this.State = SpeechRecognizerState.Disposed;
		this.reportingSessionId++;
		return ValueTask.CompletedTask;
	}
}
// << speechtotext-mycustomrecognizer

