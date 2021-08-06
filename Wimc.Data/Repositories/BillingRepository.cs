using System;
using System.Threading.Tasks;
using Wimc.Domain.Clients;
using Wimc.Domain.Models.CostManagement;
using Wimc.Domain.Repositories;

namespace Wimc.Data.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly IApiClient _apiClient;
        public BillingRepository(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
       public async Task<TimeFrameCost> GetResourceGroupCost(string resourceGroupName, DateTime fromDate, DateTime toDate)
       {
           var query = new BillingQuery(resourceGroupName, new UsageQuery(fromDate, toDate));

            var queryResult = await _apiClient.GetResourceContainerBilling(query).ConfigureAwait(false);

            var billingAmount = new BillingAmount(queryResult);
            return new TimeFrameCost(fromDate, toDate, resourceGroupName, billingAmount.Total); 
              
       }
       
               
       // {
       //   "type": "Usage",
       // "timeframe": "Custom",
       //"timePeriod" : {
       //  "from" : "07/01/2021",
       // "to" : "08/01/2021"
       //},
       //"dataset" : {
       //  "granularity" : "None",
       // "aggregation" : {
       //   "totalCost" : {
       //     "name" : "PreTaxCost",
       //     "function" : "Sum"
       // }
       // }
       // }
       // }
    }
}