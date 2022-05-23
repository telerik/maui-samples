using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace QSF.Layouts;

public partial class VerticalUniformLayout : Grid
{
    protected override ILayoutManager CreateLayoutManager()
    {
        return new VerticalUniformLayoutManager(this);
    }
}
