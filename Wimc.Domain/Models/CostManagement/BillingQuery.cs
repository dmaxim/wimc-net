namespace Wimc.Domain.Models.CostManagement
{
    public class BillingQuery
    {
        public BillingQuery(string containerName, UsageQuery query)
        {
            ContainerName = containerName;
            Query = query;
        }
        public string ContainerName { get;  }
        public UsageQuery Query { get;  }
        
    }
}