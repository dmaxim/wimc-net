using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wimc.Business.Managers;

namespace Wimc.Controllers
{
    public class CostManagementController : Controller
    {
        private readonly ICostManagementManager _costManagementManager;
        
        public CostManagementController(ICostManagementManager costManagementManager)
        {
            _costManagementManager = costManagementManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var resourceCost = await _costManagementManager.GetMonthlyCost(id).ConfigureAwait(false);
            return View(resourceCost);
        }
    }
}