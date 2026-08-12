using Microsoft.Maui.Controls;
using QSF.Examples.AIControl.A2UIExample.Models;
using QSF.Examples.AIControl.A2UIExample.Services;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QSF.Examples.AIControl.A2UIExample;

#nullable enable

public class A2UIViewModel : ExampleViewModel, IDisposable
{
    private static string DashboardPromptValue = "I want to get a flight from London to New York, give me a plan as a dashboard with important data about the trip";
    private static string FormPromptValue = "I want to fill a form for booking a flight from London to New York";
    private static string FlightPlanPlaceholder = "Describe your flight plan...";
    private static string BookingFormPlaceholder = "Describe your booking details...";

    private readonly A2UIService a2uiService = new A2UIService(new A2UILlmClient());
    private (int id, List<A2UIMessage> messages) dashboardMessages = (-1, new List<A2UIMessage>());
    private (int id, List<A2UIMessage> messages) formMessages = (-1, new List<A2UIMessage>());
    private CancellationTokenSource? cts;
    private bool isDisposed;
    private bool wizardCompleted;
    private bool isAgentRunning;
    private string prompt = DashboardPromptValue;
    private string placeholder = FlightPlanPlaceholder;
    private string? error;
    private string statusText = string.Empty;
    private string dashboardStatusText = string.Empty;
    private string formStatusText = string.Empty;
    private int wizardStepIndex;
    private ICommand? dashboardCardCommand;
    private ICommand? formCardCommand;
    private ICommand? resetCommand;
    private ICommand? changeStepCommand;
    private ICommand? finishCommand;
    private ICommand? sendCommand;
    private Dictionary<string, object>? formSubmitResult;

    public A2UIViewModel()
    {
        this.WizardStepIndex = -1;
    }

    public event Action? MessagesChanged;

    public bool WizardCompleted
    {
        get => this.wizardCompleted;
        set
        {
            if (this.UpdateValue(ref this.wizardCompleted, value))
            {
                this.OnPropertyChanged(nameof(this.IsLanding));
                this.OnPropertyChanged(nameof(this.IsActive));
                this.OnPropertyChanged(nameof(this.IsCompleted));
            }
        }
    }

    public bool IsAgentRunning
    {
        get => this.isAgentRunning;
        set
        {
            if (this.UpdateValue(ref this.isAgentRunning, value))
            {
                ((Command)this.SendCommand).ChangeCanExecute();
            }
        }
    }

    public bool IsLanding => this.wizardStepIndex == -1 && !this.wizardCompleted;

    public bool IsActive => this.wizardStepIndex != -1 && !this.wizardCompleted;

    public bool IsCompleted => this.wizardStepIndex != -1 && this.wizardCompleted;

    public bool IsConfirmationVisible => this.wizardStepIndex == 1 && this.formSubmitResult != null;

    public string Prompt
    {
        get => this.prompt;
        set
        {
            if (this.UpdateValue(ref this.prompt, value))
            {
                ((Command)this.SendCommand).ChangeCanExecute();
            }
        }
    }

    public string Placeholder
    {
        get => this.placeholder;
        set => this.UpdateValue(ref this.placeholder, value);
    }

    public string? Error
    {
        get => this.error;
        set => this.UpdateValue(ref this.error, value);
    }

    public string StatusText
    {
        get => this.wizardStepIndex == 0 ? this.dashboardStatusText : this.formStatusText;
        private set
        {
            if (this.wizardStepIndex == 0)
            {
                this.UpdateValue(ref this.dashboardStatusText, value);
            }
            else
            {
                this.UpdateValue(ref this.formStatusText, value);
            }
        }
    }

    public int WizardStepIndex
    {
        get => this.wizardStepIndex;
        set
        {
            if (this.UpdateValue(ref this.wizardStepIndex, value))
            {
                if (this.wizardStepIndex != -1)
                {
                    this.Prompt = this.wizardStepIndex == 0 ? DashboardPromptValue : FormPromptValue;
                    this.Placeholder = this.wizardStepIndex == 0 ? FlightPlanPlaceholder : BookingFormPlaceholder;
                }

                this.OnPropertyChanged(nameof(this.IsLanding));
                this.OnPropertyChanged(nameof(this.IsActive));
                this.OnPropertyChanged(nameof(this.IsCompleted));
                this.OnPropertyChanged(nameof(this.IsConfirmationVisible));
                this.OnPropertyChanged(nameof(this.CurrentMessages));
                this.OnPropertyChanged(nameof(this.StatusText));
            }
        }
    }

    public ICommand DashboardCardCommand => this.dashboardCardCommand ??= new Command(() => this.ChangeStepCommandExecute(0));

    public ICommand FormCardCommand => this.formCardCommand ??= new Command(() => this.ChangeStepCommandExecute(1));

    public ICommand ResetCommand => this.resetCommand ??= new Command(this.ResetCommandExecute);

    public ICommand ChangeStepCommand => this.changeStepCommand ??= new Command((step) => this.ChangeStepCommandExecute(int.TryParse((string)step, out var s) ? s : -1));

    public ICommand FinishCommand => this.finishCommand ??= new Command(this.FinishCommandExecute);

    public ICommand SendCommand => this.sendCommand ??= new Command(async () => await this.RunAgentAsync(), () => !this.IsAgentRunning && !string.IsNullOrEmpty(this.Prompt));

    public Dictionary<string, object>? FormSubmitResult
    {
        get => this.formSubmitResult;
        set
        {
            if (this.UpdateValue(ref this.formSubmitResult, value))
            {
                this.OnPropertyChanged(nameof(this.IsConfirmationVisible));
            }
        }
    }

    public (int id, List<A2UIMessage> messages) CurrentMessages => this.wizardStepIndex == 0 ? this.dashboardMessages : this.formMessages;

    public static string FormatDisplayedKey(string key)
    {
        var lastSegment = key.Split('/').Last(s => !string.IsNullOrEmpty(s));
        if (string.IsNullOrEmpty(lastSegment))
        {
            return lastSegment;
        }

        if (lastSegment.EndsWith("DateTime", StringComparison.Ordinal) && lastSegment.Length > "DateTime".Length)
        {
            lastSegment = lastSegment.Substring(0, lastSegment.Length - "DateTime".Length);
        }

        var words = Regex.Matches(lastSegment, "[A-Z]+(?![a-z])|[A-Z]?[a-z]+|[0-9]+").Select(m => char.ToUpperInvariant(m.Value[0]) + m.Value.Substring(1));
        return string.Join(" ", words);
    }

    public static string FormatSubmittedValue(object? value)
    {
        if (value == null)
        {
            return "(null)";
        }

        if (value is JsonElement json)
        {
            return json.ValueKind switch
            {
                JsonValueKind.String => FormatIfDateTime(json.GetString()),
                JsonValueKind.Number => json.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Null => "(null)",
                _ => json.GetRawText()
            };
        }

        if (value is string text)
        {
            return FormatIfDateTime(text);
        }

        return value.ToString() ?? string.Empty;
    }

    public static string FormatIfDateTime(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        if (!DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
        {
            return text;
        }

        return dateTime.TimeOfDay == TimeSpan.Zero
            ? dateTime.ToString("MMM d, yyyy", CultureInfo.InvariantCulture)
            : dateTime.ToString("MMM d, yyyy h:mm tt", CultureInfo.InvariantCulture);
    }

    public void Dispose()
    {
        this.isDisposed = true;

        this.cts?.Cancel();
        this.cts?.Dispose();
        this.cts = null;
    }

    public void Submit(Dictionary<string, object?> values)
    {
        var dict = values.ToDictionary(kv => kv.Key, kv => kv.Value ?? (object)"(null)");
        this.FormSubmitResult = dict;
    }

    private void ChangeStepCommandExecute(int newStep)
    {
        this.WizardStepIndex = newStep;
        this.Error = null;

        this.MessagesChanged?.Invoke();
    }

    private void ResetCommandExecute()
    {
        this.cts?.Cancel();
        this.cts?.Dispose();
        this.cts = null;

        this.WizardCompleted = false;
        this.WizardStepIndex = -1;

        this.dashboardMessages.messages.Clear();
        this.formMessages.messages.Clear();

        this.dashboardStatusText = string.Empty;
        this.formStatusText = string.Empty;
        this.StatusText = string.Empty;
        this.FormSubmitResult = null;
        this.Error = null;

        this.MessagesChanged?.Invoke();
    }

    private void FinishCommandExecute()
    {
        if (this.formSubmitResult == null)
        {
            this.Error = "Please complete and submit the booking form before finishing.";
            return;
        }

        this.WizardCompleted = true;
    }

    private async Task RunAgentAsync()
    {
        this.cts?.Cancel();
        this.cts?.Dispose();
        this.cts = new CancellationTokenSource();

        this.IsAgentRunning = true;
        this.CurrentMessages.messages.Clear();
        this.StatusText = string.Empty;
        this.Error = null;

        if (this.WizardStepIndex == 1)
        {
            this.FormSubmitResult = null;
        }

        this.StatusText = "⏳ Processing your request...\n";
        this.MessagesChanged?.Invoke();

        try
        {
            var messages = await a2uiService.GenerateSurfaceAsync(this.Prompt, this.cts.Token);
            if (messages != null && messages.Count > 0)
            {
                this.SetCurrentMessages(messages);

                this.StatusText += "📋 Rendering components...\n";

                this.MessagesChanged?.Invoke();
                this.StatusText += "✅ Done\n";
            }
            else
            {
                this.StatusText = string.Empty;
                this.Error = "The AI service returned an empty response. Please try again.";
            }
        }
        catch (OperationCanceledException)
        {
            this.StatusText += "🚫 Cancelled.\n";
        }
        catch (Exception)
        {
            this.StatusText = string.Empty;
            this.Error = "The agent server is currently unavailable. Try again in a few moments.";
        }
        finally
        {
            this.IsAgentRunning = false;
        }
    }

    private void SetCurrentMessages(List<A2UIMessage> messages)
    {
        if (this.wizardStepIndex == 0)
        {
            this.dashboardMessages.messages.AddRange(messages);
            this.dashboardMessages.id = Guid.NewGuid().GetHashCode();
        }
        else
        {
            this.formMessages.messages.AddRange(messages);
            this.formMessages.id = Guid.NewGuid().GetHashCode();
        }
    }
}
