using ShoppingAPI.Models;
using ShoppingBasket;
using ShoppingBasket.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Web.Http; 
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
 using System.Web.Http.Results;

namespace ShoppingAPI.Controllers
{
    [System.Web.Http.Route("api/basket")]
    public class ShoppingController : ApiController
    {
        private readonly IKart _basketLogic;
        private readonly IProduct _product;
        private readonly ILogger _logger;


        public ShoppingController(IKart basketLogic, IProduct product, ILogger<ShoppingController> logger)
        {
            _basketLogic = basketLogic;
            _product = product;
            _logger = logger; 
        }

        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Basket(Guid basketId)
        {
            try
            {
                var basket = _basketLogic.GetBasket(basketId);

                if (basket == null)
                {
                   // return not 
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(); 
            }
        }

        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Baskets()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            try
            {
                var baskets = _basketLogic.GetBaskets();

                if (baskets == null || baskets.Count == 0)
                {
                    return NotFound();
                }

                return Ok(baskets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(); 
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult CreateBasket([FromBody]List<Models.ProductModel> model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest(model.ToString());
                }

                List<Product> products = new List<Product>();

                foreach (var item in model)
                {
                    var newProduct = _product.CreateProduct();
                    //
                    newProduct.ProductId = Guid.NewGuid();
                    newProduct.Name = item.Name;
                    newProduct.Description = item.Description;
                    newProduct.Quantity = item.Quantity;
                    newProduct.Price = item.Price;
                    //
                    products.Add(newProduct); 
                };
                //
                Kart newBasket = _basketLogic.CreateBasket();
                //
                newBasket.BasketId = Guid.NewGuid();
                newBasket.Added = DateTime.Now;
                newBasket.Products = products;
                //
                if (newBasket == null)
                {
                    return BadRequest();
                }

                return Ok(newBasket.BasketId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult AddItem(Guid basketId, [FromBody] Models.ProductModel product)
        {
            try
            {
                if (product == null || !ModelState.IsValid)
                {
                    return BadRequest();
                }
                //
                var newProduct = _product.CreateProduct();
                //
                newProduct.ProductId = Guid.NewGuid();
                newProduct.Name = product.Name;
                newProduct.Description = product.Description;
                newProduct.Quantity = product.Quantity;
                newProduct.Price = product.Price;
                //
                var updatedBasket = _basketLogic.AddProducts(basketId, newProduct);

                if (updatedBasket == null)
                {
                    return BadRequest();
                }

                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult EmptyBasket(Guid basketId)
        {
            try
            {
                var basket = _basketLogic.GetBasket(basketId);

                if (basket == null)
                {
                    return BadRequest();
                }

                var updatedBasket = _basketLogic.RemoveProducts(basketId, basket.Products);

                if (updatedBasket == null)
                {
                    return BadRequest();
                }

                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult RemoveItem(Guid basketId, Guid productId)
        {
            try
            {
                var basket = _basketLogic.GetBasket(basketId);

                if (basket == null)
                {
                    return BadRequest();
                }

                var products = new List<Product>()
                {
                    basket.Products.Find(x => x.ProductId == productId)
                };

                if (products.Count == 0)
                {
                    return BadRequest();
                }

                var updatedBasket = _basketLogic.RemoveProducts(basketId, products);

                if (updatedBasket == null)
                {
                    return BadRequest();
                }

                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult IncreaseQuantity(Guid basketId, Guid productId)
        {
            try
            {
                var updatedBasket = _basketLogic.IncreaseProductQuantity(basketId, productId);

                if (updatedBasket == null)
                {
                    return BadRequest();
                }

                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(); 
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult ReduceQuantity(Guid basketId, Guid productId)
        {
            try
            {
                var updatedBasket = _basketLogic.DecreaseProductQuantity(basketId, productId);

                if (updatedBasket == null)
                {
                    return BadRequest();
                }

                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

    }
}
