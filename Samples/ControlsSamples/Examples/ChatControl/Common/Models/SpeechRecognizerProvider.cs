using System;
using Telerik.Maui.SpeechRecognizer;

namespace QSF.Examples.ChatControl;

public static class SpeechRecognizerProvider
{
    public static Func<IRadSpeechRecognizer> SpeechRecognizerCreator { get; } =
        () => new RadSpeechRecognizer();
}