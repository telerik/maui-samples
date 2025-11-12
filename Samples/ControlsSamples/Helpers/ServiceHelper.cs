using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace QSF.Helpers;

/// <summary>
/// A helper class to resolve services from the dependency injection container.
/// Provides a centralized way to access services throughout the application.
/// </summary>
public static class ServiceHelper
{
    /// <summary>
    /// Gets a service of the specified type from the dependency injection container.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    /// <returns>The service instance, or null if the service is not registered or cannot be resolved.</returns>
    public static T GetService<T>() where T : class
    {
        try
        {
            var serviceProvider = Application.Current?.Handler?.MauiContext?.Services;
            return serviceProvider?.GetService<T>();
        }
        catch
        {
            // If service resolution fails, return null to avoid crashes
            return null;
        }
    }

    /// <summary>
    /// Gets a required service of the specified type from the dependency injection container.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    /// <returns>The service instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the service is not registered.</exception>
    public static T GetRequiredService<T>() where T : class
    {
        var serviceProvider = Application.Current?.Handler?.MauiContext?.Services;
        if (serviceProvider == null)
        {
            throw new InvalidOperationException("Service provider is not available. Make sure the application is properly initialized.");
        }

        return serviceProvider.GetRequiredService<T>();
    }

    /// <summary>
    /// Tries to get a service of the specified type from the dependency injection container.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    /// <param name="service">When this method returns, contains the service instance if found; otherwise, null.</param>
    /// <returns>true if the service was found; otherwise, false.</returns>
    public static bool TryGetService<T>(out T service) where T : class
    {
        service = GetService<T>();
        return service != null;
    }
}