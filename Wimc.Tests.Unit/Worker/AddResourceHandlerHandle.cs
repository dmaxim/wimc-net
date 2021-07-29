using System;
using System.Collections.Generic;
using Moq;
using Rebus.Bus;
using Wimc.Domain.Messages.Commands;
using Xunit;

namespace Wimc.Tests.Unit.Worker
{
    public class AddResourceHandlerHandle
    {
        [Fact]
        public void Should_Do_Something()
        {

            var mockBus = new Mock<IBus>();
            mockBus.Setup(bus => bus.Send(It.IsAny<AddResource>(), new Dictionary<string, string>()))
                .ThrowsAsync(new Exception("Test Exception"));
        }
        
    }
}