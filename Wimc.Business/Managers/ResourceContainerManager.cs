using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mx.Library.Serialization;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Business.Managers
{
    public class ResourceContainerManager : IResourceContainerManager
    {
        private readonly IResourceContainerRepository _resourceContainerRepository;

        public ResourceContainerManager(IResourceContainerRepository resourceContainerRepository)
        {
            _resourceContainerRepository = resourceContainerRepository;
        }
        
        public async Task<IList<ResourceContainer>> GetAll()
        {
            return await _resourceContainerRepository.GetAll().ToListAsync().ConfigureAwait(false);
        }

        public async Task<ResourceContainer> Get(int resourceContainerId)
        {
            return await _resourceContainerRepository
                .FindSingleAsync(container => container.ResourceContainerId == resourceContainerId).ConfigureAwait(false);
        }

        public async Task<ResourceContainer> Create(string name, string containerJson)
        {
            var newContainer = new ResourceContainer
            {
                ContainerName = name,
                
            };
            
            var azureResources = containerJson.DeserializeJson<IList<AzureResource>>();
            newContainer.Resources = azureResources.Select(azureResource => new Resource(azureResource)).ToList();

            _resourceContainerRepository.Insert(newContainer);
            await _resourceContainerRepository.SaveChangesAsync().ConfigureAwait(false);
            return newContainer;
        }

        public async Task<ResourceContainer> GetById(int resourceContainerId)
        {
            return await _resourceContainerRepository.Get(resourceContainerId).ConfigureAwait(false);
        }

        public async Task Edit(EditResourceContainer editResourceContainer)
        {
            var container = await _resourceContainerRepository.Get(editResourceContainer.ResourceContainerId)
                .ConfigureAwait(false);

            container.ContainerName = editResourceContainer.ResourceContainerName;

            await _resourceContainerRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(int resourceContainerId)
        {
            var container = await _resourceContainerRepository.Get(resourceContainerId).ConfigureAwait(false);

            if (container != null)
            {
                _resourceContainerRepository.Delete(container);
                await _resourceContainerRepository.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}