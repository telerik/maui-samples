using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.SpeechRecognizer;

namespace QSF.Examples.ChatControl.AIIntegrationExample;

public partial class AIIntegrationView : RadContentView
{
    public AIIntegrationView()
    {
        InitializeComponent();

#if WINDOWS
        Style sttbStyle = new Style(typeof(RadSpeechToTextButton));
        sttbStyle.Setters.Add(new Setter { Property = RadSpeechToTextButton.SpeechRecognizerCreatorProperty, Value = new Func<RadSpeechRecognizer>(() => new RadSpeechRecognizer()), });
        this.chat.SpeechToTextButtonStyle = sttbStyle;
#endif

        this.Unloaded += this.OnUnloaded;
    }

    private void OnUnloaded(object sender, EventArgs args)
    {
        if (!this.IsLoaded)
        {
            AIIntegrationViewModel vm = this.BindingContext as AIIntegrationViewModel;

            if (vm != null)
            {
                try
                {
                    vm.DisconnectAI();
                }
                catch { }
            }
        }
    }
}