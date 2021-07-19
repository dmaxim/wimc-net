using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Wimc.Business.Managers;
using Wimc.Models;

namespace Wimc.Controllers
{
    public class ResourceTypesController : Controller
    {

        private readonly IResourceManager _resourceManager;
        
        public ResourceTypesController(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resourceTypes = await _resourceManager.GetResourceTypes().ConfigureAwait(false);
            return View(new ResourceTypesViewModel(resourceTypes));
        }

        [HttpGet]
        public async Task<IActionResult> Resources(string id)
        {
            var resourceType = HttpUtility.UrlDecode(id);

            var resources = await _resourceManager.Get(resourceType).ConfigureAwait(false);
            return View(new ResourceListViewModel(resourceType, resources));
        }
     
        [HttpGet]
        public async Task<IActionResult> Migrate(int id)
        {
            var resource = await _resourceManager.Get(id).ConfigureAwait(false);

            return View(new ResourceTypeResourceViewModel(resource));
        }


        [HttpPost]
        public async Task<IActionResult> Migrate(int id,[FromForm] ResourceTypeResourceViewModel model)
        {
            await _resourceManager.Migrate(model.ResourceId);
            return RedirectToAction("Resources", new {id = HttpUtility.UrlEncode(model.Type)});
        }
        
        [HttpGet]
        public IActionResult UnMigrate(string id)
        {
            return View(new UnMigrateViewModel(HttpUtility.UrlDecode(id)));
        }

        [HttpPost]
        public async Task<IActionResult> UnMigrate(UnMigrateViewModel model)
        {
            await _resourceManager.UnMigrate(model.ResourceType).ConfigureAwait(false);
            return RedirectToAction("Resources", new {id = HttpUtility.UrlEncode(model.ResourceType)});
        }
        
    }
}