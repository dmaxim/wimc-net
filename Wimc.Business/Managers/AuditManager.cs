using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wimc.Domain.Messages.Events;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Business.Managers
{
    public class AuditManager : IAuditManager
    {

        private readonly IResourceContainerManager _resourceContainerManager;
        private readonly IEventRepository _eventRepository;
        public AuditManager(IResourceContainerManager resourceContainerManager, IEventRepository eventRepository)
        {
            _resourceContainerManager = resourceContainerManager;
            _eventRepository = eventRepository;
        }
        
        public async Task Generate()
        {
            var existingContainers = await _resourceContainerManager.GetAll().ConfigureAwait(false);

            foreach (var existingContainer in existingContainers)
            {
                var comparison = await _resourceContainerManager
                    .CompareExistingToRemote(existingContainer.ResourceContainerId).ConfigureAwait(false);

                await PublishResourceAddedEvents(comparison).ConfigureAwait(false);
                await PublishResourceDeletedEvents(comparison).ConfigureAwait(false);
            }
            
            
        }

        private async Task PublishResourceAddedEvents(ResourceComparison resourceComparison)
        {
            var resourcesAdded = resourceComparison.New.Select(resource => new ResourceAdded(resource)).ToList();
            if (resourcesAdded.Any())
            {
                await _eventRepository.Publish(resourcesAdded).ConfigureAwait(false);
            }
            
        }
        
        private async Task PublishResourceDeletedEvents(ResourceComparison resourceComparison)
        {
            var resourcesDeleted =
                resourceComparison.Deleted.Select(resource => new ResourceDeleted(resource)).ToList();

            if (resourcesDeleted.Any())
            {
                await _eventRepository.Publish(resourcesDeleted).ConfigureAwait(false);
            }
            

        }
    }
}