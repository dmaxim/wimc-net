using System.Collections.Generic;
using System.Threading.Tasks;
using Wimc.Domain.Models;

namespace Wimc.Business.Managers
{
    public interface IResourceManager
    {
        Task<Resource> Get(int resourceId);

        Task<IList<Resource>> Get(string resourceType);

        Task Migrate(int resourceId);

        Task<string> GetTemplate(string resourceType, string templatePath);

        Task<IList<string>> GetResourceTypes();
        
        Task<string> GetResourceDefinition(string resourceId);

        Task UpdateNotes(int resourceId, string notes);

    }
}