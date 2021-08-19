using System.Collections.Generic;
using System.Threading.Tasks;
using Mx.EntityFramework.Contracts;
using Wimc.Domain.Models;

namespace Wimc.Domain.Repositories
{
    public interface IResourceContainerRepository : IRepository<ResourceContainer>
    {
        Task<ResourceContainer> Get(int resourceContainerId);

        Task<string> GetDefinition(string resourceContainerName);
    
        /// <summary>
        /// Return a list of resource containers for the list of ids
        /// </summary>
        /// <param name="resourceContainerIds"></param>
        /// <returns></returns>
        Task<IList<ResourceContainer>> GetContainers(IList<int> resourceContainerIds);
    }
}