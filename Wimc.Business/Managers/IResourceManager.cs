using System.Threading.Tasks;
using Wimc.Domain.Models;

namespace Wimc.Business.Managers
{
    public interface IResourceManager
    {
        Task<Resource> Get(int resourceId);

        Task Migrate(int resourceId);
    }
}