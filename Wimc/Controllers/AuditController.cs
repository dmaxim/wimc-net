using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wimc.Business.Managers;
using Wimc.Models.Audit;

namespace Wimc.Controllers
{
    public class AuditController : Controller
    {
        private readonly IAuditResultManager _auditResultManager;

        public AuditController(IAuditResultManager auditResultManager)
        {
            _auditResultManager = auditResultManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var newResources = await _auditResultManager.GetNewResources().ConfigureAwait(false);
            return View(new AuditIndexViewModel(newResources));
        }
    }
}