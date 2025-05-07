using ExpenseAdminSystem.API.Controllers;
using ExpenseAdminSystem.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExpenseAdminSystem.Tests
{
    [TestClass]
    public class ExpenseControllerTests
    {
        private ExpenseController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new ExpenseController(new FakeExpenseRepository());
        }

        [TestMethod]
        public void GetExpense_ReturnsOk_WhenExpenseExists()
        {
            var result = _controller.GetExpense(1).Result;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetExpense_ReturnsNotFound_WhenExpenseDoesNotExist()
        {
            var result = _controller.GetExpense(999).Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetExpenses_ReturnsOkList()
        {
            var result = _controller.GetExpenses(1).Result;
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void PostExpense_ReturnsOk_WhenValid()
        {
            var newExpense = new Expense(3)
            {
                UserId = 1,
                Amount = 20,
                ExpenseDate = DateTime.Now,
                CategoryId = 3,
                CurrencyId = 1,
                Comments = "Coffee",
                CreatedAt = DateTime.Now
            };

            var result = _controller.Post(newExpense);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void PostExpense_ReturnsBadRequest_WhenNull()
        {
            var result = _controller.Post(null);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void DeleteExpense_ReturnsNoContent_WhenSuccessful()
        {
            var result = _controller.DeleteExpense(1);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteExpense_ReturnsNotFound_WhenExpenseMissing()
        {
            var result = _controller.DeleteExpense(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public void UpdateExpense_ReturnsOk_WhenSuccessful()
        {
            var updateExpense = new Expense(1)
            {
                UserId = 1,
                Amount = 999,
                ExpenseDate = DateTime.Now,
                CategoryId = 5,
                CurrencyId = 2,
                Comments = "Updated expense",
                CreatedAt = DateTime.Now
            };

            var result = _controller.UpdateExpense(updateExpense);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateExpense_ReturnsNotFound_WhenExpenseNotFound()
        {
            var updateExpense = new Expense(999)
            {
                UserId = 1,
                Amount = 50,
                ExpenseDate = DateTime.Now,
                CategoryId = 1,
                CurrencyId = 1,
                Comments = "Doesn't exist",
                CreatedAt = DateTime.Now
            };

            var result = _controller.UpdateExpense(updateExpense);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
    }
}

