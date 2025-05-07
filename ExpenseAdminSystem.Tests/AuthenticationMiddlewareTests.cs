// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Microsoft.AspNetCore.Http;
// using Moq;
// using System.IO;
// using System.Text;
// using System.Threading.Tasks;
// using CourseAdminSystem.API.Middleware;
// using ExpenseAdminSystem.Model.Repositories;
// using ExpenseAdminSystem.Model.Entities;
// using Microsoft.Extensions.DependencyInjection;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.Extensions.Configuration;

// namespace ExpenseAdminSystem.Tests
// {
//     [TestClass]
//     public class BasicAuthenticationMiddlewareTests
//     {
//         private DefaultHttpContext CreateHttpContext(string authHeader, UserRepository fakeRepo)
//         {
//             var context = new DefaultHttpContext();
//             context.Request.Headers["Authorization"] = authHeader;

//             // Set up service provider with mocked repository
//             var services = new ServiceCollection();
//             services.AddSingleton(fakeRepo);
//             context.RequestServices = services.BuildServiceProvider();

//             // Set up response body to capture output
//             context.Response.Body = new MemoryStream();

//             return context;
//         }

//         private string GetResponseBody(HttpResponse response)
//         {
//             response.Body.Seek(0, SeekOrigin.Begin);
//             using var reader = new StreamReader(response.Body, Encoding.UTF8);
//             return reader.ReadToEnd();
//         }

//         [TestMethod]
//         public async Task Middleware_Allows_Request_With_Valid_Credentials()
//         {
//             // Arrange
//             var userRepo = new UserRepositoryFake(new[]
//             {
//                 new User(1)
//                 {
//                     UserName = "john.doe",
//                     Password = "VerySecret!",
//                     Email = "john@example.com",
//                     CreatedAt = DateTime.UtcNow
//                 }
//             });

//             var context = CreateHttpContext("Basic am9obi5kb2U6VmVyeVNlY3JldCE=", userRepo);
//             bool nextCalled = false;
//             RequestDelegate next = (ctx) =>
//             {
//                 nextCalled = true;
//                 //ctx.Response.StatusCode = 200;
//                 return Task.CompletedTask;
//             };

//             var middleware = new BasicAuthenticationMiddleware(next);

//             // Act
//             await middleware.InvokeAsync(context);

//             // Assert
//             Assert.IsFalse(nextCalled, "Next delegate should not have been called due to invalid credentials");
//             Assert.IsTrue(nextCalled);
//             Assert.AreEqual(200, context.Response.StatusCode); // default if not set
//         }

//         [TestMethod]
//         public async Task Middleware_Rejects_Request_With_Invalid_Password()
//         {
//             // Arrange
//             var userRepo = new UserRepositoryFake(new[]
//             {
//                 new User(1)
//                 {
//                     UserName = "john.doe",
//                     Password = "FakePassword!",
//                     Email = "john@example.com",
//                     CreatedAt = DateTime.UtcNow
//                 }
//             });

//             var context = CreateHttpContext("Basic am9obi5kb2U6VmVyeVNlY3JldCE=", userRepo);
//             var middleware = new BasicAuthenticationMiddleware((ctx) => Task.CompletedTask);

//             // Act
//             await middleware.InvokeAsync(context);
//             var body = GetResponseBody(context.Response);

//             // Assert
//             Assert.AreEqual(401, context.Response.StatusCode);
//             Console.WriteLine("Response body: >" + body + "<");
//             Assert.AreEqual("Invalid Authorization Header", body.Trim());
//         }
//     }

//     // Fake UserRepository for testing
//     public class UserRepositoryFake : UserRepository
//     {
//         private readonly List<User> _users;

//         public UserRepositoryFake(IEnumerable<User> users)
//         : base(new ConfigurationBuilder().Build())
//         {
//             _users = users.ToList();
//         }

//         // public override IEnumerable<User> GetUsers()
//         // {
//         //     return _users;
//         // }
//         public new IEnumerable<User> GetUsers() => _users;
//     }
// }
