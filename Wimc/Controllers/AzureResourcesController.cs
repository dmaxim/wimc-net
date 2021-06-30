using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wimc.Business.Managers;
using Wimc.Domain.Models;
using Wimc.Models;

namespace Wimc.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class AzureResourcesController : Controller
    {
        private readonly IResourceContainerManager _resourceContainerManager;
        private readonly IResourceManager _resourceManager;

        public AzureResourcesController(IResourceContainerManager resourceContainerManager, IResourceManager resourceManager)
        {
            _resourceContainerManager = resourceContainerManager;
            _resourceManager = resourceManager;
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

        [HttpGet]
        public async Task<IActionResult> Migrate(int id)
        {
            var resource = await _resourceManager.Get(id).ConfigureAwait(false);

            return View(new AzureResourceViewModel(resource));
        }


        [HttpPost]
        public async Task<IActionResult> Migrate(AzureResourceViewModel model)
        {
            await _resourceManager.Migrate(model.ResourceId);
            return RedirectToAction("Detail", new {id = model.Id});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var resource = await _resourceContainerManager.Get(id).ConfigureAwait(false);
            return View(new EditResourceContainerViewModel(resource));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditResourceContainerViewModel model)
        {
            await _resourceContainerManager.Edit(new EditResourceContainer(model.ResourceContainerId, model.Name)).ConfigureAwait(false);
            return RedirectToAction("Index");
        }
    }
}
