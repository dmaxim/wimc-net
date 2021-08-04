using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        
        public async Task<IList<ResourceContainer>> GetNewResources()
        {
            var newResources = await _auditRepository.GetNewResources().ConfigureAwait(false);
           
            var resourceContainerIds =
                newResources.Select(resource => resource.ResourceContainerId).Distinct().ToList();

            var resourceContainers = await _resourceContainerRepository.GetAll()
                .Where(container => resourceContainerIds.Contains(container.ResourceContainerId))
                .ToListAsync().ConfigureAwait(false);

            foreach (var resourceContainer in resourceContainers)
            {
                resourceContainer.Resources = newResources.Where(resource =>
                    resource.ResourceContainerId.Equals(resourceContainer.ResourceContainerId)).ToList();
            }

            return resourceContainers;
        }
    }
}