namespace Wimc.Domain.Models
{
    public class AzureResource
    {
        public string Id { get; set; }
        public string Location { get; set;  }
        public string Name { get; set;  }
        public string Type { get; set; }
    
        //public Sku Sku { get; set; }

        //public Identity Identity { get; set; }
        public string Notes { get; set;  }


    }
}