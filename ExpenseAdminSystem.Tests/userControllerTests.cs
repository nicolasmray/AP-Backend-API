using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpenseAdminSystem.API.Controllers;
using ExpenseAdminSystem.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseAdminSystem.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new UserController(new FakeUserRepository());
        }

        [TestMethod]
        public void GetUser_ReturnsUser_WhenExists()
        {
            var result = _controller.GetUser(1).Result;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetUser_ReturnsNotFound_WhenNotExists()
        {
            var result = _controller.GetUser(999).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteUser_ReturnsNoContent_WhenSuccessful()
        {
            var result = _controller.DeleteUser(1);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteUser_ReturnsNotFound_WhenUserMissing()
        {
            var result = _controller.DeleteUser(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public void PostUser_ReturnsOk_WhenValid()
        {
            var newUser = new User(3) { UserName = "new", Password = "pwd" };
            var result = _controller.Post(newUser);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void PostUser_ReturnsBadRequest_WhenNull()
        {
            var result = _controller.Post(null);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}
