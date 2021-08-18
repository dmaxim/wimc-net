using Microsoft.AspNetCore.Http;

namespace Wimc.Models
{
    public class NewResourceViewModel
    {
        public string Name { get; set;  }

        public IFormFile ResourceFile { get; set; }
    }
}
