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
    public class ProductsTests
    {
        private ProductsController _controller;
        private MockRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new MockRepository();
            _controller = new ProductsController(_repository);
            MockUser.CurrentUser(_controller);
            MockHttpConfiguration.HttpConfiguration(_controller);
        }

        [TestMethod]
        public void ProductsGet_Null_AllProductsMatchId()
        {
            // Arrange
            MockHttpConfiguration.HttpGet(_controller);
            var expectedProducts = _repository.Widgets.GetAllWidgets();

            // Act
            var response = _controller.Get();
            Type responseType = response.GetType();
            PropertyInfo productsProperty = responseType.GetProperty("Products"); // getting the Orders array inside json result
            IEnumerable results = productsProperty.GetValue(response, null) as IEnumerable;

            // Assert
            foreach (Product product in results)
            {
                Assert.IsTrue(expectedProducts.Where(x => x.Id == product.Id).First() != null);
            }
        }

        [TestMethod]
        public void ProductGet_Id_ProductMatchId()
        {
            // Arrange
            int id = 100;
            MockHttpConfiguration.HttpGet(_controller);

            // Act
            IHttpActionResult response = _controller.Get(id);
            var result = response as OkNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(id, result.Content.Id);
        }

        [TestMethod]
        public void ProductPost_Product_HttpOk()
        {
            // Arrange
            var productType = new ProductType
            {
                Id = 1,
                Description = "Donic"
            };
            Product newProduct = new Product()
            {
                Id = 9,
                Description = "lorem ipsum test",
                Name = "Product Test Name",
                Price = 1.99m,
                Type = productType
            };
            MockHttpConfiguration.HttpPost(_controller);

            // Act
            IHttpActionResult response = _controller.Post(newProduct);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void ProductPut_Product_HttpOk()
        {
            // Arrange
            int id = 100;
            MockHttpConfiguration.HttpGet(_controller);
            IHttpActionResult response = _controller.Get(id);
            var result = response as OkNegotiatedContentResult<Product>;
            var existingProduct = result.Content;
            existingProduct.Price = 99.99m; // Changing value for update
            MockHttpConfiguration.HttpPut(_controller);

            // Act
            response = _controller.Put(existingProduct);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void ProductDelete_Product_HttpOk()
        {
            // Arrange
            int id = 100;
            MockHttpConfiguration.HttpGet(_controller);
            IHttpActionResult response = _controller.Get(id);
            var result = response as OkNegotiatedContentResult<Product>;
            var existingProduct = result.Content;
            MockHttpConfiguration.HttpDelete(_controller);

            // Act
            response = _controller.Delete(existingProduct);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }
    }
}
