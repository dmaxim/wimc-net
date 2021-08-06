namespace Wimc.Data.Repositories
{
    public class BillingRepository
    {
        private const string ExampleUrl =
            "https://management.azure.com/subscriptions/bb0c99b7-d44d-413a-b294-564466712637/resourcegroups/MC_rg-mxinfo-kube-prod_mxinfo-kube-prod_eastus2/providers/Microsoft.CostManagement/query?api-version=2019-11-01";
        
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