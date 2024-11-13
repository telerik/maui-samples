using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.ChatControl.ThemingExample;

public class ThemingViewModel : ExampleViewModel
{
    private readonly Author authorBot;
    private Author authorMe;
    private string message;

    public ThemingViewModel()
    {
        this.authorMe = new Author() { Name = "Me" };
        this.authorBot = new Author() { Name = "Bot" };

        this.Items = new ObservableCollection<ChatItem>
        {
            new TextMessage() { Author = this.authorBot, Text = "Welcome to the RadChat!" },
            new TextMessage() { Author = this.authorBot, Text = "You can add new messages here" }
        };

        this.SendMessageCommand = new Command(this.OnSendMessageCommandExecuted);

    }

    public Author AuthorMe
    {
        get => this.authorMe;
        set => this.UpdateValue(ref this.authorMe, value);
    }

    public string Message
    {
        get => this.message;
        set => this.UpdateValue(ref this.message, value);
    }

    public IList<ChatItem> Items { get; set; }

    public ICommand SendMessageCommand { get; set; }

    private void OnSendMessageCommandExecuted()
    {
        if (String.IsNullOrEmpty(this.Message))
        {
            return;
        }

        TextMessage item = new()
        {
            Author = this.AuthorMe,
            Text = this.Message
        };

        this.Items.Add(item);
        this.Message = String.Empty;
    }
}
