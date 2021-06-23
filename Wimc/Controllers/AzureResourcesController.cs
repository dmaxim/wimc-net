using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wimc.Models;

namespace Wimc.Controllers
{
    public class AzureResourcesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadJson(NewResourceViewModel newResourceViewModel)
        {
            
            var test = newResourceViewModel.Name;
            var fileContents = await ReadResourceJson(newResourceViewModel.ResourceFile).ConfigureAwait(false);
            var model = new ResourceDetailViewModel(newResourceViewModel.Name,
                fileContents.DeserializeJson<IList<AzureResourceViewModel>>());
            return View("Detail", model);
        }


        private async Task<string> ReadResourceJson(IFormFile formFile)
        {
            using var reader = new StreamReader(formFile.OpenReadStream());
            var contents = await reader.ReadToEndAsync().ConfigureAwait(false);
            return contents;
        }
    }
}
