using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Wimc.Models
{
    public class NewResourceViewModel
    {
        public string Name { get; set;  }

        public IFormFile ResourceFile { get; set; }
    }
}
