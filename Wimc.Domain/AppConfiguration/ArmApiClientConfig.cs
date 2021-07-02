namespace Wimc.Domain.AppConfiguration
{
    public class ArmApiClientConfig
    {
        public string Instance { get; set; }
        public string Domain { get; set;  }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set;  }
        public string SubscriptionId { get; set; }
        public string Resource { get; set; }
        public string BaseUri { get; set; }
        public string ApiVersion { get; set; }
    }
}