namespace RolandK.AvaloniaExtensions.Tests.Util;

// ReSharper disable once ClassNeverInstantiated.Global
internal class UnitTestApplicationFixture : IDisposable
{
    /// <inheritdoc />
    public void Dispose()
    {
        UnitTestApplication.StopAsync().Wait();
    }
}

[CollectionDefinition(nameof(ApplicationTestCollection))]
public class ApplicationTestCollection : ICollectionFixture<UnitTestApplicationFixture> { }