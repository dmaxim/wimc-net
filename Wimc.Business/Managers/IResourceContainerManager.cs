using System.Collections.Generic;
using System.Threading.Tasks;
using Wimc.Domain.Models;

namespace Wimc.Business.Managers
{
    public interface IResourceContainerManager
    {
        Task<IList<ResourceContainer>> GetAll();

        Task<ResourceContainer> Get(int resourceContainerId);
    }
}