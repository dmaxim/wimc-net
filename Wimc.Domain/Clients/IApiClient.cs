using System.Threading.Tasks;

namespace Wimc.Domain.Clients
{
    public interface IApiClient
    {
        Task<string> GetResourceDefinition(string resourceId);

    }
}