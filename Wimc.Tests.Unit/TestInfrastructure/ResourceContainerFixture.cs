// ReSharper disable ClassNeverInstantiated.Global

using System.Collections.Generic;
using Moq;
using Wimc.Business.Managers;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Tests.Unit.TestInfrastructure
{
    /// <summary>
    /// Test fixture supporting tests on the behavior of ResourceContainers
    /// </summary>
    public class ResourceContainerFixture
    {
        
        public const int ExistingResourceContainerId = 33;
        public const int NonExistentResourceContainerId = 55;
        
        public Mock<IResourceContainerRepository> GetMockResourceContainerRepository()
        {
            var mockResourceContainerRepository = new Mock<IResourceContainerRepository>();

            mockResourceContainerRepository.Setup(resourceContainerRepository =>
                    resourceContainerRepository.Get(It.Is<int>(id => id.Equals(ExistingResourceContainerId))))
                .ReturnsAsync(() => ExistingResourceContainer);
            
            mockResourceContainerRepository.Setup(resourceContainerRepository =>
                    resourceContainerRepository.Get(It.Is<int>(id => id.Equals(NonExistentResourceContainerId))))
                .ReturnsAsync(() => default);
            
            return mockResourceContainerRepository;
        }

        public Mock<IResourceRepository> GetMockResourceRepository()
        {
            var mockResourceRepository = new Mock<IResourceRepository>();

            return mockResourceRepository;
        }

        public Mock<IMessageRepository> GetMockMessageRepository()
        {
            var mockMessageRepository = new Mock<IMessageRepository>();
            return mockMessageRepository;
        }

        public ResourceContainerManager GetResourceContainerManager(Mock<IResourceContainerRepository> resourceContainerRepository)
        {
            return new ResourceContainerManager(resourceContainerRepository.Object, GetMockResourceRepository().Object,
                GetMockMessageRepository().Object);
        }
        

        private ResourceContainer ExistingResourceContainer => new ResourceContainer
        {
            ResourceContainerId = ExistingResourceContainerId,
            ContainerName = "test-container",
            Resources = new List<Resource>()
        };
    }
}