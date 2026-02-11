using System;
using Telerik.Maui.SpeechRecognizer;

namespace QSF.ExampleUtilities;

public static class SpeechRecognizerProvider
{
    public static Func<IRadSpeechRecognizer> SpeechRecognizerCreator { get; } =
        () => new RadSpeechRecognizer();
}