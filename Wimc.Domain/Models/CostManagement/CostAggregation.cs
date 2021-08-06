namespace Wimc.Domain.Models.CostManagement
{
    public class CostAggregation
    {
        public CostAggregation()
        {
            TotalCost = new TotalCost();
        }
        public TotalCost TotalCost { get; set; }
    }
}