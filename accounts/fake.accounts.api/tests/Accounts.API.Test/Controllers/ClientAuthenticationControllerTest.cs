using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.API.Controllers;
using Accounts.API.Test.Fakes;
using Accounts.Application.Exceptions;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;
using Accounts.Core.Handlers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task Authentication_ReturnTokenResponse_WhenAuthenticationAsyncOk()
        {
            //Arrange
            var tokenResponse = new TokenResponse();
            tokenResponse.Token = "tokenResponseTeste";
            _clientAuthorizationHandlerMock
                .Setup(s => s.AuthenticationAsync(It.IsAny<ClientLoginRequest>()))
                .ReturnsAsync(tokenResponse);

            //Act
            var result = (ObjectResult) await _clientAuthenticationController.AuthenticationAsync(new ClientLoginRequest());

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(tokenResponse);
            _clientAuthorizationHandlerMock.Verify(v => v.AuthenticationAsync(It.IsAny<ClientLoginRequest>()), Times.Once);
        }

        [Fact]
        public async void Authentication_ReturnBadRequest_WhenHandlerAuthenticationAsyncAuthenticationException()
        {
            //Arrange
            var genericFakeException = new AuthenticationException("test_authentication_exception"); 
            _clientAuthorizationHandlerMock
                .Setup(s => s.AuthenticationAsync(It.IsAny<ClientLoginRequest>()))
                .ThrowsAsync(genericFakeException);

            //Act
            var result = (ObjectResult) await _clientAuthenticationController.AuthenticationAsync(new ClientLoginRequest());

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            var problemDetails = (ProblemDetails) result.Value;
            problemDetails?.Detail.Should().Be(genericFakeException.Message);
        }

        [Fact]
        public async void Authentication_ReturnProblemInternalServerError_WhenHandlerAuthenticationAsyncFakeException()
        {
            //Arrange
            var genericFakeException = new GenericFakeException(); 
            _clientAuthorizationHandlerMock
                .Setup(s => s.AuthenticationAsync(It.IsAny<ClientLoginRequest>()))
                .ThrowsAsync(genericFakeException);

            //Act
            var result = (ObjectResult) await _clientAuthenticationController.AuthenticationAsync(new ClientLoginRequest());

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            var problemDetails = (ProblemDetails) result.Value;
            problemDetails?.Detail.Should().Be(genericFakeException.Message);
        }
    }
}