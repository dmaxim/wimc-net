using System.Threading.Tasks;
using Moq;
using Mx.Library.ExceptionHandling;
using Wimc.Domain.Models;
using Wimc.Tests.Unit.TestInfrastructure;
using Xunit;

namespace Wimc.Tests.Unit.Managers
{
    /// <summary>
    /// Unit tests validating the behavior of the ResourceContainerManager Edit method
    /// </summary>
    [Collection("Resource Container Fixture Collection")]
    public class ResourceContainerManagerEdit
    {
        private readonly ResourceContainerFixture _testFixture;

        public ResourceContainerManagerEdit(ResourceContainerFixture fixture)
        {
            _testFixture = fixture;
        }

        [Fact]
        public async Task Calls_Get_On_The_Resource_Container_Repository()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var resourceContainerManager = _testFixture.GetResourceContainerManager(mockResourceContainerRepository);

            await resourceContainerManager.Edit(EditResourceContainerCommand).ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.Get(It.IsAny<int>()),
                Times.Once);
        }

        [Fact]
        public async Task Calls_Get_On_The_Resource_Container_Repository_With_The_Expected_Resource_Container_Id()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var resourceContainerManager = _testFixture.GetResourceContainerManager(mockResourceContainerRepository);
            var editCommand = EditResourceContainerCommand;
            
            await resourceContainerManager.Edit(editCommand).ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.Get(It.Is<int>(id => id.Equals(editCommand.ResourceContainerId))),
                Times.Once);
        }

        [Fact]
        public async Task Throws_Expected_Not_Found_Exception_When_Resource_Container_With_Id_Does_Not_Exist()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var resourceContainerManager = _testFixture.GetResourceContainerManager(mockResourceContainerRepository);
            var editCommand =
                new EditResourceContainer(ResourceContainerFixture.NonExistentResourceContainerId, "new-name");

            await Assert.ThrowsAsync<MxNotFoundException>(() => resourceContainerManager.Edit(editCommand)).ConfigureAwait(false);

        }


        [Fact]
        public async Task Calls_Save_Changes_On_The_Resource_Container_Repository_When_Resource_Container_Exists()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var resourceContainerManager = _testFixture.GetResourceContainerManager(mockResourceContainerRepository);

            await resourceContainerManager.Edit(EditResourceContainerCommand).ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.SaveChangesAsync(),
                Times.Once);
        }


        private static EditResourceContainer EditResourceContainerCommand =>
            new EditResourceContainer(ResourceContainerFixture.ExistingResourceContainerId, "new-name");

    }
}