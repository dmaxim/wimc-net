using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wimc.Business.Managers;
using Wimc.Models;

namespace Wimc.Controllers
{
    public class MigrationController : Controller
    {
        private readonly IResourceContainerManager _resourceContainerManager;
        
        public MigrationController(IResourceContainerManager resourceContainerManager)
        {
            _resourceContainerManager = resourceContainerManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resourceContainers = await _resourceContainerManager.GetAllWithResources().ConfigureAwait(false);
            return View(new ContainerMigrationsViewModel(resourceContainers));
        }
    }
}