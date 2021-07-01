namespace Wimc.Models
{
    public class ResourceTypeViewModel
    {
        public ResourceTypeViewModel(string resourceType)
        {
            ResourceType = resourceType;
        }

        public string ResourceType { get; }
    }
}