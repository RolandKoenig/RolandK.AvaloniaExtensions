namespace RolandK.AvaloniaExtensions.DependencyInjection;

public class AvaloniaExtensionsException : Exception
{
    public AvaloniaExtensionsException()
    {
    }

    public AvaloniaExtensionsException(string message)
        : base(message)
    {
    }

    public AvaloniaExtensionsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}