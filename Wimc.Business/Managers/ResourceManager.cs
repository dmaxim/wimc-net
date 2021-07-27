using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mx.Library.ExceptionHandling;
using Wimc.Business.Builders;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Business.Managers
{
    public class ResourceManager : IResourceManager
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceManager(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }
        
        public async Task<Resource> Get(int resourceId)
        {
            return await _resourceRepository.FindSingleAsync(resource => resource.ResourceId.Equals(resourceId))
                .ConfigureAwait(false);
        }

        public async Task<IList<Resource>> Get(string resourceType)
        {
            return await _resourceRepository.GetAll()
                .Where(resource => resource.ResourceType.Equals(resourceType))
                .ToListAsync();
        }

        public async Task Migrate(int resourceId)
        {
            var resource = await Get(resourceId);

            resource.IsMigrated = true;
            await _resourceRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<string> GetTemplate(string resourceType, string templatePath)
        {
            return await GetTemplateContent(resourceType, templatePath).ConfigureAwait(false);
        }

        public async Task<IList<string>> GetResourceTypes()
        {
            return await _resourceRepository.GetDistinctResources().ConfigureAwait(false);
        }

        public async Task<string> GetResourceDefinition(string resourceId)
        {
            return await _resourceRepository.GetResourceDefinition(resourceId).ConfigureAwait(false);
        }

        public async Task UpdateNotes(int resourceId, string notes)
        {
            var existingResource = await _resourceRepository.FindSingleAsync(resource => resource.ResourceId == resourceId)
                .ConfigureAwait(false);

            if (existingResource == null)
            {
                throw new MxNotFoundException($"Resource with id {resourceId} does not exist");
            }

            existingResource.Notes = notes;
            await _resourceRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UnMigrate(string resourceType)
        {
            var resources = await _resourceRepository.GetAll()
                .Where(resource => resource.ResourceType.Equals(resourceType))
                .ToListAsync()
                .ConfigureAwait(false);

            foreach (var resource in resources)
            {
                resource.IsMigrated = false;
            }

            await _resourceRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Resource> Add(int resourceContainerId, string cloudId)
        {
            var resourceDefinition = await _resourceRepository.GetResourceDefinition(cloudId).ConfigureAwait(false);
            if (resourceDefinition != null)
            {
                var resource = ResourceContainerBuilder.BuildResource(resourceDefinition);
                resource.ResourceContainerId = resourceContainerId;
                
                _resourceRepository.Insert(resource);
                await _resourceRepository.SaveChangesAsync().ConfigureAwait(false);
                return resource;
            }

            return null;
        }

        private async Task<string> GetTemplateContent(string resourceType, string templatePath)
        {
            var contentPath = $"{templatePath}/templates/{resourceType}.tf";
            if (!File.Exists(contentPath))
            {
                return "Does not exist";
            }

            return await File.ReadAllTextAsync(contentPath).ConfigureAwait(false);
        }
    }
}