using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rebus.Extensions;
using Rebus.Handlers;
using Rebus.Pipeline;
using Wimc.Business.Managers;
using Wimc.Domain.Messages.Commands;

namespace WImc.Worker.MessageHandlers
{
    public class AddResourceHandler : IHandleMessages<AddResource>
    {
        private readonly IResourceManager _resourceManager;
        private readonly ILogger<AddResourceHandler> _logger;
        private readonly IMessageContext _messageContext;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        public AddResourceHandler(IResourceManager resourceManager, ILogger<AddResourceHandler> logger, IMessageContext messageContext, IHostApplicationLifetime hostApplicationLifetime)
        {
            _resourceManager = resourceManager;
            _logger = logger;
            _messageContext = messageContext;
            _hostApplicationLifetime = hostApplicationLifetime;
        }
        
        public async Task Handle(AddResource message)
        {
            _logger.LogInformation("Handling message");
            //var cancellationToken = _messageContext.GetCancellationToken(); TODO:  Update Add Method to accept a cancellation token
            //try
            //{
                await _resourceManager.Add(message.ResourceContainerId, message.CloudId).ConfigureAwait(false);
            //}
            //catch (Exception e)
            //{
                // Depending on the exception type could shutdown the handler so it gets recycled
                //_hostApplicationLifetime.StopApplication();
              //  throw;
           // }
            
            _logger.LogInformation("Message Handled");
        }
    }
}