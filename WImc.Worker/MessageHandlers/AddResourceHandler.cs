using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using Wimc.Business.Managers;
using Wimc.Domain.Messages.Commands;

namespace WImc.Worker.MessageHandlers
{
    public class AddResourceHandler : IHandleMessages<AddResource>
    {
        private readonly IResourceManager _resourceManager;
        private readonly ILogger<AddResourceHandler> _logger;

        public AddResourceHandler(IResourceManager resourceManager, ILogger<AddResourceHandler> logger)
        {
            _resourceManager = resourceManager;
            _logger = logger;
        }
        
        public async Task Handle(AddResource message)
        {
            _logger.LogWarning("Handling message");
            await _resourceManager.Add(message.ResourceContainerId, message.CloudId).ConfigureAwait(false);
            _logger.LogWarning("Message Handled");
        }
    }
}