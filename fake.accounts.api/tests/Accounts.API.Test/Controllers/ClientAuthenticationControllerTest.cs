using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.API.Controllers;
using Accounts.Core.Handlers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Accounts.API.Test.Controllers
{
    public class ClientAuthenticationControllerTest
    {
        private readonly Mock<IClientAuthorizationHandler> _clientAuthorizationHandlerMock;
        private readonly Mock<ILogger<ClientAuthenticationController>> _loggerMock;
        private readonly ClientAuthenticationController _clientAuthenticationController;

        public ClientAuthenticationControllerTest()
        {
            _clientAuthorizationHandlerMock = new();
            _loggerMock = new();
            _clientAuthenticationController = new ClientAuthenticationController(
                _loggerMock.Object,
                _clientAuthorizationHandlerMock.Object);
        }

        [Fact]
        public void Authentication_OkTokenResponse_WhenAuthenticationAsyncOk()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}