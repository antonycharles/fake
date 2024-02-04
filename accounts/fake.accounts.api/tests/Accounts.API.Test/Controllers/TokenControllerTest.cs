using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.API.Controllers;
using Accounts.API.Test.Fakes;
using Accounts.Core.Handlers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace Accounts.API.Test.Controllers
{
    public class TokenControllerTest
    {
        private readonly Mock<ITokenKeyHandler> _tokenKeyHandlerMock;
        private readonly Mock<ILogger<TokenController>> _loggerMock;
        private readonly TokenController _tokenController;

        public TokenControllerTest()
        {
            _tokenKeyHandlerMock = new();
            _loggerMock = new();
            _tokenController = new TokenController(_tokenKeyHandlerMock.Object, _loggerMock.Object);
        }
        
        [Fact]
        public void IndexGet_ReturnJsonWebKeyResponse_WhenTokenHandlerGetPublicKeyOk()
        {
            //Arrange
            IList<JsonWebKey> jsonWebKey = new List<JsonWebKey>(){
                new JsonWebKey() 
            };
            _tokenKeyHandlerMock
                .Setup(s => s.GetPublicKey())
                .Returns(jsonWebKey);


            //Act
            var result = (ObjectResult)  _tokenController.Get();

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(jsonWebKey);
            _tokenKeyHandlerMock.Verify(v => v.GetPublicKey(), Times.Once);
        }

        [Fact]
        public void IndexGet_ReturnProblemInternalServerError_WhenHandlerGetPublicKeyReturnGenericFakeException()
        {
            //Arrange
            var genericFakeException = new GenericFakeException(); 
            _tokenKeyHandlerMock
                .Setup(s => s.GetPublicKey())
                .Throws(genericFakeException);

            //Act
            var result = (ObjectResult) _tokenController.Get();

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            var problemDetails = (ProblemDetails) result.Value;
            problemDetails?.Detail.Should().Be(genericFakeException.Message);
        }
    }
}