using System.Collections.Generic;

namespace QSF.Common;

public class Configuration
{
    public string HeaderTitle { get; set; }

    public string HeaderIcon { get; set; }

    public string SplashTitle { get; set; }

    public string SplashIcon { get; set; }

    public string SplashInfo { get; set; }

    public string DocumentationUrl { get; set; }

    public string FeedbackPortalUrl { get; set; }

    public string DownloadTrialUrl { get; set; }

    public string ExampleCodeUrl { get; set; }

    public List<Control> Controls { get; set; }

    public List<MauiHighlight> MauiHighlights { get; set; }

    public List<DemoApp> DemoApps { get; set; }

    public List<HighlightedControl> HighlightedControls { get; set; }
}
