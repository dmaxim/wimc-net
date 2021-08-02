using System.Security.Cryptography;
using System.Text;


namespace Wimc.Audit.EventHandling
{
    public class ResourceAdded
    {
        public int ResourceContainerId { get; set; }
        public string CloudId { get; set; }
        public string Name { get; set; }

        public string RowKey
        {
            get
            {
                using var hashAlgorithm = MD5.Create();
                var hashBytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(this.CloudId));
                var hashOutput = new StringBuilder(50);
                foreach (var byteValue in hashBytes)
                {
                    hashOutput.Append(byteValue.ToString("x2"));
                }

                return hashOutput.ToString();
            }
        }
    }
}