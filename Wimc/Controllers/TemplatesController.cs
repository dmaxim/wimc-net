using System;
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
        public TemplatesController(IResourceManager resourceManager, IWebHostEnvironment webHostEnvironment)
        {
            _resourceManager = resourceManager;
            _webHostEnvironment = webHostEnvironment;
        }
        
        [HttpGet]
        public async Task<IActionResult> Template(int id)
        {
            var resource = await _resourceManager.Get(id).ConfigureAwait(false);
            var template = await _resourceManager.GetTemplate(resource.ResourceType, _webHostEnvironment.WebRootPath).ConfigureAwait(false);
            return View(new TemplateViewModel(resource.ResourceType, template));
        }

        [HttpGet]
        public async Task<IActionResult> ResourceTypes()
        {
            var resourceTypes = await _resourceManager.GetResourceTypes().ConfigureAwait(false);
            return View(new ResourceTypesViewModel(resourceTypes));
        }
        
    }
}