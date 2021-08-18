using System.Collections.Generic;

namespace Wimc.Domain.Models.CostManagement
{
    public class BillingAmount
    {
        public BillingAmount(QueryResult queryResult)
        {
            if (queryResult == null)
            {
                Total = "0.00";
            }
            else
            {
                Total = ParseAmount(queryResult.Properties.Rows);
            }
            
        }
        
        public string Total { get; }


        private static string ParseAmount(IList<IList<string>> rowValues)
        {
            return string.Join(' ', rowValues[0]);
        }
        
    }
}