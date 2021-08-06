using System;
using System.Threading.Tasks;
using Mx.Library.ExceptionHandling;
using Wimc.Domain.Models.CostManagement;
using Wimc.Domain.Repositories;

namespace Wimc.Business.Managers
{
    public class CostManagementManager : ICostManagementManager
    {
        private readonly IBillingRepository _billingRepository;
        private readonly IResourceContainerRepository _resourceContainerRepository;
        
        public CostManagementManager(IBillingRepository billingRepository, IResourceContainerRepository resourceContainerRepository)
        {
            _billingRepository = billingRepository;
            _resourceContainerRepository = resourceContainerRepository;
        }
        
        public async Task<TimeFrameCost> GetMonthlyCost(int resourceContainerId)
        {
            var resourceContainer = await _resourceContainerRepository.Get(resourceContainerId).ConfigureAwait(false);
            if (resourceContainer == null)
            {
                throw new MxNotFoundException($"A resource container with id {resourceContainerId} was not found");
            }
            
            return await _billingRepository.GetResourceGroupCost(resourceContainer.ContainerName,
                DateTime.Now.AddDays(-30),
                DateTime.Now).ConfigureAwait(false);
        }
    }
}