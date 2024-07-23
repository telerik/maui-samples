namespace Telerik.AppUtils.Services;

public interface ITestingService
{
    bool IsAppUnderTest { get; set; }

    DateTime DateTimeNow(DateTime staticTime);

    Random Random(int seed);

    public event EventHandler<TestCommandEventArgs> OnCommand;
}