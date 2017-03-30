using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Exceptions;
using Moq;
using NUnit.Framework;

namespace GameStore.Infrastructure.Services.Tests
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IUserService _userService;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userService = new UserService(_unitOfWorkMock.Object);
        }

        [Test]
        public void ShouldRegisterNewUser()
        {
            _unitOfWorkMock
                .Setup(m => m.Roles.Find(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns(new List<Role>());

            _unitOfWorkMock
                .Setup(m => m.Users.SingleOrDefaultDeleted(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns((User)null);

            _unitOfWorkMock
                .Setup(m => m.Users.Create(It.IsAny<User>()));

            _unitOfWorkMock
                .Setup(m => m.Save());

            _userService.Register("userName", "password", new List<string>());

            _unitOfWorkMock.Verify(m => m.Roles.Find(It.IsAny<Expression<Func<Role, bool>>>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Users.SingleOrDefaultDeleted(It.IsAny<Expression<Func<User, bool>>>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Users.Create(It.IsAny<User>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void ShouldRegisterUser_WithUserName_WhichWasDeleted()
        {
            _unitOfWorkMock
                .Setup(m => m.Roles.Find(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns(new List<Role>());

            _unitOfWorkMock
                .Setup(m => m.Users.SingleOrDefaultDeleted(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User());

            _unitOfWorkMock
                .Setup(m => m.Users.Update(It.IsAny<User>()));

            _unitOfWorkMock
                .Setup(m => m.Save());

            _userService.Register("userName", "password", new List<string>());

            _unitOfWorkMock.Verify(m => m.Roles.Find(It.IsAny<Expression<Func<Role, bool>>>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Users.SingleOrDefaultDeleted(It.IsAny<Expression<Func<User, bool>>>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Users.Update(It.IsAny<User>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void ShouldBanUser()
        {
            const string userName = "userName";
            _unitOfWorkMock
                .Setup(m => m.Users.SingleOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User("userName", "password", "salt", new List<Role>()));
            var service = new UserService(_unitOfWorkMock.Object);

            service.BanUser(userName, BanDuration.Hour);
            service.BanUser(userName, BanDuration.Day);
            service.BanUser(userName, BanDuration.Week);
            service.BanUser(userName, BanDuration.Month);
            service.BanUser(userName, BanDuration.Permanent);
            service.BanUser(userName, BanDuration.Default);

            _unitOfWorkMock.Verify(m => m.Users.SingleOrDefault(It.IsAny<Expression<Func<User, bool>>>()), Times.Exactly(6));
        }

        [Test]
        public void ShouldUpdateUser()
        {
            DateTime banExpiryDate = DateTime.UtcNow;
            IList<string> rolesNames = new List<string>();
            const string id = "1";

            _unitOfWorkMock
                .Setup(m => m.Users.Get(It.IsAny<string>()))
                .Returns(new User());

            _unitOfWorkMock
                .Setup(m => m.Roles.Find(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns(new List<Role>());

            _unitOfWorkMock.Setup(m => m.Users.Update(It.IsAny<User>()));
            _unitOfWorkMock.Setup(m => m.Save());

            _userService.Update(id, banExpiryDate, rolesNames);

            _unitOfWorkMock.Verify(m => m.Users.Get(It.IsAny<string>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Roles.Find(It.IsAny<Expression<Func<Role, bool>>>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Users.Update(It.IsAny<User>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void ShouldThrowException_WhenUserNotFound()
        {
            DateTime banExpiryDate = DateTime.UtcNow;
            IList<string> rolesNames = new List<string>();
            const string id = "1";

            _unitOfWorkMock
                .Setup(m => m.Users.Get(It.IsAny<string>()))
                .Returns((User)null);

            Assert.That(() => _userService.Update(id, banExpiryDate, rolesNames), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldDeleteUser()
        {
            _unitOfWorkMock
                .Setup(m => m.Users.Get(It.IsAny<string>()))
                .Returns(new User());

            _unitOfWorkMock.Setup(m => m.Users.Delete(It.IsAny<User>()));
            _unitOfWorkMock.Setup(m => m.Save());

            _userService.Delete("userId");

            _unitOfWorkMock.Verify(m => m.Users.Get(It.IsAny<string>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Users.Delete(It.IsAny<User>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void ShouldThrowException_WhenUserNotFound_ForBanning()
        {
            _unitOfWorkMock
                .Setup(m => m.Users.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>());

            _unitOfWorkMock
                .Setup(m => m.Users.FindDeletedEntities(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>());

            Assert.That(() => _userService.BanUser("userName", BanDuration.Default), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldReturnAllGames()
        {
            _unitOfWorkMock
                .Setup(m => m.Users.GetAll())
                .Returns(new List<User>());

            IEnumerable<UserDto> users = _userService.GetAll();

            _unitOfWorkMock.Verify(m => m.Users.GetAll(), Times.AtLeastOnce);
            Assert.AreEqual(0, users.Count());
        }

        [Test]
        public void ShouldReturnGame()
        {
            _unitOfWorkMock
                .Setup(m => m.Users.SingleOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User());

            _userService.Get("userName", "password");

            _unitOfWorkMock.Verify(m => m.Users.SingleOrDefault(It.IsAny<Expression<Func<User, bool>>>()), Times.AtLeastOnce);
        }

        [Test]
        public void ShouldThrowException_WhenAddNotExistenRole_ForUser()
        {
            _unitOfWorkMock
                .Setup(m => m.Users.Get(It.IsAny<string>()))
                .Returns(new User());

            _unitOfWorkMock
                .Setup(m => m.Roles.Find(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns(new List<Role>());

            Assert.That(() => _userService.AddRoleToUser("userId", "role"), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldThrowException_WhenAddRole_WhichContains_AtUserRoles()
        {
            var role = new Role();

            _unitOfWorkMock
                .Setup(m => m.Users.Get(It.IsAny<string>()))
                .Returns(new User { Roles = new List<Role> { role } });

            _unitOfWorkMock
                .Setup(m => m.Roles.SingleOrDefault(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns(role);

            Assert.That(() => _userService.AddRoleToUser("userId", "role"), Throws.TypeOf<RoleDoubleAddingException>());
        }

        [Test]
        public void ShouldAddRoleToUser()
        {
            _unitOfWorkMock
                .Setup(m => m.Users.Get(It.IsAny<string>()))
                .Returns(new User());

            _unitOfWorkMock
                .Setup(m => m.Roles.SingleOrDefault(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns(new Role());

            _unitOfWorkMock.Setup(m => m.Users.Update(It.IsAny<User>()));
            _unitOfWorkMock.Setup(m => m.Save());

            _userService.AddRoleToUser("userId", "role");

            _unitOfWorkMock.Verify(m => m.Users.Get(It.IsAny<string>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Roles.SingleOrDefault(It.IsAny<Expression<Func<Role, bool>>>()));
            _unitOfWorkMock.Verify(m => m.Users.Update(It.IsAny<User>()));
            _unitOfWorkMock.Verify(m => m.Save());
        }

        [Test]
        public void ShouldReturnUserById()
        {
            _unitOfWorkMock
                .Setup(m => m.Users.Get(It.IsAny<string>()))
                .Returns(new User());

            _userService.Get("userId");

            _unitOfWorkMock.Verify(m => m.Users.Get(It.IsAny<string>()));
        }
    }
}
