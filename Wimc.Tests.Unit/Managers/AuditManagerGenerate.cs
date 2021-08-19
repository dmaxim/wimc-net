using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Wimc.Business.Managers;
using Wimc.Domain.Messages.Events;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;
using Xunit;

namespace Wimc.Tests.Unit.Managers
{
    public class AuditManagerGenerate
    {
        [Fact]
        public async Task Calls_Get_All_On_Resource_Container_Manager()
        {
            var resourceContainerManager = MockResourceContainerManager;
            var auditManager = new AuditManager(resourceContainerManager.Object, MockEventRepository.Object);

            await auditManager.Generate().ConfigureAwait(false);

            resourceContainerManager.Verify(manager => manager.GetAll(), Times.Once);

        }


        [Fact]
        public async Task Calls_Compare_On_Resource_Container_Manager_For_Each_Resource_Container()
        {
            var resourceContainerManager = MockResourceContainerManager;
            var auditManager = new AuditManager(resourceContainerManager.Object, MockEventRepository.Object);

            await auditManager.Generate().ConfigureAwait(false);

            resourceContainerManager.Verify(manager => manager.CompareExistingToRemote(It.IsAny<int>()),
                Times.Exactly(2));
        }

        [Fact]
        public async Task Calls_Publish_On_Event_Repository_For_New_Resources()
        {
            var eventRepository = MockEventRepository;
            var auditManager = new AuditManager(MockResourceContainerManager.Object, eventRepository.Object);

            await auditManager.Generate().ConfigureAwait(false);
            
            eventRepository.Verify(repository => repository.Publish(It.IsAny<IList<ResourceAdded>>()), Times.AtLeast(1));
        }
        
        [Fact]
        public async Task Calls_Publish_On_Event_Repository_For_Deleted_Resources()
        {
            var eventRepository = MockEventRepository;
            var auditManager = new AuditManager(MockResourceContainerManager.Object, eventRepository.Object);

            await auditManager.Generate().ConfigureAwait(false);
            
            eventRepository.Verify(repository => repository.Publish(It.IsAny<IList<ResourceDeleted>>()), Times.AtLeast(1));
        }

        [Fact]
        public async Task Does_Not_Call_Compare_Existing_To_Remote_On_Resource_Container_Manager_When_There_Are_Not_Any_Existing_Resource_Containers()
        {
            var resourceContainerManager = MockResourceContainerManager;
            resourceContainerManager.Setup(manager => manager.GetAll())
                .ReturnsAsync(() => new List<ResourceContainer>());
            
            var auditManager = new AuditManager(resourceContainerManager.Object, MockEventRepository.Object);

            await auditManager.Generate().ConfigureAwait(false);

            resourceContainerManager.Verify(manager => manager.CompareExistingToRemote(It.IsAny<int>()),
                Times.Never);
        }
        
        

        private Mock<IResourceContainerManager> MockResourceContainerManager
        {
            get
            {
                var mockManager = new Mock<IResourceContainerManager>();
                mockManager.Setup(manager => manager.GetAll())
                    .ReturnsAsync(() => ExistingContainers);

                mockManager.Setup(manager => manager.CompareExistingToRemote(It.IsAny<int>()))
                    .ReturnsAsync(() => ComparisonWIthNewAndDeletedResources);
                
                return mockManager;
            }
        }

        private Mock<IEventRepository> MockEventRepository => new Mock<IEventRepository>();
        private IList<ResourceContainer> ExistingContainers
        {
            get
            {
                var existingResources = ExistingResources;
                return new List<ResourceContainer>
                {
                    new ResourceContainer {ResourceContainerId = 1, Resources = existingResources.Where(resource => resource.ResourceContainerId.Equals(1)).ToList()},
                    new ResourceContainer {ResourceContainerId =  2, Resources = existingResources.Where(resource => resource.ResourceContainerId.Equals(2)).ToList()}
                };

            }
        }

        private IList<Resource> ExistingResources
        {
            get
            {
                return new List<Resource>
                {
                    new Resource {ResourceId = 1, CloudId = "resource-one", ResourceName = "resource-one", ResourceContainerId = 1, ResourceType = "type-one"},
                    new Resource {ResourceId = 2, CloudId = "resource-two", ResourceName = "resource-two", ResourceContainerId = 1, ResourceType = "type-one"},
                    new Resource {ResourceId = 3, CloudId = "resource-three", ResourceName = "resource-three", ResourceContainerId = 2, ResourceType = "type-one"}
                };
            }
        }

        private IList<Resource> RemoteResources
        {
            get
            {
                return new List<Resource>
                {
                    new Resource {ResourceId = 1, CloudId = "resource-one", ResourceName = "resource-one", ResourceType = "type-one"},
                  //  new Resource {ResourceId = 2, CloudId = "resource-two", ResourceName = "resource-two"} # Deleted
                  new Resource {ResourceId = 3, CloudId = "resource-new", ResourceName = "resource-new", ResourceType = "type-one"},
                };
            }
        }

        private ResourceComparison ComparisonWIthNewAndDeletedResources
        {
            get
            {
                return new ResourceComparison(ExistingResources, RemoteResources, 1, "MyContainer");


            }
        }
    }
}