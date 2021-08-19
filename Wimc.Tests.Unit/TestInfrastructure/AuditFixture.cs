using System.Collections.Generic;
using Moq;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Tests.Unit.TestInfrastructure
{
    /// <summary>
    /// Fixture supporting testing of the Audit functionality
    /// </summary>
    public class AuditFixture
    {
        public AuditFixture()
        {
            
        }

        /// <summary>
        /// Returns a mock IAuditRepository for use in unit tests
        /// </summary>
        /// <returns></returns>
        public Mock<IAuditRepository> GetMockAuditRepository()
        {
            var mockAuditRepository = new Mock<IAuditRepository>();

            mockAuditRepository.Setup(auditRepository => auditRepository.GetNewResources())
                .ReturnsAsync(() => NewResources);
            
            return mockAuditRepository;
        }

        /// <summary>
        /// Returns a mock IResourceContainerRepository for use in unit tests
        /// </summary>
        /// <returns></returns>
        public Mock<IResourceContainerRepository> GetMockResourceContainerRepository()
        {
            var mockResourceContainerRepository = new Mock<IResourceContainerRepository>();
            mockResourceContainerRepository.Setup(resourceContainerRepository => resourceContainerRepository.GetContainers(It.IsAny<IList<int>>()))
                .ReturnsAsync(() => ResourceContainers);
            
            return mockResourceContainerRepository;
        }

        /// <summary>
        /// List of Resources used for testing audit functionality
        /// </summary>
        private List<Resource> NewResources
        {
            get
            {
                return new List<Resource>
                {
                    new Resource{ ResourceId = 2, ResourceContainerId = 1},
                    new Resource{ ResourceId = 3, ResourceContainerId = 1},
                    new Resource{ ResourceId = 4, ResourceContainerId = 2}
                };
                
            }
        }
        
        /// <summary>
        /// List of ResourceContainers associated with the new resources used for testing the audit functionality
        /// </summary>
        private IList<ResourceContainer> ResourceContainers
        {
            get
            {
                return new List<ResourceContainer>
                {
                    new ResourceContainer{ ResourceContainerId = 1},
                    new ResourceContainer{ ResourceContainerId = 2}
                };

            }

        }
        
    }
}