using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.AIPromptControl.GettingStartedCategory.GettingStartedExample;

public partial class AIPromptGettingStartedXaml : ContentView
{
    public AIPromptGettingStartedXaml()
    {
      InitializeComponent();

        // >> aiprompt-getting-started-setvm
        this.BindingContext = new ViewModel();
        // << aiprompt-getting-started-setvm

        this.aiPrompt.AutomationId = "aiPrompt";
    }
}