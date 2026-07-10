namespace TelerikCRM.Maui.ViewModels;

public class AboutViewModel : ViewModelBase
{
    public AboutViewModel()
    {
        this.OpenWebCommand = new Command(async (obj) =>
        {
            if (obj is not string target) return;

            var url = target switch
            {
                "ProductPage" => "https://www.telerik.com/maui-ui",
                "ReleaseHistoryPage" => "https://www.telerik.com/support/whats-new/maui-ui",
                "DocumentationPage" => "https://docs.telerik.com/devtools/maui/introduction",
                "GitHubDemosPage" => "https://github.com/telerik/maui-samples",
                _ => "https://www.telerik.com"
            };

            await Launcher.OpenAsync(new Uri(url));
        });

#if !(MACCATALYST || WINDOWS)
        this.CanNavigateBack = true;
        this.NavigateBackContextName = "More";
        this.Title = "About";
#endif
    }

    public string TelerikDescription => $"Copyright © {DateTime.Now.Year} Progress Software Corporation";

    public string AppName => "Telerik CRM";

    public string AppVersion
    {
        get
        {
            var text = "";

#if WINDOWS
            var version = Windows.ApplicationModel.Package.Current.Id.Version;
            text = $"v{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";

#elif MACCATALYST || IOS
            text = $"v{Foundation.NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"]}";

#elif ANDROID
            text =  $"v{Android.App.Application.Context.ApplicationContext!.PackageManager!.GetPackageInfo(Android.App.Application.Context.ApplicationContext!.PackageName!, 0)!.VersionName}";
#endif

            return text;
        }
    }

    public Command OpenWebCommand { get; }
}