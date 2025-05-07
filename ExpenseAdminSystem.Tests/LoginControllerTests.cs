// using ExpenseAdminSystem.API.Controllers;
// using ExpenseAdminSystem.API.Model;
// using ExpenseAdminSystem.Model.Entities;
// using ExpenseAdminSystem.Model.Repositories;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
// using System.Collections.Generic;

// namespace ExpenseAdminSystem.Tests
// {
//     [TestClass]
//     public class LoginControllerTests
//     {
//         private Mock<IUserRepository> _userRepositoryMock;
//         private LoginController _loginController;

//         [TestInitialize]
//         public void Setup()
//         {
//             // Use IUserRepository instead of concrete UserRepository
//             _userRepositoryMock = new Mock<IUserRepository>();
//             _loginController = new LoginController(_userRepositoryMock.Object);
//         }

//         [TestMethod]
//         public void Login_ShouldReturnOk_WhenCredentialsAreValid()
//         {
//             // Arrange
//             var validCredentials = new Login { Username = "testuser", Password = "testpassword" };
//             var user = new User(1) { UserName = "testuser", Password = "testpassword" };

//             _userRepositoryMock.Setup(repo => repo.GetUsers()).Returns(new List<User> { user });

//             // Act
//             var result = _loginController.Login(validCredentials);

//             // Assert
//             var okResult = result as OkObjectResult;
//             Assert.IsNotNull(okResult);
//             dynamic response = okResult.Value;
//             Assert.AreEqual("Basic dGVzdHVzZXI6dGVzdHBhc3N3b3Jk", response.headerValue);
//             Assert.AreEqual("testuser", response.username);
//             Assert.AreEqual(1, response.id);
//         }

//         [TestMethod]
//         public void Login_ShouldReturnUnauthorized_WhenPasswordDoesNotMatch()
//         {
//             // Arrange
//             var credentials = new Login { Username = "testuser", Password = "wrongpassword" };
//             var user = new User(1) { UserName = "testuser", Password = "correctpassword" };

//             _userRepositoryMock.Setup(repo => repo.GetUsers()).Returns(new List<User> { user });

//             // Act
//             var result = _loginController.Login(credentials);

//             // Assert
//             Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
//         }

//         [TestMethod]
//         public void Login_ShouldReturnUnauthorized_WhenUserNotFound()
//         {
//             // Arrange
//             var credentials = new Login { Username = "nonexistent", Password = "somepassword" };

//             _userRepositoryMock.Setup(repo => repo.GetUsers()).Returns(new List<User>
//             {
//                 new User(1) { UserName = "testuser", Password = "testpassword" }
//             });

//             // Act
//             var result = _loginController.Login(credentials);

//             // Assert
//             Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
//         }

//         [TestMethod]
//         public void Login_ShouldReturnBadRequest_WhenCredentialsAreNull()
//         {
//             // Act
//             var result = _loginController.Login(null);

//             // Assert
//             var badRequest = result as BadRequestObjectResult;
//             Assert.IsNotNull(badRequest);
//             Assert.AreEqual("Invalid client request", badRequest.Value);
//         }

//         [TestMethod]
//         public void Login_ShouldReturnBadRequest_WhenCredentialsAreEmpty()
//         {
//             // Arrange
//             var emptyCredentials = new Login { Username = "", Password = "" };

//             // Act
//             var result = _loginController.Login(emptyCredentials);

//             // Assert
//             var badRequest = result as BadRequestObjectResult;
//             Assert.IsNotNull(badRequest);
//             Assert.AreEqual("Invalid client request", badRequest.Value);
//         }
//     }
// }
