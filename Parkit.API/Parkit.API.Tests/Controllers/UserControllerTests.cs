using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Parkit.API.Controllers;
using Parkit.API.Tests.AutoFixture;
using Parkit.Core.DAL;
using Parkit.Core.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Parkit.API.Tests.Controllers
{
    public class UserControllerTests
    {
        #region Get
        [Theory]
        [AutoDomainData]
        public async Task Get_WithValidEntityId_ShouldReturnUser(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            User user)
        {
            // Arrange
            userRepository.GetUserById(user.Id.Value)
                .Returns(user);

            var sut = new UserController(logger, userRepository);

            // Act
            var result = await sut.Get(user.Id.Value) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            var actual = result.Value as User;
            actual.Should().BeEquivalentTo(user);
        }

        [Theory]
        [AutoDomainData]
        public async Task Get_WithInvalidEntityId_ShouldReturnNotFound(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            User user)
        {
            // Arrange
            var sut = new UserController(logger, userRepository);

            // Act
            var result = await sut.Get(user.Id.Value) as NotFoundResult;

            // Assert
            result.Should().NotBeNull();
        }
        #endregion Get

        #region Create
        [Theory]
        [AutoDomainData]
        public async Task Create_WithValidData_ShouldReturnUser(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            User user)
        {
            // Arrange
            var newUser = new User()
            {
                Email = user.Email,
                FamilyName = user.FamilyName,
                GivenName = user.GivenName,
            };

            userRepository.When(x => x.InsertUser(newUser))
                .Do(x => {
                    newUser.Id = user.Id;
                    newUser.Created = user.Created;
                });

            var sut = new UserController(logger, userRepository);

            // Act
            var result = await sut.Add(newUser) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            var actual = result.Value as User;
            actual.Should().BeEquivalentTo(user);
        }

        [Theory]
        [AutoDomainData]
        public async Task Create_DbSaveException_ShouldReturnServerError(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            User user,
            DbUpdateException dbException)
        {
            // Arrange
            userRepository.Save()
                .Throws(dbException);

            var sut = new UserController(logger, userRepository);

            // Act
            await Assert.ThrowsAsync<DbUpdateException>(() => sut.Add(user));
        }
        #endregion Create

        #region Update
        [Theory]
        [AutoDomainData]
        public async Task Update_WithValidData_ShouldReturnUser(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            User user)
        {
            // Arrange
            var sut = new UserController(logger, userRepository);

            // Act
            var result = await sut.Update(user.Id.Value, user) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            var actual = result.Value as User;
            actual.Should().BeEquivalentTo(user);
        }

        [Theory]
        [AutoDomainData]
        public async Task Update_DbSaveException_ShouldReturnServerError(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            User user,
            DbUpdateException dbException)
        {
            // Arrange
            userRepository.Save()
                .Throws(dbException);

            var sut = new UserController(logger, userRepository);

            // Act
            await Assert.ThrowsAsync<DbUpdateException>(() => sut.Update(user.Id.Value, user));
        }

        [Theory]
        [AutoDomainData]
        public async Task Update_IdMisMatch_ShouldReturnValidation(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            User user,
            Guid differentEntityId,
            DbUpdateException dbException)
        {
            // Arrange
            userRepository.Save()
                .Throws(dbException);

            var sut = new UserController(logger, userRepository);

            // Act
            var result = await sut.Update(differentEntityId, user) as StatusCodeResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }
        #endregion Update

        #region Delete
        [Theory]
        [AutoDomainData]
        public async Task Delete_ValidEntityId_ShouldReturnOk(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            Guid entityId)
        {
            // Arrange
            var sut = new UserController(logger, userRepository);

            // Act
            var result = await sut.Delete(entityId) as OkResult;

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [AutoDomainData]
        public async Task Delete_DbSaveException_ShouldReturnServerError(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            Guid entityId,
            DbUpdateException dbUpdateException)
        {
            // Arrange
            userRepository.Save()
                .Throws(dbUpdateException);

            var sut = new UserController(logger, userRepository);

            // Act
            await Assert.ThrowsAsync<DbUpdateException>(() => sut.Delete(entityId));
        }
        #endregion Delete
    }
}
