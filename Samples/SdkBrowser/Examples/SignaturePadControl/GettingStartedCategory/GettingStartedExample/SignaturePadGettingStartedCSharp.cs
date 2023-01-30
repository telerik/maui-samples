using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SignaturePadControl.GettingStartedCategory.GettingStartedExample;

public class SignaturePadGettingStartedCSharp : ContentView
{
    public SignaturePadGettingStartedCSharp()
    {
        var grid = new Grid();

        // >> signaturepad-getting-started-csharp
        var signaturePad = new RadSignaturePad
        {
            BorderColor = Colors.LightGray,
            BorderThickness = new Thickness(1)
        };
        // << signaturepad-getting-started-csharp

        grid.Children.Add(signaturePad);
        Content = grid;
    }
}