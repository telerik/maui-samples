using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;

namespace QSF.Examples.EditorControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
	private string name = "Glen Stracke";
	private string emailAddress = "glen_stracke@email.com";
	private string subject = "Financial advice";
	private string message = "I’m looking for some general financial advice to help me review where I’m at and plan ahead. I’d love guidance on budgeting, saving, and investment options that fit my goals. Let me know if you need any more details from me. Thanks!";

	public FirstLookViewModel()
	{
		this.SendMessageCommand = new Command(this.SendMessage);
	}

	public string Name
	{
		get
		{
			return this.name;
		}
		set
		{
			this.UpdateValue(ref this.name, value);
		}
	}

	public string EmailAddress
	{
		get
		{
			return this.emailAddress;
		}
		set
		{
			this.UpdateValue(ref this.emailAddress, value);
		}
	}

	public string Subject
	{
		get
		{
			return this.subject;
		}
		set
		{
			this.UpdateValue(ref this.subject, value);
		}
	}

	public string Message
	{
		get
		{
			return this.message;
		}
		set
		{
			this.UpdateValue(ref this.message, value);
		}
	}

	public Command SendMessageCommand { get; }

	private void SendMessage()
	{
		string sendMessage;

		if (string.IsNullOrEmpty(this.Name) ||
			string.IsNullOrEmpty(this.EmailAddress) ||
			string.IsNullOrEmpty(this.Subject) ||
			string.IsNullOrEmpty(this.Message))
		{
			sendMessage = "All fields are mandatory!";
		}
		else
		{
			sendMessage = "Message has been sent!";
			this.Name = string.Empty;
			this.EmailAddress = string.Empty;
			this.Subject = string.Empty;
			this.Message = string.Empty;
		}

		var toastService = DependencyService.Get<IToastMessageService>();
		toastService.ShortAlert(sendMessage);
	}
}
