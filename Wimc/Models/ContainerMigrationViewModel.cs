using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class ContainerMigrationViewModel
    {
        public ContainerMigrationViewModel(ResourceContainer resourceContainer)
        {
            ContainerName = resourceContainer.ContainerName;
            ResourceCount = resourceContainer.Resources.Count;
            MigratedCount = resourceContainer.Resources.Count(resource => resource.IsMigrated);

        }
        
        public string ContainerName { get;  }
        
        public double ResourceCount { get; }
        
        public double MigratedCount { get; }

        public double MigrationPercent
        {
            get
            {
                if (ResourceCount > 0 && MigratedCount > 0)
                {
                    return (MigratedCount / ResourceCount) * 100.00d;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}