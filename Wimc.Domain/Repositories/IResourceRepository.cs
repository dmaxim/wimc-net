using System.Collections.Generic;
using System.Threading.Tasks;
using Mx.EntityFramework.Contracts;
using Wimc.Domain.Models;

namespace Wimc.Domain.Repositories
{
    public interface IResourceRepository : IRepository<Resource>
    {
        Task<IList<string>> GetDistinctResources();
    }
}