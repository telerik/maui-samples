using Microsoft.Datasync.Client;
using TelerikCRM.Maui.Common;

namespace TelerikCRM.Maui.Services;

public abstract class RemoteServiceBase<T>(DatasyncClientService clientService) : IDataStore<T>
    where T : DatasyncClientData
{
    internal readonly IOfflineTable<T> table = clientService.Client.GetOfflineTable<T>();

    public async Task<IReadOnlyList<T>> GetItemsAsync()
        => await this.table.GetAsyncItems().ToListAsync();

    public async Task<T> GetItemAsync(string id)
        => await this.table.GetItemAsync(id);

    public abstract Task<string> GetIdAsync(T item);

    public async Task<bool> AddItemAsync(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "The item is null.");
        }

        try
        {
            await this.table.InsertItemAsync(item);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateItemAsync(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "The item is null.");
        }

        try
        {
            await this.table.ReplaceItemAsync(item);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteItemAsync(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "The item is null.");
        }

        // Short circuit for when the item has not been saved yet.
        if (item.Id == null)
        {
            return true;
        }

        try
        {
            await this.table.DeleteItemAsync(item);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}