using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Layouts;

namespace QSF.Layouts;

public class UniformLayout : Layout, IUniformLayout
{
    public static readonly BindableProperty LayoutModeProperty = BindableProperty.Create(
        nameof(LayoutMode), typeof(UniformLayoutMode), typeof(UniformLayout),
        defaultValueCreator: b => ((UniformLayout)b).CreateDefaultLayoutMode());

    public static readonly BindableProperty SpacingProperty = BindableProperty.Create(
        nameof(Spacing), typeof(double), typeof(UniformLayout), 0.0);

    internal static readonly UniformLayoutMode DefaultLayoutMode;

    static UniformLayout()
    {
        DefaultLayoutMode = DeviceInfo.Idiom == DeviceIdiom.Desktop ? UniformLayoutMode.HorizontalStretch : UniformLayoutMode.HorizontalStack;
    }

    public UniformLayoutMode LayoutMode
    {
        get { return (UniformLayoutMode)this.GetValue(LayoutModeProperty); }
        set { this.SetValue(LayoutModeProperty, value); }
    }

    public double Spacing
    {
        get { return (double)this.GetValue(SpacingProperty); }
        set { this.SetValue(SpacingProperty, value); }
    }

    protected override ILayoutManager CreateLayoutManager()
    {
        return new UniformLayoutManager(this);
    }

    private UniformLayoutMode CreateDefaultLayoutMode()
    {
        return DefaultLayoutMode;
    }
}
