using System.Collections.Generic;


namespace Wimc.Domain.Models
{
    public class ResourceContainer
    {
        public int ResourceContainerId { get; set; }
        public string ContainerName { get; set; }

        public List<Resource> Resources { get; set; }
        
        public string RawJson { get; set; }
        
    }
}