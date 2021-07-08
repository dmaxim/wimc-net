using System.Collections.Generic;
using System.Linq;
using Wimc.Domain.Models;

namespace Wimc.Models
{
    public class ContainerMigrationsViewModel
    {
        public ContainerMigrationsViewModel(IList<ResourceContainer> resourceContainers)
        {
            if (resourceContainers == null)
            {
                Containers = new List<ContainerMigrationViewModel>();
            }
            else
            {
                Containers = resourceContainers.Select(container => new ContainerMigrationViewModel(container))
                    .ToList();
            }

            MigrationStatus = new ContainerMigrationStatusViewModel(Containers);
        }
        
        public IList<ContainerMigrationViewModel> Containers { get; }
        
        public ContainerMigrationStatusViewModel MigrationStatus { get; }
    }
}