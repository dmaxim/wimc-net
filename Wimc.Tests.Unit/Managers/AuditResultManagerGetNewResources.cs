using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Wimc.Business.Managers;
using Wimc.Domain.Models;
using Wimc.Tests.Unit.TestInfrastructure;
using Xunit;

namespace Wimc.Tests.Unit.Managers
{
    /// <summary>
    /// Unit tests validating the behavior of the AuditResultManager GetNewResources method
    /// </summary>
    [Collection("Audit Fixture Collection")]
    public class AuditResultManagerGetNewResources
    {
        private readonly AuditFixture _testFixture;
        
        public AuditResultManagerGetNewResources(AuditFixture fixture)
        {
            _testFixture = fixture;
        }
        
        [Fact]
        public async Task Calls_Get_New_Resources_On_The_Audit_Repository()
        {
            var mockAuditRepository = _testFixture.GetMockAuditRepository();
            var auditManager = new AuditResultManager(mockAuditRepository.Object,
                _testFixture.GetMockResourceContainerRepository().Object);

            _ = await auditManager.GetNewResources().ConfigureAwait(false);
            
            mockAuditRepository.Verify(auditRepository => auditRepository.GetNewResources(), Times.Once);
        }


        [Fact]
        public async Task Calls_Get_Containers_On_The_Resource_Container_Repository()
        {
            var mockAuditRepository = _testFixture.GetMockAuditRepository();
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var auditManager = new AuditResultManager(mockAuditRepository.Object, mockResourceContainerRepository.Object);
            _ = await auditManager.GetNewResources().ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.GetContainers(It.Is<IList<int>>(ids => ids.Count > 0)),
                Times.Once);
        }

        [Fact]
        public async Task Calls_Get_Containers_On_The_Resource_Container_Repository_With_The_Expected_Number_Of_Ids()
        {
            var mockAuditRepository = _testFixture.GetMockAuditRepository();
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var auditManager = new AuditResultManager(mockAuditRepository.Object, mockResourceContainerRepository.Object);
            _ = await auditManager.GetNewResources().ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.GetContainers(It.Is<IList<int>>(ids => ids.Count == 2)),
                Times.Once);
        }

        [Fact]
        public async Task Does_Not_Call_Get_Containers_On_The_Resource_Container_Repository_When_There_Are_Not_Any_New_Resources()
        {
            var mockAuditRepository = _testFixture.GetMockAuditRepository();
            mockAuditRepository.Setup(auditRepository => auditRepository.GetNewResources())
                .ReturnsAsync(() => new List<Resource>());
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var auditManager = new AuditResultManager(mockAuditRepository.Object, mockResourceContainerRepository.Object);
            _ = await auditManager.GetNewResources().ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.GetContainers(It.IsAny<IList<int>>()),
                Times.Never);
        }
        
    }
}