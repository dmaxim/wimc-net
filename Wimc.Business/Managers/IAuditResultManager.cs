using System.Collections.Generic;
using System.Threading.Tasks;
using Wimc.Domain.Models;

namespace Wimc.Business.Managers
{
    public interface IAuditResultManager
    {
        Task<IList<ResourceContainer>> GetNewResources();
    }
}