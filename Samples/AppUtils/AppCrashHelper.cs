using Telerik.Maui.Controls;

namespace Telerik.AppUtils;

public static class AppCrashHelper
{
    public static bool Initialize(IDispatcher dispatcher)
    {
        ShowLastRunCrashInfo(dispatcher);
        return AttachUnhandledExceptionHandler();
    }

    private static void ShowLastRunCrashInfo(IDispatcher dispatcher)
    {
        string message = null;

        try
        {
            string lastRunCrashInfoFilePath = GetFileName();

            if (File.Exists(lastRunCrashInfoFilePath))
            {
                message = File.ReadAllText(lastRunCrashInfoFilePath);
                File.Delete(lastRunCrashInfoFilePath);
            }
        }
        catch { }

        if (message != null)
        {
            dispatcher.DispatchDelayed(TimeSpan.FromSeconds(1), () =>
            {
                try
                {
                    MakeCopyToast("Last crash details:", message);
                }
                catch { }
            });
        }
    }

    private static bool AttachUnhandledExceptionHandler()
    {
        AppDomain currentAppDomain = AppDomain.CurrentDomain;

        if (currentAppDomain != null)
        {
            currentAppDomain.UnhandledException += (_, args) =>
            {
                try
                {
                    string message;

                    if (args.ExceptionObject is Exception ex)
                    {
                        string nl = Environment.NewLine;
                        message = $"Exception: {ex.GetType()}{nl}Message: {ex.Message}{nl}StackTrace: {ex.StackTrace}";
                    }
                    else
                    {
                        message = $"ExceptionObject: {args.ExceptionObject}";
                    }

                    string lastRunCrashInfoFilePath = GetFileName();
                    File.WriteAllText(lastRunCrashInfoFilePath, message);
                }
                catch { }
            };

            return true;
        }
        else
        {
            return false;
        }
    }

    private static string GetFileName() => Path.Combine(FileSystem.AppDataDirectory, "LastRunCrashInfo.txt");

    private static void MakeCopyToast(string title, string message)
    {
        Label titleLabel = new Label { Text = title, TextColor = Colors.White, Margin = new Thickness(15, 5, 15, 5) };
        RadPopup popup = new RadPopup();
        Button closeButton = new Button { Text = "X" };
        Grid.SetColumn(closeButton, 1);
        closeButton.Clicked += (_, _) => { popup.IsOpen = false; };
        Label messageLabel = new Label { Text = message, TextColor = Colors.White, Margin = new Thickness(15) };
        Grid.SetRow(messageLabel, 1);
        Grid.SetColumnSpan(messageLabel, 2);
        Button copyDetailsButton = new Button { Text = "Copy details" };
        copyDetailsButton.Clicked += async (_, _) => await Clipboard.SetTextAsync(message);
        Grid.SetRow(copyDetailsButton, 2);
        Grid.SetColumn(copyDetailsButton, 1);
        Grid grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        RadBorder border = new RadBorder { Content = grid, BackgroundColor = Colors.Black, Margin = new Thickness(25, 0, 25, 40), CornerRadius = new Thickness(6), Opacity = 0.85, HorizontalOptions = LayoutOptions.Center };
        grid.Children.Add(titleLabel);
        grid.Children.Add(closeButton);
        grid.Children.Add(messageLabel);
        grid.Children.Add(copyDetailsButton);
        Layout container = new RadLayout();
        container.Children.Add(border);
        popup.Content = container;
        popup.IsModal = true;
        Window window = Application.Current?.Windows[0];

        if (window != null)
        {
            container.WidthRequest = window.Width;
            container.MaximumHeightRequest = window.Height;
        }

        popup.Placement = PlacementMode.Center;
        popup.IsOpen = true;
    }
}
