namespace TelerikCRM.Maui.Services;

public interface INavigationService
{
    public Task NavigateToAsync<TViewModel>(params object[] arguments);

    public Task NavigateToRootAsync();

    public Task NavigateBackAsync();
}
