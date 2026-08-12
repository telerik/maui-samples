using QSF.Examples.AIControl.A2UIExample.Views;
using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace QSF.Examples.AIControl.A2UIExample;

public partial class A2UIView : RadContentView
{
    private readonly Dictionary<int, A2UITelerikRenderer> cachedRenderers = new Dictionary<int, A2UITelerikRenderer>();

    public A2UIView()
    {
        this.InitializeComponent();
        this.Loaded += this.OnLoaded;
        this.Unloaded += this.OnUnloaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        if (this.BindingContext is A2UIViewModel viewModel)
        {
            viewModel.MessagesChanged += this.OnMessagesChanged;
        }
    }

    private void OnUnloaded(object sender, EventArgs e)
    {
        if (this.BindingContext is A2UIViewModel viewModel)
        {
            viewModel.Dispose();
            viewModel.MessagesChanged -= this.OnMessagesChanged;
        }
    }

    private void OnMessagesChanged()
    {
        if (this.Dispatcher.IsDispatchRequired)
        {
            this.Dispatcher.Dispatch(() => this.RenderCurrentMessages());
        }
        else
        {
            this.RenderCurrentMessages();
        }
    }

    private void RenderCurrentMessages()
    {
        var viewModel = ((A2UIViewModel)this.BindingContext);
        var messages = viewModel.CurrentMessages.messages;
        var id = viewModel.CurrentMessages.id;

        if (messages.Count > 0)
        {
            if (this.cachedRenderers.TryGetValue(id, out var cachedRenderer))
            {
                this.promptView.SetRenderedContent(cachedRenderer);
                return;
            }

            var renderer = new A2UITelerikRenderer();
            renderer.SetMessages(messages, (v) => viewModel.Submit(v));
            this.cachedRenderers[id] = renderer;
            this.promptView.SetRenderedContent(renderer);
        }
        else
        {
            if (viewModel.WizardStepIndex == -1)
            {
                this.cachedRenderers.Clear();
            }
            else
            {
                this.cachedRenderers.Remove(id);
            }

            this.promptView.SetRenderedContent(null);
        }
    }
}