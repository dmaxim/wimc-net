using System.Threading.Tasks;
using Wimc.Domain.Models.CostManagement;

namespace Wimc.Business.Managers
{
    public interface ICostManagementManager
    {
        Task<TimeFrameCost> GetMonthlyCost(int resourceContainerId);
    }
}