using System;

namespace Wimc.Infrastructure.Configuration
{
    public class WimcUIConfiguration
    {
        public string DatabaseConnection { get; set; }

        public void ThrowIfInvalid()
        {
            if (string.IsNullOrWhiteSpace(DatabaseConnection))
            {
                throw new ArgumentNullException(nameof(DatabaseConnection));
            }
        }
        
    }
}