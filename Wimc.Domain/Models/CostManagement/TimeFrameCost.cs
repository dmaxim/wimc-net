using System;

namespace Wimc.Domain.Models.CostManagement
{
    public class TimeFrameCost
    {
        public TimeFrameCost(DateTime fromDate, DateTime toDate, string containerName, string cost)
        {
            FromDate = fromDate;
            ToDate = toDate;
            ContainerName = containerName;
            Cost = cost;

        }
        
        public DateTime FromDate { get;  }
        public DateTime ToDate { get;  }
        public string ContainerName { get;   }
        public string Cost { get;  }
    }
}