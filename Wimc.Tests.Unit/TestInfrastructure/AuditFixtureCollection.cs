using Xunit;

namespace Wimc.Tests.Unit.TestInfrastructure
{
    /// <summary>
    /// Fixture Collection supporting the testing of Audit related functionality
    /// </summary>
    [CollectionDefinition("Audit Fixture Collection")]
    public class AuditFixtureCollection : ICollectionFixture<AuditFixture>
    {
        
    }
}