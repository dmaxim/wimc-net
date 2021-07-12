using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Mx.Library.ExceptionHandling;
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