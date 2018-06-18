using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingAPI.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using Moq;
using System.Threading.Tasks;
using System.Web.Http;
using ShoppingAPI.Models;
using ShoppingBasket.Logic;
using ShoppingBasket.Models;
using ShoppingBasket;
using ProductModel = ShoppingAPI.Models.ProductModel;

namespace ShoppingAPI.Controllers.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        private ShoppingController _controller;
        private readonly Guid _testGuid = Guid.NewGuid();
        private Kart _testBasket;
        private List<Product> newTestProd;
        private Models.ProductModel _testProductModel;
        private List<Models.ProductModel> _testProductModelList;

        private readonly Mock<KartLogic> _mockBasketLogic = new Mock<KartLogic>();
        private readonly Mock<ILogger<ShoppingController>> _mockILogger = new Mock<ILogger<ShoppingController>>();
        
        [TestInitialize]
        public void TestSetup_2018()
        {

            Product newTestProd = new Product()
            {
                ProductId = Guid.NewGuid(),
                Name = "TestItem 1",
                Description = "Test Product",
                Price = 23,
                Quantity = 12
            };

            _testBasket = new Kart()
            {
                BasketId = _testGuid,
                Products = this.newTestProd,
                Added = DateTime.Now
            };
            _testProductModel = new Models.ProductModel()
            {
                Name = "TestItem 2",
                Description = "Description 1",
                Price = 10,
                Quantity = 1
            };
        }

        [TestMethod]
        public void CreateBasket_Valid()
        {
            var productModelList = new List<ProductModel>()
            {
                _testProductModel
            };

            var actualResult = _controller.CreateBasket(productModelList);

            var result = actualResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Accepted, actualResult);

        }

        [TestMethod]
        public void CreateBasket()
        {
            var productModelList = new List<ProductModel>()
            {
                _testProductModel
            };

            var actualResult = _controller.CreateBasket(productModelList);

            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();

            // Act
            var response = _controller.CreateBasket(productModelList); 
            // Assert
            Assert.AreEqual(HttpStatusCode.Accepted, actualResult);
        }
    }
}