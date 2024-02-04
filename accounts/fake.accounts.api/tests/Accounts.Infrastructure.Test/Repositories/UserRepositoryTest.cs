using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Infrastructure.Data;
using Accounts.Infrastructure.Repositories;
using Accounts.Infrastructure.Test.Fakes;
using Xunit;

namespace Accounts.Infrastructure.Test.Repositories
{
    public class UserRepositoryTest
    {
        private readonly AccountsContext _accountsContextMock;
        private readonly UserRepository _userRepository;

        public UserRepositoryTest()
        {
            _accountsContextMock = DatabaseContextFake.Create();
            _userRepository = new UserRepository(_accountsContextMock);
        }

        [Fact]
        public async void GetByEmailAsync_ReturnUser_WhenUserExistInTheDatabase()
        {
            //Arrange
            var userFake = UserFake.Create().Generate(10);
            var userTest = userFake.First();

            _accountsContextMock.Users.AddRange(userFake);
            await _accountsContextMock.SaveChangesAsync();

            //Act
            var result = await _userRepository.GetByEmailAsync(userTest.Email);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(userTest.Email, result.Email);
        }

        [Fact]
        public async void GetByEmailAsync_ReturnNull_WhenUserNotExistInTheDatabase()
        {
            //Arrange
            var userFake = UserFake.Create().Generate(1).First();
            userFake.Email = "andre.test@gmail.com";

            _accountsContextMock.Users.Add(userFake);
            await _accountsContextMock.SaveChangesAsync();

            //Act
            var result = await _userRepository.GetByEmailAsync("camila.test@gmail.com");

            //Assert
            Assert.Null(result);
            
        }

        [Theory]
        [InlineData(20,1,10,10)]
        [InlineData(15,2,10,5)]
        [InlineData(20,3,10,0)]
        [InlineData(0,1,10,0)]
        public async void GetPaginationAsync_ReturnUsers_WhenPositionAndTakeValid(
            int totalUser, int position,int take, int expectedResult)
        {
            //Arrange
            var usersFake = UserFake.Create().Generate(totalUser);

            _accountsContextMock.AddRange(usersFake);
            await _accountsContextMock.SaveChangesAsync();

            //Act
            var result = await _userRepository.GetPaginationAsync(null,position,take);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult,result.Count());
        }

        [Theory]
        [InlineData("camila","andre.test","bruno.test",0)]
        [InlineData("andre","andre.test","bruno.test",1)]
        [InlineData("test","andre.test","bruno.test",2)]
        public async void GetPaginationAsync_ReturnUsers_WhenSearchValid(
            string search, string userOne, string userTwo, int expectedResult)
        {
            //Arrange
            var usersFake = UserFake.Create().Generate(20);
            _accountsContextMock.AddRange(usersFake);

            var userOneFake = UserFake.Create().Generate(1).First();
            userOneFake.Name = userOne;
            userOneFake.Email = $"{userOne}@email.com";
            _accountsContextMock.Add(userOneFake);

            var userTwoFake = UserFake.Create().Generate(1).First();
            userTwoFake.Name = userTwo;
            userTwoFake.Email = $"{userTwo}@email.com";
            _accountsContextMock.Add(userTwoFake);


            await _accountsContextMock.SaveChangesAsync();

            //Act
            var result = await _userRepository.GetPaginationAsync(search,1,20);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult,result.Count());
        }
    }
}