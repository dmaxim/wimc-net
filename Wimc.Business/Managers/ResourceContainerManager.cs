using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
    }
}