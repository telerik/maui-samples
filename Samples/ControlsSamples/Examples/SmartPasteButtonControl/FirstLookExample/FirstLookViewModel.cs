using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.AI.SmartComponents.Extensions;
using Telerik.Maui.Controls.SmartPasteButton;

namespace QSF.Examples.SmartPasteButtonControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private string fullName;
    private string city;
    private string phoneNumber;
    private string letterDescription;
    private static readonly HttpClient HttpClient = new HttpClient();
    private string copyText = "Copy Text";
    private bool isLoading = false;

    public FirstLookViewModel()
    {
        this.LetterDescription = "Ashley Johnson is a UX Designer with 8 years of experience in Portland, Oregon. She's reliable, and great at making complex ideas simple. Her approach ensures smooth teamwork and great results. Reach her at (555) 248-9173.";

        this.CopyTextCommand = new Command(this.OnCopyText);
        this.ResetCommand = new Command(this.OnReset);
        this.SmartPasteCommand = new Command<object>(async obj => await this.OnSmartPasteRequestAsync(obj));
    }

    [Display(Name = "Full Name")]
    public string FullName
    {
        get => this.fullName;
        set => this.UpdateValue(ref this.fullName, value);
    }

    [Display(Name = "City")]
    public string City
    {
        get => this.city;
        set => this.UpdateValue(ref this.city, value);
    }

    [Display(Name = "Phone Number")]
    public string PhoneNumber
    {
        get => this.phoneNumber;
        set => this.UpdateValue(ref this.phoneNumber, value);
    }

    public string LetterDescription
    {
        get => this.letterDescription;
        set => this.UpdateValue(ref this.letterDescription, value);
    }

    public string CopyText
    {
        get => this.copyText;
        set => this.UpdateValue(ref this.copyText, value);
    }

    public bool IsLoading
    {
        get { return this.isLoading; }
        set { this.UpdateValue(ref this.isLoading, value); }
    }

    public ICommand CopyTextCommand { get; }

    public ICommand ResetCommand { get; }

    public ICommand SmartPasteCommand { get; }

    private async void OnCopyText()
    {
        if (!string.IsNullOrEmpty(this.LetterDescription))
        {
            await Clipboard.SetTextAsync(this.LetterDescription);
            this.CopyText = "Text Copied";
        }
    }

    private void OnReset()
    {
        this.FullName = string.Empty;
        this.City = string.Empty;
        this.PhoneNumber = string.Empty;
    }

    private async Task OnSmartPasteRequestAsync(object obj)
    {
        this.IsLoading = true;
        this.CopyText = "Copy Text";
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
        finally
        {
            this.IsLoading = false;
        }
    }
}
