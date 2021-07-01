using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wimc.Business.Managers;
using Wimc.Models;

namespace Wimc.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly IResourceManager _resourceManager;
        
        public TemplatesController(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }
        
        public async Task<IActionResult> Template(int id)
        {
            var resource = await _resourceManager.Get(id).ConfigureAwait(false);
            var template = await _resourceManager.GetTemplate(resource.ResourceType).ConfigureAwait(false);
            return View(new TemplateViewModel(resource.ResourceType, template));
        }
    }
}