using System;
using Telerik.Maui.SpeechRecognizer;

namespace SDKBrowserMaui.Common;

public static class SpeechRecognizerProvider
{
    public static Func<IRadSpeechRecognizer> SpeechRecognizerCreator { get; } =
        () => new RadSpeechRecognizer();
}
