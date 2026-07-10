using Microsoft.Datasync.Client;
using Microsoft.Datasync.Client.SQLiteStore;
using TelerikCRM.Maui.Common;
using TelerikCRM.Maui.Models.DataService;

namespace TelerikCRM.Maui.Services;
#pragma warning disable CA1416
public class DatasyncClientService
{
    public DatasyncClient Client { get; }

    public DatasyncClientService()
    {
        var connectionString = new UriBuilder
        {
            Scheme = "file",
            Path = $"{FileSystem.CacheDirectory}/offline.db",
            Query = "?mode=rwc"
        }.Uri.ToString();

        var store = new OfflineSQLiteStore(connectionString);

        store.DefineTable<Employee>();
        store.DefineTable<Customer>();
        store.DefineTable<Product>();
        store.DefineTable<Order>();

        var options = new DatasyncClientOptions
        {
            OfflineStore = store,
            HttpPipeline =
            [
                new LoggingHandler()
            ]
        };

        this.Client = new DatasyncClient(ServiceConstants.ServiceUrl, options);
    }

    public async Task RefreshItemsAsync()
    {
        if (Connectivity.NetworkAccess == NetworkAccess.None)
        {
            return;
        }

        // Initialize the offline store (this will create the local database if it doesn't exist, and do nothing if it already exists)
        // This will cause a longer first run experience, but is necessary to ensure the local database is ready before we attempt to push/pull data
        await this.Client.InitializeOfflineStoreAsync();

        // This is a protective mechanism to prevent stale data in your offline db form overwriting newer dat aon the server db
        await this.PushSafelyAsync<Employee>();
        await this.PushSafelyAsync<Customer>();
        await this.PushSafelyAsync<Product>();
        await this.PushSafelyAsync<Order>();

        // After new/updated items have been pushed, we can update the local offline database with newer items from the server
        await Client.GetOfflineTable<Employee>().PullItemsAsync();
        await Client.GetOfflineTable<Customer>().PullItemsAsync();
        await Client.GetOfflineTable<Product>().PullItemsAsync();
        await Client.GetOfflineTable<Order>().PullItemsAsync();
    }

    private async Task PushSafelyAsync<T>() where T : DatasyncClientData
    {
        try
        {
            await Client.GetOfflineTable<T>().PushItemsAsync();
        }
        catch (DatasyncConflictException<T> conflict)
        {
            // conflict.Item is the local record
            var clientUpdatedAt = conflict.Item?.UpdatedAt;

            // conflict.Value is the server record (it's a json object at this point)
            var serverUpdatedAt = conflict.Value["updatedAt"]?.ToObject<DateTimeOffset?>();

            // IMPORTANT Only overwrite the server record if the local copy is newer that server version
            if (clientUpdatedAt > serverUpdatedAt)
            {
                await Client.GetOfflineTable<T>().ReplaceItemAsync(conflict.Item);
            }
        }
    }
}

#pragma warning restore CA1416
