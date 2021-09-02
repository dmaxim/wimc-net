using System.Threading.Tasks;
using Moq;
using Wimc.Domain.Models;
using Wimc.Tests.Unit.TestInfrastructure;
using Xunit;

namespace Wimc.Tests.Unit.Managers
{
    /// <summary>
    /// Unit tests validating the behavior of the ResourceContainerManager Delete method
    /// </summary>
    [Collection("Resource Container Fixture Collection")]
    public class ResourceContainerManagerDelete
    {
        private readonly ResourceContainerFixture _testFixture;

        public ResourceContainerManagerDelete(ResourceContainerFixture fixture)
        {
            _testFixture = fixture;
        }

        [Fact]
        public async Task Calls_Get_On_The_Resource_Container_Repository()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var resourceContainerManager = _testFixture.GetResourceContainerManager(mockResourceContainerRepository);

            await resourceContainerManager.Delete(ResourceContainerFixture.ExistingResourceContainerId).ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.Get(It.IsAny<int>()),
                Times.Once);
        }

        [Fact]
        public async Task Calls_Get_On_The_Resource_Container_Repository_With_The_Expected_Resource_Container_Id()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var resourceContainerManager = _testFixture.GetResourceContainerManager(mockResourceContainerRepository);

            await resourceContainerManager.Delete(ResourceContainerFixture.ExistingResourceContainerId).ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.Get(
                    It.Is<int>(id => id.Equals(ResourceContainerFixture.ExistingResourceContainerId))), Times.Once);
        }

        [Fact]
        public async Task Calls_Delete_On_Resource_Container_Repository_To_Delete_The_Existing_Resource_Container()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var resourceContainerManager = _testFixture.GetResourceContainerManager(mockResourceContainerRepository);

            await resourceContainerManager.Delete(ResourceContainerFixture.ExistingResourceContainerId).ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.Delete(It.IsAny<ResourceContainer>()), Times.Once);
        }
        
        [Fact]
        public async Task Calls_Save_Changes_On_Resource_Container_Repository_To_Delete_The_Existing_Resource_Container()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var resourceContainerManager = _testFixture.GetResourceContainerManager(mockResourceContainerRepository);

            await resourceContainerManager.Delete(ResourceContainerFixture.ExistingResourceContainerId).ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Does_Not_Call_Delete_On_Resource_Container_Repository_When_The_Resource_Container_Does_Not_Exist()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var resourceContainerManager = _testFixture.GetResourceContainerManager(mockResourceContainerRepository);

            await resourceContainerManager.Delete(ResourceContainerFixture.NonExistentResourceContainerId).ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.Delete(It.IsAny<ResourceContainer>()), Times.Never);
        }

        [Fact]
        public async Task Does_Not_Call_Save_Changes_On_Resource_Container_Repository_When_The_Resource_Container_Does_Not_Exist()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var resourceContainerManager = _testFixture.GetResourceContainerManager(mockResourceContainerRepository);

            await resourceContainerManager.Delete(ResourceContainerFixture.NonExistentResourceContainerId).ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.SaveChangesAsync(), Times.Never);
        }
        
        
        
    }
}