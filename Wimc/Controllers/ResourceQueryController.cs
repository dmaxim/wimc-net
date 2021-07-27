using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wimc.Business.Managers;
using Wimc.Models;

namespace Wimc.Controllers
{
    public class ResourceQueryController : Controller
    {
        private readonly IResourceQueryManager _resourceQueryManager;

        public ResourceQueryController(IResourceQueryManager resourceQueryManager)
        {
            _resourceQueryManager = resourceQueryManager;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View(new ResourceQueryViewModel
            {
                ApiVersion = "2021-05-01"
            });
            
        }

        [HttpPost]
        public async Task<IActionResult> Index(ResourceQueryViewModel model)
        {
            model.Result = await _resourceQueryManager.GetResource(model.Uri, model.ApiVersion);
            return View(model);
        }
    }
}