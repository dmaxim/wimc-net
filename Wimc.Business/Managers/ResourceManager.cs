using System.Threading.Tasks;
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


    }
}