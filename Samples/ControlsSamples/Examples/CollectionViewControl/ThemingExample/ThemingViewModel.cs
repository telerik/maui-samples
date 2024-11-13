using QSF.Examples.CollectionViewControl.Common;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.CollectionViewControl.ThemingExample;

public class ThemingViewModel : ExampleViewModel
{
    public ThemingViewModel()
    {
        this.Messages = MessageDataProvider.GetMessageData();
    }

    public ObservableCollection<MessageData> Messages { get; set; }
}