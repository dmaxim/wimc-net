using System.Threading.Tasks;

namespace Wimc.Business.Managers
{
    public interface IResourceQueryManager
    {
         Task<string> GetResource(string resourceId, string apiVersion);
    }
}