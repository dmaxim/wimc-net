using System;
using System.Threading.Tasks;
using Moq;
using Mx.Library.ExceptionHandling;
using Wimc.Business.Managers;
using Wimc.Domain.Models.CostManagement;
using Wimc.Tests.Unit.TestInfrastructure;
using Xunit;

namespace Wimc.Tests.Unit.Managers
{
    /// <summary>
    /// Unit Tests to validate the behavior of the CostManager GetMonthlyCost method
    /// </summary>
    [Collection("Cost Management Fixture Collection")]
    public class CostManagerGetMonthlyCost
    {
        private readonly CostManagementFixture _testFixture;
        
        public CostManagerGetMonthlyCost(CostManagementFixture fixture)
        {
            _testFixture = fixture;
        }


        [Fact]
        public async Task Calls_Get_On_Resource_Container_Repository()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var costManagementManager = new CostManagementManager(_testFixture.GetMockBillingRepository().Object,
                mockResourceContainerRepository.Object);
            _ = await costManagementManager.GetMonthlyCost(CostManagementFixture.ExistingResourceContainerId)
                .ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.Get(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Calls_Get_On_Resource_Container_Repository_With_Expected_Resource_Container_Id()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var costManagementManager = new CostManagementManager(_testFixture.GetMockBillingRepository().Object,
                mockResourceContainerRepository.Object);
            _ = await costManagementManager.GetMonthlyCost(CostManagementFixture.ExistingResourceContainerId)
                .ConfigureAwait(false);
            
            mockResourceContainerRepository.Verify(resourceContainerRepository => resourceContainerRepository.Get(It.Is<int>(id => id.Equals(CostManagementFixture.ExistingResourceContainerId))), Times.Once);
        }

        [Fact]
        public async  Task Throws_Expected_Not_Found_Exception_When_Resource_Container_Does_Not_Exist()
        {
            var mockResourceContainerRepository = _testFixture.GetMockResourceContainerRepository();
            var costManagementManager = new CostManagementManager(_testFixture.GetMockBillingRepository().Object,
                mockResourceContainerRepository.Object);
            await Assert.ThrowsAsync<MxNotFoundException>(() =>
                 costManagementManager.GetMonthlyCost(CostManagementFixture.NonExistentResourceContainerId));
        }

        [Fact]
        public async Task Calls_Get_Resource_Group_Cost_On_The_Billing_Repository_When_The_Resource_Container_Exists()
        {
            var mockBillingRepository = _testFixture.GetMockBillingRepository();
            var costManagementManager = new CostManagementManager(mockBillingRepository.Object,
                _testFixture.GetMockResourceContainerRepository().Object);
            _ = await costManagementManager.GetMonthlyCost(CostManagementFixture.ExistingResourceContainerId)
                .ConfigureAwait(false);

            mockBillingRepository.Verify(billingRepository => billingRepository.GetResourceGroupCost(It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>()),
                Times.Once);
        }

        [Fact]
        public async Task Calls_Get_Resource_Group_Cost_On_The_Billing_Repository_With_The_Expected_From_Date()
        {
            var expectedFromDate = DateTime.Now.AddDays(-30);
            var mockBillingRepository = _testFixture.GetMockBillingRepository();
            var costManagementManager = new CostManagementManager(mockBillingRepository.Object,
                _testFixture.GetMockResourceContainerRepository().Object);
            _ = await costManagementManager.GetMonthlyCost(CostManagementFixture.ExistingResourceContainerId)
                .ConfigureAwait(false);

            mockBillingRepository.Verify(billingRepository => billingRepository.GetResourceGroupCost(It.IsAny<string>(),
                    It.Is<DateTime>(fromDate => fromDate.ToString("M/d/yy").Equals(expectedFromDate.ToString("M/d/yy"))),
                    It.IsAny<DateTime>()),
                Times.Once);
        }

        [Fact]
        public async Task Calls_Get_Resource_Group_Cost_On_The_Billing_Repository_With_The_Expected_To_Date()
        {
            var expectedToDate = DateTime.Now;
            var mockBillingRepository = _testFixture.GetMockBillingRepository();
            var costManagementManager = new CostManagementManager(mockBillingRepository.Object,
                _testFixture.GetMockResourceContainerRepository().Object);
            _ = await costManagementManager.GetMonthlyCost(CostManagementFixture.ExistingResourceContainerId)
                .ConfigureAwait(false);

            mockBillingRepository.Verify(billingRepository => billingRepository.GetResourceGroupCost(It.IsAny<string>(),
                    It.IsAny<DateTime>(),
                    It.Is<DateTime>(toDate => toDate.ToString("M/d/yy").Equals(expectedToDate.ToString("M/d/yy")))),
                Times.Once);
        }

        [Fact]
        public async Task Returns_A_Time_Frame_Cost_Instance_For_The_Resource_Container()
        {
            var costManagementManager = new CostManagementManager(_testFixture.GetMockBillingRepository().Object,
                _testFixture.GetMockResourceContainerRepository().Object);

            var timeFrameCost = await costManagementManager.GetMonthlyCost(CostManagementFixture.ExistingResourceContainerId)
                .ConfigureAwait(false);
            Assert.IsType<TimeFrameCost>(timeFrameCost);
        }
        
        
        
        



    }
}