using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.API.Controllers;
using Accounts.API.Test.Fakes;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses.User;
using Accounts.Core.Handlers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Accounts.API.Test.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly Mock<IUserHandler> _userHandlerMock;
        private readonly UserController _userController;

        public UserControllerTest(){
            _loggerMock = new();
            _userHandlerMock = new();

            _userController = new UserController(_loggerMock.Object,_userHandlerMock.Object);
        }
        
        [Fact]
        public async void IndexGet_OKUserPaginationResponse_WhenHandlerGetPaginationOk()
        {
            //Arrange
            var userPaginationResponse = new UserPaginationResponse();
            _userHandlerMock
                .Setup(s => s.GetPaginationAsync(It.IsAny<PaginationRequest>()))
                .ReturnsAsync(userPaginationResponse);

            //Act
            var result = (ObjectResult) await _userController.GetAsync(new PaginationRequest());

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(userPaginationResponse);
            _userHandlerMock.Verify(v => v.GetPaginationAsync(It.IsAny<PaginationRequest>()), Times.Once);
        }

        [Fact]
        public async void IndexGet_Problem500_WhenHandlerGetPaginationGenericFakeException()
        {
            //Arrange
            var genericFakeException = new GenericFakeException(); 
            _userHandlerMock
                .Setup(s => s.GetPaginationAsync(It.IsAny<PaginationRequest>()))
                .ThrowsAsync(genericFakeException);

            //Act
            var result = (ObjectResult) await _userController.GetAsync(new PaginationRequest());

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            var problemDetails = (ProblemDetails) result.Value;
            problemDetails?.Detail.Should().Be(genericFakeException.Message);
        }
    }
}