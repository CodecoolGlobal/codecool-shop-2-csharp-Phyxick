﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using NSubstitute;
using NSubstitute.ClearExtensions;
using NUnit.Framework;

namespace Codecool.CodecoolShop.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserDao _userDao;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userDao = Substitute.For<IUserDao>();
            _userService = new UserService(_userDao);
        }

        [Test]
        public void ValidateLoginValidValue()
        {
            var user = new User {Username = "WorkPlease", Password = "PleaseWork"};
            _userDao.ValidateLogin(user).Returns(true);
            Assert.IsTrue(_userService.ValidateLogin(user));
        }

        [Test]
        public void RemoveValidValue()
        {
            var user = new User { Username = "WorkPlease", Password = "PleaseWork" };
            _userDao.Remove(user);
            _userDao.Received().Remove(user);
        }

        [Test]
        public void RegisterValidValue()
        {
            var user = new User { Username = "WorkPlease", Password = "PleaseWork" };
            _userDao.Register(user).Returns(true);
            Assert.IsTrue(_userService.Register(user));
        }
    }
}