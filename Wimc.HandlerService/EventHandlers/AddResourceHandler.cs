using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using Wimc.Business.Managers;
using Wimc.Domain.Messages.Commands;

namespace Wimc.HandlerService.EventHandlers
{
    public class AddResourceHandler : IHandleMessages<AddResource>
    {
        private readonly IResourceManager _resourceManager;
       
        public AddResourceHandler(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }
        
        public async Task Handle(AddResource message)
        {
            await _resourceManager.Add(message.ResourceContainerId, message.CloudId).ConfigureAwait(false);
        }
    }
}