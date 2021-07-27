using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mx.Library.Serialization;
using Newtonsoft.Json.Linq;
using Wimc.Business.Builders;
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
                .GetAll().Include(container => container.Resources)
                .FirstOrDefaultAsync(container => container.ResourceContainerId == resourceContainerId)
                .ConfigureAwait(false);
        }

        public async Task<ResourceContainer> Create(string name, string containerJson)
        {
            var newContainer = ResourceContainerBuilder.BuildFromUpload(name, containerJson);
            _resourceContainerRepository.Insert(newContainer);
            await _resourceContainerRepository.SaveChangesAsync().ConfigureAwait(false);
            return newContainer;
        }

        public async Task<ResourceContainer> CreateFromDefinition(string name, string containerJson)
        {
            var newContainer = ResourceContainerBuilder.BuildFromApi(name, containerJson);
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

        public async Task<string> GetDefinition(string resourceContainerName)
        {
            return await _resourceContainerRepository.GetDefinition(resourceContainerName).ConfigureAwait(false);
        }

        public async Task<IList<ResourceContainer>> GetAllWithResources()
        {
            return await _resourceContainerRepository.GetAll()
                .Include(container => container.Resources)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<ResourceComparison> CompareExistingToRemote(int containerId)
        {
            var existing = await Get(containerId).ConfigureAwait(false);
            var remoteDefinition = await GetDefinition(existing.ContainerName).ConfigureAwait(false);
            var remote = ResourceContainerBuilder.BuildFromApi(existing.ContainerName, remoteDefinition);
            
            return new ResourceComparison(existing.Resources.ToList(), remote.Resources.ToList(), containerId, existing.ContainerName);

        }
    }
}