namespace Wimc.Domain.Models
{
    public class EditResourceContainer
    {
        public EditResourceContainer(int resourceContainerId, string containerName)
        {
            ResourceContainerId = resourceContainerId;
            ResourceContainerName = containerName;
        }
        
        public int ResourceContainerId { get; }
        
        public string ResourceContainerName { get; }
    }
}