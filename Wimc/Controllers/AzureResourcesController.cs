﻿using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wimc.Business.Builders;
using Wimc.Business.Managers;
using Wimc.Domain.Models;
using Wimc.Models;
using Wimc.Models.Resources;

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
            
            var model = new ResourceDetailViewModel(newResourceViewModel.Name, fileContents, newResourceContainer.ResourceContainerId, newResourceContainer.Resources);
            
            return View("Detail", model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var resourceContainer = await _resourceContainerManager.GetById(id);

            return View("Detail",
                new ResourceDetailViewModel(resourceContainer.ContainerName, resourceContainer.RawJson,resourceContainer.ResourceContainerId, resourceContainer.Resources));
        }

        public async Task<IActionResult> UnMigrated(int id)
        {
            var resourceContainer = await _resourceContainerManager.GetById(id);
            return View("UnmigratedDetail",
                new ResourceDetailViewModel(resourceContainer.ContainerName, resourceContainer.RawJson,resourceContainer.ResourceContainerId, resourceContainer.Resources));
        }
        
        private static async Task<string> ReadResourceJson(IFormFile formFile)
        {
            using var reader = new StreamReader(formFile.OpenReadStream());
            var contents = await reader.ReadToEndAsync().ConfigureAwait(false);
            return contents;
        }

        [HttpGet]
        public async Task<IActionResult> Migrate(int id, int containerId)
        {
            var resource = await _resourceManager.Get(id).ConfigureAwait(false);

            return View(new AzureResourceViewModel(resource, containerId));
        }


        [HttpPost]
        public async Task<IActionResult> Migrate(AzureResourceViewModel model)
        {
            await _resourceManager.Migrate(model.ResourceId);
            return RedirectToAction("Detail", new {id =  model.ResourceContainerId});
        }

        [HttpGet]
        public async Task<IActionResult> MigrateNew(int id, int containerId)
        {
            var resource = await _resourceManager.Get(id).ConfigureAwait(false);

            return View(new UnMigratedResourceViewModel(resource, containerId));
        }
        
        [HttpPost]
        public async Task<IActionResult> MigrateNew(UnMigratedResourceViewModel model)
        {
            await _resourceManager.Migrate(model.ResourceId);
            return RedirectToAction("UnMigrated", new {id =  model.ResourceContainerId});
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new DeleteResourceContainerViewModel {ResourceContainerId = id});
        }

        [HttpPost]
        public async  Task<IActionResult> ConfirmDelete(DeleteResourceContainerViewModel model)
        {
            await _resourceContainerManager.Delete(model.ResourceContainerId).ConfigureAwait(false);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Inspect()
        {
            return View(new InspectContainerViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Inspect(InspectContainerViewModel model)
        {
            var containerJson = await _resourceContainerManager.GetDefinition(model.ResourceContainerName);
            var newResourceContainer = await _resourceContainerManager.CreateFromDefinition(model.ResourceContainerName, containerJson);
            
            var viewModel = new ResourceDetailViewModel(model.ResourceContainerName, containerJson, newResourceContainer.ResourceContainerId, newResourceContainer.Resources);
            return View("Detail", viewModel);
            
        }

        [HttpGet]
        public async Task<IActionResult> Notes(int id, int containerId)
        {
            var resource = await _resourceManager.Get(id).ConfigureAwait(false);
            return View(new EditResourceNotesViewModel(resource, containerId));
        }

        [HttpPost]
        public async Task<IActionResult> Notes(EditResourceNotesViewModel model)
        {
            await _resourceManager.UpdateNotes(model.ResourceId, model.Notes).ConfigureAwait(false);
            return RedirectToAction("Detail", new {id =  model.ResourceContainerId});
        }

        [HttpGet]
        public async Task<IActionResult> Compare(int id)
        {
            var comparison = await _resourceContainerManager.CompareExistingToRemote(id).ConfigureAwait(false);
            return View(new ResourceComparisonViewModel(comparison));
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id, string cloudId)
        {
            var resourceDefinition = await _resourceManager.GetResourceDefinition(HttpUtility.UrlDecode(cloudId)).ConfigureAwait(false);
            var resource = ResourceContainerBuilder.BuildResource(resourceDefinition);
            resource.ResourceContainerId = id;
            return View(new NewAzureResourceViewModel(resource));
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewAzureResourceViewModel newAzureResourceViewModel)
        {
            _ = await _resourceManager
                .Add(newAzureResourceViewModel.ResourceContainerId, HttpUtility.UrlDecode(newAzureResourceViewModel.CloudId)).ConfigureAwait(false);

            return RedirectToAction("Detail", new {id = newAzureResourceViewModel.ResourceContainerId});
        }

        [HttpGet]
        public async Task<ActionResult> AddNewResources(int id)
        {
            await _resourceContainerManager.AddNewResources(id).ConfigureAwait(false);
            return RedirectToAction("Detail", new {id = id});
        }
        
        [HttpGet]
        public IActionResult DeleteResource(int id, int containerId)
        {
            return View(new DeleteResourceViewModel {ResourceId = id, ResourceContainerId = containerId});
        }

        [HttpPost]
        public async  Task<IActionResult> ConfirmResourceDelete(DeleteResourceViewModel model)
        {
            await _resourceManager.Delete(model.ResourceId).ConfigureAwait(false);

            return RedirectToAction("Detail", new {id = model.ResourceContainerId});
        }

    }
}
