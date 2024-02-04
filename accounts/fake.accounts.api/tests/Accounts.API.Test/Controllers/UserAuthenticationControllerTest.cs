using System.Threading.Tasks;
using Accounts.API.Controllers;
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
    public class UserAuthenticationControllerTest
    {
        private readonly Mock<ILogger<UserAuthenticationController>> _loggerMock;
        private readonly Mock<IUserAuthorizationHandler> _userAuthorizationHandlerMock;
        public readonly UserAuthenticationController _userAuthenticationController;

        public UserAuthenticationControllerTest()
        {
            _loggerMock = new();
            _userAuthorizationHandlerMock = new();

            _userAuthenticationController =new UserAuthenticationController(
                _userAuthorizationHandlerMock.Object, 
                _loggerMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ReturnAppTokenResponse_WhenAuthenticationAsyncOk()
        {
            //Arrange
            var appTokenResponse = new AppTokenResponse{
                Token  = new TokenResponse{
                    Token = "tokenResponseTeste"
                }
            };

            _userAuthorizationHandlerMock
                .Setup(s => s.AuthenticationAsync(It.IsAny<LoginRequest>()))
                .ReturnsAsync(appTokenResponse);

            //Act
            var result = (ObjectResult) await _userAuthenticationController.LoginAsync(new LoginRequest());

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(appTokenResponse);
            _userAuthorizationHandlerMock.Verify(v => v.AuthenticationAsync(It.IsAny<LoginRequest>()), Times.Once);
        }

        [Fact]
        public async void LoginAsync_ReturnBadRequest_WhenHandlerAuthenticationAsyncAuthenticationException()
        {
            //Arrange
            var genericFakeException = new AuthenticationException("test_authentication_exception"); 
            _userAuthorizationHandlerMock
                .Setup(s => s.AuthenticationAsync(It.IsAny<LoginRequest>()))
                .ThrowsAsync(genericFakeException);

            //Act
            var result = (ObjectResult) await _userAuthenticationController.LoginAsync(new LoginRequest());

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            var problemDetails = (ProblemDetails) result.Value;
            problemDetails?.Detail.Should().Be(genericFakeException.Message);
        }
    }
}