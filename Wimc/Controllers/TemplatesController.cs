using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Wimc.Business.Managers;
using Wimc.Models;

namespace Wimc.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly IResourceManager _resourceManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IResourceContainerManager _resourceContainerManager;
        public TemplatesController(IResourceManager resourceManager, IResourceContainerManager resourceContainerManager, IWebHostEnvironment webHostEnvironment)
        {
            _resourceManager = resourceManager;
            _webHostEnvironment = webHostEnvironment;
            _resourceContainerManager = resourceContainerManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> Template(int id)
        {
            var resource = await _resourceManager.Get(id).ConfigureAwait(false);
            var template = await _resourceManager.GetTemplate(resource.ResourceType, _webHostEnvironment.WebRootPath).ConfigureAwait(false);
            var resourceDefinition =
                await _resourceManager.GetResourceDefinition(resource.CloudId).ConfigureAwait(false);
            return View(new TemplateViewModel(resource, template, resourceDefinition));
        }

        [HttpGet]
        public async Task<IActionResult> ResourceTypes()
        {
            var resourceTypes = await _resourceManager.GetResourceTypes().ConfigureAwait(false);
            return View(new ResourceTypesViewModel(resourceTypes));
        }

        [HttpGet]
        public async Task<IActionResult> ContainerTemplate(int id)
        {
            var resourceContainer = await _resourceContainerManager.Get(id).ConfigureAwait(false);

            var templates = new List<ResourceTemplateViewModel>();
            foreach (var resource in resourceContainer.Resources)
            {
                var template = await _resourceManager.GetTemplate(resource.ResourceType, _webHostEnvironment.WebRootPath)
                    .ConfigureAwait(false);
                
                templates.Add(new ResourceTemplateViewModel(resource.ResourceName, template));
            }

            return View(new ContainerTemplateViewModel(resourceContainer.ContainerName, templates));

        }
    }
}