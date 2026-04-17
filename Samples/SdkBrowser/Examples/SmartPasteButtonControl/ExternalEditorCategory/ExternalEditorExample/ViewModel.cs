using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.AI.SmartComponents.Extensions;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.SmartPasteButton;

namespace SDKBrowserMaui.Examples.SmartPasteButtonControl.ExternalEditorCategory.ExternalEditorExample;

// >> smartpaste-viewmodel-external
public class ViewModel : NotifyPropertyChangedBase, ISmartPasteButtonProvider
{
    private static readonly HttpClient HttpClient = new HttpClient();
    private string name;
    private string city;
    private string description;
    private string email;
    private string copyText = "Jane Smith is a Senior Back-End Engineer based in downtown Austin, Texas, USA. She designs scalable REST APIs, manages cloud infrastructure, mentors junior developers, and drives performance improvements across distributed systems. Her email is jane.smith@techcorp.io.";

    public ViewModel()
    {
        this.SmartPasteRequestCommand = new Command<object>(async obj => await this.OnSmartPasteRequestAsync(obj));
        this.CopyToClipboardCommand = new Command(async () => await this.OnCopyToClipboard());
    }

    public string Name
    {
        get => this.name;
        set => this.UpdateValue(ref this.name, value);
    }

    public string City
    {
        get => this.city;
        set => this.UpdateValue(ref this.city, value);
    }

    public string Description
    {
        get => this.description;
        set => this.UpdateValue(ref this.description, value);
    }

    public string Email
    {
        get => this.email;
        set => this.UpdateValue(ref this.email, value);
    }

    public string CopyText
    {
        get => this.copyText;
        set => this.UpdateValue(ref this.copyText, value);
    }

    public ICommand SmartPasteRequestCommand { get; }

    public ICommand CopyToClipboardCommand { get; }

    public IEnumerable<SmartPasteButtonField> GetFields()
    {
        yield return new SmartPasteButtonField { Field = nameof(this.Name), Description = "Full Name" };
        yield return new SmartPasteButtonField { Field = nameof(this.City), Description = "City" };
        yield return new SmartPasteButtonField { Field = nameof(this.Description), Description = "Description for job position, email address and daily work" };
        yield return new SmartPasteButtonField { Field = nameof(this.Email), Description = "Email address" };
    }

    public void SetFieldValue(string field, object value)
    {
        switch (field)
        {
            case nameof(this.Name):
                this.Name = (string)value;
                break;
            case nameof(this.City):
                this.City = (string)value;
                break;
            case nameof(this.Description):
                this.Description = (string)value;
                break;
            case nameof(this.Email):
                this.Email = (string)value;
                break;
        }
    }

    private async Task OnSmartPasteRequestAsync(object obj)
    {
        var context = (SmartPasteButtonRequestContext)obj;

        try
        {
            var request = new { Content = context.Content, FormFields = context.Fields };
            var httpResponse = await HttpClient.PostAsJsonAsync(
                "https://demos.telerik.com/service/v2/ai/smartpaste/smartpaste",
                request,
                context.CancellationToken);
            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadFromJsonAsync<SmartPasteResponse>(context.CancellationToken);
            context.SetResponse(response.FieldValues);
        }
        catch (OperationCanceledException)
        {
            context.Cancel();
        }
        catch (Exception ex)
        {
            context.SetError(ex);
        }
    }

    private async Task OnCopyToClipboard()
    {
        await Clipboard.SetTextAsync(this.CopyText);
    }
}
// << smartpaste-viewmodel-external