using Microsoft.Maui.Controls;
using System.Collections.Generic;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.ChatControl.AIIntegrationExample;

public partial class AIIntegrationView : RadContentView
{
    private SuggestedActionsItem suggestedActionsItem;

    public AIIntegrationView()
    {
        InitializeComponent();
        this.Unloaded += this.OnUnloaded;

        this.Dispatcher.Dispatch(this.AddSuggestedActions);
    }

    private void AddSuggestedActions()
    {
        if (this.suggestedActionsItem != null)
        {
            return;
        }

        if (this.BindingContext is not AIIntegrationViewModel vm || vm.QuickActions.Count == 0)
        {
            return;
        }

        var item = new SuggestedActionsItem();
        var actions = new List<SuggestedAction>();

        foreach (var quickAction in vm.QuickActions)
        {
            actions.Add(new SuggestedAction
            {
                Text = quickAction,
                Command = new Command(() =>
                {
                    vm.QuickActionCommand.Execute(quickAction);

                    this.Dispatcher.Dispatch(() =>
                    {
                        vm.Messages.Remove(item);
                        this.suggestedActionsItem = null;
                    });
                })
            });
        }

        item.Actions = actions;

        vm.Messages.Add(item);
        this.suggestedActionsItem = item;
    }

    private void OnUnloaded(object sender, EventArgs args)
    {
        if (!this.IsLoaded && this.BindingContext is AIIntegrationViewModel vm)
        {
            try { vm.DisconnectAI(); } catch { }
        }
    }
}