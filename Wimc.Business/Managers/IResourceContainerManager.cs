using System.Collections.Generic;
using System.Threading.Tasks;
using Wimc.Domain.Models;

namespace Wimc.Business.Managers
{
    public interface IResourceContainerManager
    {
        Task<IList<ResourceContainer>> GetAll();

        Task<ResourceContainer> Get(int resourceContainerId);

        Task<ResourceContainer> Create(string name, string containerJson);

        Task<ResourceContainer> CreateFromDefinition(string name, string containerJson);

        Task<ResourceContainer> GetById(int resourceContainerId);
        
        Task Edit(EditResourceContainer editResourceContainer);

        Task Delete(int resourceContainerId);

        Task<string> GetDefinition(string resourceContainerName);
    }
}