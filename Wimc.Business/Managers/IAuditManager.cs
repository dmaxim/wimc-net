using System.Threading.Tasks;

namespace Wimc.Business.Managers
{
    public interface IAuditManager
    {
        Task Generate();
    }
}