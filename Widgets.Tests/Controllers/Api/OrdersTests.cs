using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Results;
using Widgets.Controllers.Api;
using Widgets.Models;
using Widgets.Tests.Mocks;

/// <summary>
/// Test Method Names = Method_Condition_Result
/// Test Method = Arrange, Act, Assert
/// </summary>
namespace Widgets.Tests.Controllers.Api
{
    [TestClass]
    public class OrdersTests
    {
        private OrdersController _controller;
        private MockRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new MockRepository();
            _controller = new OrdersController(_repository);
            MockUser.CurrentUser(_controller);
            MockHttpConfiguration.HttpConfiguration(_controller);
        }

        [TestMethod]
        public void OrdersGet_Null_AllOrdersMatchId()
        {
            // Arrange
            MockHttpConfiguration.HttpGet(_controller);
            var expectedOrders = _repository.Orders.GetAllOrders();

            // Act
            var response = _controller.Get();
            Type responseType = response.GetType();
            PropertyInfo ordersProperty = responseType.GetProperty("Orders"); // getting the Orders array inside json result
            IEnumerable results = ordersProperty.GetValue(response, null) as IEnumerable;

            // Assert
            foreach (PurchaseOrder order in results)
            {
                Assert.IsTrue(expectedOrders.Where(x => x.Id == order.Id).First() != null);
            }
        }

        [TestMethod]
        public void OrderGet_Id_OrderMatchId()
        {
            // Arrange
            int id = 1;
            MockHttpConfiguration.HttpGet(_controller);

            // Act
            IHttpActionResult response = _controller.Get(id);
            var result = response as OkNegotiatedContentResult<PurchaseOrder>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(id, result.Content.Id);
        }

        [TestMethod]
        public void OrderPost_Order_HttpOk()
        {
            // Arrange
            PurchaseOrder newOrder = new PurchaseOrder()
            {
                ProductId = 105,
                Quantity = 1,
                TotalPrice = 100,
                UserId = 1
            };
            MockHttpConfiguration.HttpPost(_controller);

            // Act
            IHttpActionResult response = _controller.Post(newOrder);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void OrderPut_Order_HttpOk()
        {
            // Arrange
            int id = 2;
            MockHttpConfiguration.HttpGet(_controller);
            IHttpActionResult response = _controller.Get(id);
            var result = response as OkNegotiatedContentResult<PurchaseOrder>;
            var existingOrder = result.Content;
            existingOrder.Quantity = 999; // Changing value for update
            MockHttpConfiguration.HttpPut(_controller);

            // Act
            response = _controller.Put(existingOrder);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void OrderDelete_Order_HttpOk()
        {
            // Arrange
            int id = 1;
            MockHttpConfiguration.HttpGet(_controller);
            IHttpActionResult response = _controller.Get(id);
            var result = response as OkNegotiatedContentResult<PurchaseOrder>;
            var existingOrder = result.Content;
            MockHttpConfiguration.HttpDelete(_controller);

            // Act
            response = _controller.Delete(existingOrder);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }
    }
}
