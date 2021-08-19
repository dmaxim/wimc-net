
// ReSharper disable ClassNeverInstantiated.Global

using System;
using Moq;
using Wimc.Domain.Models;
using Wimc.Domain.Models.CostManagement;
using Wimc.Domain.Repositories;

namespace Wimc.Tests.Unit.TestInfrastructure
{
    /// <summary>
    /// Fixture supporting testing of the Cost Management functionality
    /// </summary>
    public class CostManagementFixture
    {
        public const int ExistingResourceContainerId = 5;
        public const int NonExistentResourceContainerId = 66;
        private const string ExistingContainerName = "test-container";
        public Mock<IBillingRepository> GetMockBillingRepository()
        {
            var mockBillingRepository = new Mock<IBillingRepository>();
            mockBillingRepository.Setup(billingRepository => billingRepository.GetResourceGroupCost(It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>())).ReturnsAsync(() => TimeFrameCost);
            
            return mockBillingRepository;

        }


        public Mock<IResourceContainerRepository> GetMockResourceContainerRepository()
        {
            var mockResourceContainerRepository = new Mock<IResourceContainerRepository>();
            mockResourceContainerRepository.Setup(resourceContainerRepository =>
                    resourceContainerRepository.Get(It.Is<int>(id => id.Equals(ExistingResourceContainerId))))
                .ReturnsAsync(() => ExistingResourceContainer);
            
            return mockResourceContainerRepository;
        }

        private ResourceContainer ExistingResourceContainer =>
            new ResourceContainer
                { ResourceContainerId = ExistingResourceContainerId, ContainerName = ExistingContainerName};

        private TimeFrameCost TimeFrameCost =>
            new TimeFrameCost(DateTime.Now.AddDays(-30), DateTime.Now, ExistingContainerName, "55.78"
            );
    }
}