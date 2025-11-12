using QSF.Examples.CollectionViewControl.Common;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QSF.Examples.SkeletonControl.CustomTemplateExample;

public class CustomTemplateViewModel : ExampleViewModel
{
    private bool isLoading;

    public CustomTemplateViewModel()
    {
        this.Messages = MessageDataProvider.GetMessageData();
        this.ReloadCommand = new Microsoft.Maui.Controls.Command(async () => await this.StartLoadingAsync());
        _ = this.StartLoadingAsync();
    }

    public ObservableCollection<MessageData> Messages { get; }

    public ICommand ReloadCommand { get; }

    public bool IsLoading
    {
        get => this.isLoading;
        set
        {
            if (this.isLoading != value)
            {
                this.isLoading = value;
                this.OnPropertyChanged();
            }
        }
    }

    private async Task StartLoadingAsync()
    {
        this.IsLoading = true;
        await Task.Delay(5000);
        this.IsLoading = false;
    }
}
