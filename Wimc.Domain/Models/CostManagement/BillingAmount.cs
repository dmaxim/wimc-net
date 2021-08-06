using System.Collections.Generic;
using System.Linq;

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


        public string ParseAmount(IList<IList<string>> rowValues)
        {
            return string.Join(' ', rowValues[0]);
        }
        
    }
}