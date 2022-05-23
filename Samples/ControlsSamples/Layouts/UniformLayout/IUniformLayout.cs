using Microsoft.Maui;

namespace QSF.Layouts;

public interface IUniformLayout : ILayout
{
    UniformLayoutMode LayoutMode { get; }
    double Spacing { get; }
}
