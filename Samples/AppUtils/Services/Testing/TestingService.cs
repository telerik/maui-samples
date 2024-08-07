namespace Telerik.AppUtils.Services;

public class TestingService : ITestingService
{
    public int? TestCommandTcpPort { get; set; }

    public bool IsAppUnderTest { get; set; }

    public Random Random(int seed) => this.IsAppUnderTest ? new Random(seed) : new Random();

    public DateTime DateTimeNow(DateTime staticTime) => this.IsAppUnderTest ? staticTime : DateTime.Now;

    internal Task<string?> HandleCommandAsync(string line)
    {
        var args = new TestCommandEventArgs(line);
        this.OnCommand?.Invoke(this, args);
        return args.Result;
    }

    public event EventHandler<TestCommandEventArgs>? OnCommand;
}