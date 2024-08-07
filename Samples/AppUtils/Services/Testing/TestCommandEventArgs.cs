namespace Telerik.AppUtils.Services;

public class TestCommandEventArgs : EventArgs
{
    /// <summary>
    /// Set the Result task if the command can complete asynchronously with result.
    /// The string will be written out using "WriteLine", so it is recommended the response is simple.
    /// Setting Result will also block processing further commands, until the response is resolved.
    /// </summary>
    /// <value></value>
    public Task<string?> Result { get; set; }

    public string Command { get; private set; }

    public TestCommandEventArgs(string command)
    {
        this.Result = Task.FromResult<string?>(null);
        this.Command = command;
    }
}