using System.Collections.Generic;
using System.Threading.Tasks;
using Wimc.Domain.Models;
using Wimc.Domain.Repositories;

namespace Wimc.Business.Managers
{
    public class AuditResultManager : IAuditResultManager
    {
        private readonly IAuditRepository _auditRepository;

        public AuditResultManager(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }
        
        public async Task<IList<Resource>> GetNewResources()
        {
            return await _auditRepository.GetNewResources().ConfigureAwait(false);
        }
    }
}