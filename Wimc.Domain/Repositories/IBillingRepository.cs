using System;
using System.Threading.Tasks;
using Wimc.Domain.Models.CostManagement;

namespace Wimc.Domain.Repositories
{
    public interface IBillingRepository
    {
        Task<TimeFrameCost> GetResourceGroupCost(string resourceGroupName, DateTime from, DateTime to);
    }
}