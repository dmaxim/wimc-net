using Wimc.Domain.Models.CostManagement;

namespace Wimc.Models.CostManagement
{
    public class TimeFrameCostViewModel
    {
        public TimeFrameCostViewModel(TimeFrameCost timeFrameCost)
        {
            FromDate = timeFrameCost.FromDate.ToShortDateString();
            ToDate = timeFrameCost.ToDate.ToShortDateString();
            ContainerName = timeFrameCost.ContainerName;
            Cost = timeFrameCost.Cost;
        }
        
        public string FromDate { get; }
        public string ToDate { get;  }
        public string ContainerName { get; }
        public string Cost { get;  }
    }
}