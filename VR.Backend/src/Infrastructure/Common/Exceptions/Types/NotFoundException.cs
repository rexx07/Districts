namespace Infrastructure.Common.Exceptions.Types;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}