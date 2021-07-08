using System.Collections.Generic;
using System.Linq;

namespace Wimc.Models
{
    public class ContainerMigrationStatusViewModel
    {
        public ContainerMigrationStatusViewModel(IList<ContainerMigrationViewModel> containers)
        {
            if (containers == null)
            {
                return;
            }
            ResourceCount = containers.Sum(container => container.ResourceCount);
            MigratedCount = containers.Sum(container => container.MigratedCount);
            MigrationPercent = CalculateMigrationPercent();
        }
        
        public double ResourceCount { get; }
        
        public double MigratedCount { get;  }
        
        public double MigrationPercent { get; }


        private double CalculateMigrationPercent()
        {
            if (ResourceCount > 0 && MigratedCount > 0)
            {
                return (MigratedCount / ResourceCount) * 100d;
            }
            else
            {
                return 0;
            }
            
        }
        
    }
}