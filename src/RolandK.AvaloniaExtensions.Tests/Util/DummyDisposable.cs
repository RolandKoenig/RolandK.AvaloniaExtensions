namespace RolandK.AvaloniaExtensions.Tests.Util;

public class DummyDisposable(Action onDispose) : IDisposable
{
    /// <inheritdoc />
    public void Dispose()
    {
        onDispose();
    }
}