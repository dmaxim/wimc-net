using System.Collections;
using System.Collections.Generic;


namespace Wimc.Domain.Models
{
    public class ResourceContainer
    {
        public int ResourceContainerId { get; set; }
        public string ContainerName { get; set; }

        public ICollection<Resource> Resources { get; set; }
        
    }
}