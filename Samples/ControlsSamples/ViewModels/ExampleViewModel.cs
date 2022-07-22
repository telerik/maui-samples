using QSF.Common;

namespace QSF.ViewModels;

public class ExampleViewModel : PageViewModel
{
    private object content;
    private Example example;

    public object Content
    {
        get { return this.content; }
        set { this.UpdateValue(ref this.content, value); }
    }

    public Example Example
    {
        get { return this.example; }
        set { this.UpdateValue(ref this.example, value); }
    }
}
