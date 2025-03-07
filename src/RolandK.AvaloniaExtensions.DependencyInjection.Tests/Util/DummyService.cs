namespace RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util;

public class DummyService : IDummyService
{
    /// <inheritdoc />
    public string GetSomeDummyValue()
    {
        return "TestValue";
    }
}