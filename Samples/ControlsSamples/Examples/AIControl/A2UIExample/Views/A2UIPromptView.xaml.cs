using Microsoft.Maui.Controls;

namespace QSF.Examples.AIControl.A2UIExample.Views;

public partial class A2UIPromptView : ContentView
{
    public A2UIPromptView()
    {
        this.InitializeComponent();
    }

    public void SetRenderedContent(View content)
        => this.rendererContainer.Content = content;
}