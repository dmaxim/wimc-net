namespace Wimc.Domain.Models
{
    public class Resource
    {
        public int ResourceId { get; set; }
        
        public int ResourceContainerId { get; set; }
        
        public string ResourceName { get; set; }
        
        public string ResourceType { get; set; }
        
        public string CloudId { get; set; }
        
        public string ResourceDefinition { get; set; }
        
    }
}