namespace Wimc.Models
{
    public class UnMigrateViewModel
    {
        public UnMigrateViewModel(string resourceType)
        {
            ResourceType = resourceType;
        }
        public UnMigrateViewModel() {}
        public string ResourceType { get; set; }
    }
}