using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mx.EntityFramework.Contracts;
using Mx.EntityFramework.Repositories;
using Wimc.Domain.Clients;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Data.Repositories
{
    public class ResourceContainerRepository : Repository<ResourceContainer>, IResourceContainerRepository
    {
        private readonly IApiClient _apiClient;

        public ResourceContainerRepository(IEntityContext entityContext, IApiClient apiClient) : base(entityContext)
        {
            _apiClient = apiClient;
        }
        public async Task<ResourceContainer> Get(int resourceContainerId)
        {
            return await GetAll()
                .Include(resourceContainer => resourceContainer.Resources)
                .FirstOrDefaultAsync(resourceContainer =>
                    resourceContainer.ResourceContainerId.Equals(resourceContainerId));


        }

        public async Task<string> GetDefinition(string resourceContainerName)
        {
            return await _apiClient.GetResourceContainerDefinition(resourceContainerName).ConfigureAwait(false);
        }
    }
}