using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using Telerik.AI.SmartComponents.Extensions;
using Telerik.Maui.Controls.SmartPasteButton;

namespace SDKBrowserMaui.Examples.SmartPasteButtonControl.TemplateCategory.TemplateExample;

public partial class Template : ContentView
{
    public Template()
    {
        this.InitializeComponent();
    }

    // >> smartpaste-template-paste-request
    private async void OnSmartPasteRequest(object sender, SmartPasteButtonRequestContext e)
    {
        try
        {
            var request = new { Content = e.Content, FormFields = e.Fields };
            var httpResponse = await new HttpClient().PostAsJsonAsync(
                "https://demos.telerik.com/service/v2/ai/smartpaste/smartpaste",
                request,
                e.CancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadFromJsonAsync<SmartPasteResponse>(e.CancellationToken);
            e.SetResponse(response.FieldValues);
        }
        catch (OperationCanceledException)
        {
            e.Cancel();
        }
        catch (Exception ex)
        {
            e.SetError(ex);
        }
    }
    // << smartpaste-template-paste-request

    // >> smartpaste-template-copy
    private async void OnCopyFromClipboardClicked(object sender, System.EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.label.Text))
        {
            await Clipboard.SetTextAsync(this.label.Text);
        }
    }
    // << smartpaste-template-copy
}