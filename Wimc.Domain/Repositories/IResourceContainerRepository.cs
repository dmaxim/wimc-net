using System.Threading.Tasks;
using Mx.EntityFramework.Contracts;
using Wimc.Domain.Models;

namespace Wimc.Domain.Repositories
{
    public interface IResourceContainerRepository : IRepository<ResourceContainer>
    {
        Task<ResourceContainer> Get(int resourceContainerId);

        Task<string> GetDefinition(string resourceContainerName);
    }
}