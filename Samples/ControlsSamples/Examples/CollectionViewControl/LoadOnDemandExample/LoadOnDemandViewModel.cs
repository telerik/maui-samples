using Microsoft.Maui.Controls;
using QSF.Examples.CollectionViewControl.Common;
using QSF.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Telerik.AppUtils.Services;
using Telerik.Maui;

namespace QSF.Examples.CollectionViewControl.LoadOnDemandExample;

public class LoadOnDemandViewModel : ConfigurationExampleViewModel
{
    private ObservableCollection<MessageData> messageSource = MessageDataProvider.GetMessageData();
    private int loadOnDemandBufferItemsCount = 10;
    private LoadOnDemandMode loadOnDemandMode = LoadOnDemandMode.Manual;
    private bool isAutomaticLoadOnDemand;

    public LoadOnDemandViewModel()
    {
        this.Messages = new LoadOnDemandCollection<MessageData>(this.LoadMoreMessages);
        foreach (var item in MessageDataProvider.GetMessageData())
        {
            this.Messages.Add(item);
        }
    }

    public LoadOnDemandCollection<MessageData> Messages { get; set; }

    public IEnumerable<LoadOnDemandMode> LoadOnDemandModes { get; } = Enum.GetValues(typeof(LoadOnDemandMode)).Cast<LoadOnDemandMode>();

    public int LoadOnDemandBufferItemsCount
    {
        get => this.loadOnDemandBufferItemsCount;
        set => this.UpdateValue(ref this.loadOnDemandBufferItemsCount, value);
    }

    public LoadOnDemandMode LoadOnDemandMode
    {
        get => this.loadOnDemandMode;
        set
        {
            if (this.UpdateValue(ref this.loadOnDemandMode, value))
            {
                this.IsAutomaticLoadOnDemand = this.loadOnDemandMode == LoadOnDemandMode.Automatic;
            }
        }
    }

    public bool IsAutomaticLoadOnDemand
    {
        get => this.isAutomaticLoadOnDemand;
        set => this.UpdateValue(ref this.isAutomaticLoadOnDemand, value);
    }

    private IEnumerable LoadMoreMessages(CancellationToken cancellationToken)
    {
        List<MessageData> newMessages = new List<MessageData>();

        try
        {
            Thread.Sleep(2000);

            var random = DependencyService.Get<ITestingService>().Random(30);

            ObservableCollection<MessageData> additionalItems = MessageDataProvider.GetMessageDataChunk(random.Next(5, 30));
            foreach (var item in additionalItems)
            {
                newMessages.Add(item);
            }

            return newMessages;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
