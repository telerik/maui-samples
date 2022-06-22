using Microsoft.Maui.ApplicationModel;

namespace CryptoTracker.Utils
{
    public static class Utils
    {
        private const string DownloadURL = "https://www.telerik.com/maui-ui?utm_medium=product&utm_source=appstores&utm_campaign=maui-awareness-maui-crypto-app";

        public static void TryNavigateToDownloadUrl()
        {
            Launcher.TryOpenAsync(DownloadURL);
        }
    }
}
