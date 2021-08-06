using System;

namespace Wimc.Domain.Models.CostManagement
{
    public class UsageQuery
    {
        public UsageQuery(DateTime from, DateTime to)
        {
            TimePeriod = new TimePeriod(from, to);
            Dataset = new QueryDataset();
        }
        
        public string Type => "Usage";
        public string Timeframe => "Custom";
        public TimePeriod TimePeriod { get; set; }
        public QueryDataset Dataset { get; set; }
    
    }
}