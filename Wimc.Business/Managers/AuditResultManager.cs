using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Business.Managers
{
    public class AuditResultManager : IAuditResultManager
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IResourceContainerRepository _resourceContainerRepository;
        
        public AuditResultManager(IAuditRepository auditRepository, IResourceContainerRepository resourceContainerRepository)
        {
            _auditRepository = auditRepository;
            _resourceContainerRepository = resourceContainerRepository;
        }
        
        /// <summary>
        /// Retrieve the list of new resources by resource container
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ResourceContainer>> GetNewResources()
        {
            var newResources = await _auditRepository.GetNewResources().ConfigureAwait(false);
           
            var resourceContainerIds =
                newResources.Select(resource => resource.ResourceContainerId).Distinct().ToList();

            if (!resourceContainerIds.Any())
            {
                return new List<ResourceContainer>();
            }
            var resourceContainers =
                await _resourceContainerRepository.GetContainers(resourceContainerIds).ConfigureAwait(false);

            foreach (var resourceContainer in resourceContainers)
            {
                resourceContainer.Resources = newResources.Where(resource =>
                    resource.ResourceContainerId.Equals(resourceContainer.ResourceContainerId)).ToList();
            }

            return resourceContainers;
        }
    }
}