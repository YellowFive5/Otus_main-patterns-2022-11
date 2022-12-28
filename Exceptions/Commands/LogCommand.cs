#region Usings

using Exceptions;
using Exceptions.Commands;

#endregion

public class LogCommand : ICommand
{
    private readonly ILogger logger;
    private readonly string message;

    public LogCommand(ILogger logger, string message)
    {
        this.logger = logger;
        this.message = message;
    }

    public void Execute()
    {
        logger.Log(message);
    }
}