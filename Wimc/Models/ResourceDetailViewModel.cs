﻿using System.Collections.Generic;

namespace Wimc.Models
{
    public class ResourceDetailViewModel
    {
        public ResourceDetailViewModel(string name, IList<AzureResource> resources)
        {
            Name = name;
            Resources = resources;
        }
        public string Name { get; set; }

        public IList<AzureResource> Resources { get; set; }
    }
}
