using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using QSF.Helpers;

namespace QSF.Common;

public class HyperlinkLabel : Label
{
    public static readonly BindableProperty UrlProperty =
        BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkLabel), null);

    public HyperlinkLabel()
    {
        TextColor = Application.Current.RequestedTheme == AppTheme.Light 
            ? Color.FromArgb("#3D57D8")
            : Color.FromArgb("#8DB8FF");
        
        GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(async () => await Launcher.OpenAsync(Url))
        });

#if MACCATALYST || WINDOWS
        var pointerRecognizer = new PointerGestureRecognizer();
        pointerRecognizer.PointerEntered += (s, e) => DesktopUtils.SetMouseCursorType(this, MouseCursorType.Hand);
        pointerRecognizer.PointerExited += (s, e) => DesktopUtils.SetMouseCursorType(this, MouseCursorType.Arrow);

        this.GestureRecognizers.Add(pointerRecognizer);
#endif
    }

    public string Url
    {
        get => (string)GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }
}
