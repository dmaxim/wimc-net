using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mx.Library.Serialization;
using Wimc.Business.Managers;
using Wimc.Models;

namespace Wimc.Controllers
{
    public class AzureResourcesController : Controller
    {
        private readonly IResourceContainerManager _resourceContainerManager;

        public AzureResourcesController(IResourceContainerManager resourceContainerManager)
        {
            _resourceContainerManager = resourceContainerManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var resources = await _resourceContainerManager.GetAll().ConfigureAwait(false);
            return View(new ResourceContainerIndexModel(resources));
        }

        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> UploadJson(NewResourceViewModel newResourceViewModel)
        {
            var fileContents = await ReadResourceJson(newResourceViewModel.ResourceFile).ConfigureAwait(false);

            var newResourceContainer = await _resourceContainerManager.Create(newResourceViewModel.Name, fileContents);
            
            var model = new ResourceDetailViewModel(newResourceViewModel.Name, newResourceContainer.Resources);
            return View("Detail", model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var resourceContainer = await _resourceContainerManager.GetById(id);

            return View("Detail",
                new ResourceDetailViewModel(resourceContainer.ContainerName, resourceContainer.Resources));
        }
        
        private async Task<string> ReadResourceJson(IFormFile formFile)
        {
            using var reader = new StreamReader(formFile.OpenReadStream());
            var contents = await reader.ReadToEndAsync().ConfigureAwait(false);
            return contents;
        }
    }
}
