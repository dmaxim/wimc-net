using System.Threading.Tasks;
using Wimc.Domain.Repositories;

namespace Wimc.Business.Managers
{
    public class ResourceQueryManager : IResourceQueryManager
    {
        private readonly IResourceRepository _resourceRepository;
        
        public ResourceQueryManager(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }
        public async Task<string> GetResource(string resourceId, string apiVersion)
        {
            return await _resourceRepository.GetResource(resourceId, apiVersion).ConfigureAwait(false);
        }
    }
}