using System.Collections.Generic;
using System.Threading.Tasks;
using Wimc.Domain.Models;

namespace Wimc.Domain.Repositories
{
    public interface IAuditRepository
    {
        Task<List<Resource>> GetNewResources();
    }
}