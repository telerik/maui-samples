using Microsoft.Maui.Graphics;
using QSF.Examples.CollectionViewControl.Common;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.CollectionViewControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    public FirstLookViewModel()
    {
        this.Messages = MessageDataProvider.GetMessageData();
    }

    public ObservableCollection<MessageData> Messages { get; set; }
}
