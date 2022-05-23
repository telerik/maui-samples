using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace QSF.Layouts;

public partial class QGrid : Grid
{
    protected override ILayoutManager CreateLayoutManager()
    {
        return new QGridLayoutManager(this);
    }
}
