using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wimc.Business.Builders;
using Wimc.Business.Configuration;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Business.Managers
{
    public class ResourceContainerManager : IResourceContainerManager
    {
        private readonly IResourceContainerRepository _resourceContainerRepository;
        private readonly IResourceRepository _resourceRepository;

        public ResourceContainerManager(IResourceContainerRepository resourceContainerRepository, IResourceRepository resourceRepository)
        {
            _resourceContainerRepository = resourceContainerRepository;
            _resourceRepository = resourceRepository;
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

            await AppendChildResources(newContainer).ConfigureAwait(false);

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
            await AppendChildResources(remote).ConfigureAwait(false);
            
            return new ResourceComparison(existing.Resources.ToList(), remote.Resources.ToList(), containerId, existing.ContainerName);

        }

        private async Task AppendChildResources(ResourceContainer resourceContainer)
        {
            var webSites = resourceContainer.Resources
                .Where(resource => resource.ResourceType.Equals(ResourceTypes.WebSite)).ToList();

            foreach (var webSite in webSites)
            {
                var hybridConnections =
                    await _resourceRepository.GetResourceDefinition($"{webSite.CloudId}/hybridConnectionRelays").ConfigureAwait(false);

                if (!string.IsNullOrWhiteSpace(hybridConnections))
                {
                    var resources = ResourceContainerBuilder.BuildResources(hybridConnections);
                    resourceContainer.Resources.AddRange(resources);
                }
                
            }

            var sqlServers = resourceContainer.Resources
                .Where(resource => resource.ResourceType.Equals(ResourceTypes.SqlServer)).ToList();

            foreach (var sqlServer in sqlServers)
            {
                var firewallRules = await _resourceRepository
                    .GetResourceDefinition($"{sqlServer.CloudId}/firewallRules").ConfigureAwait(false);

                if (!string.IsNullOrWhiteSpace(firewallRules))
                {
                    var resources = ResourceContainerBuilder.BuildResources(firewallRules);
                    foreach (var resource in resources)
                    {
                        resource.ResourceLocation = "NA";
                    }
                    resourceContainer.Resources.AddRange(resources);
                }
            }
        }
    }
}