using Xunit;

namespace RocaWebApi.Tests
{
    [CollectionDefinition(nameof(SharedFixture))]
    public class SharedFixtureCollection : ICollectionFixture<SharedFixture>
    {
    }
}
