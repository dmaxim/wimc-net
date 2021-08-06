namespace Wimc.Domain.Models.CostManagement
{
    public class QueryDataset
    {
        public QueryDataset()
        {
            Granularity = "None";
            Aggregation = new CostAggregation();
        }
        public string Granularity { get; set; }
        public CostAggregation Aggregation { get; set; }
    }
}