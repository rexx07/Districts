namespace Infrastructure.Common.Exceptions.Types;

public class AuthorizationException : Exception
{
    public AuthorizationException(string message)
        : base(message)
    {
    }
}