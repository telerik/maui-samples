namespace TelerikCRM.Maui.Services;

public interface IDataStore<T>
{
    Task<bool> AddItemAsync(T item);

    Task<bool> UpdateItemAsync(T item);

    Task<bool> DeleteItemAsync(T item);

    Task<T> GetItemAsync(string id);

    Task<string> GetIdAsync(T item);

    Task<IReadOnlyList<T>> GetItemsAsync();
}