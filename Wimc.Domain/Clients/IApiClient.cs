using System.Net.Http;
using System.Threading.Tasks;

namespace Wimc.Domain.Clients
{
    public interface IApiClient
    {
        HttpClient HttpClient { get; }
        Task<string> GetResourceDefinition(string resourceId);

    }
}