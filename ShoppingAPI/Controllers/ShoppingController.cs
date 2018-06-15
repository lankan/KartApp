using ShoppingAPI.Models;
using ShoppingBasket;
using ShoppingBasket.Models;
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
        private readonly IKartLogic _basketLogic;
        private HttpResponseMessage reponse; 

        public ShoppingController(IKartLogic basketLogic)
        {
            _basketLogic = basketLogic;
        }

        #region Helpers

        private Product CreateProductModelObject(Product model)
        {
            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity
            };

            return product;
        }

        #endregion

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
                return BadRequest(); 
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult CreateBasket([FromBody]List<ProductModel> model)
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
                    products.Add(new Product
                    {
                        ProductId = Guid.NewGuid(),
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                        Quantity = item.Quantity
                    });
                };

                var basket = new Kart()
                {
                    BasketId = Guid.NewGuid(),
                    Products = products,
                    Added = DateTime.Now
                };

                var newBasket = _basketLogic.CreateBasket(basket);

                if (newBasket == null)
                {
                    return BadRequest();
                }

                return Ok(newBasket.BasketId);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult AddItem(Guid basketId, [FromBody] ProductModel product)
        {
            try
            {
                if (product == null || !ModelState.IsValid)
                {
                    return BadRequest();
                }

                var Product = new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity
                };

                var updatedBasket = _basketLogic.AddProducts(basketId, Product);

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
        public ActionResult EmptyBasket(Guid basketId)
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
                    return StatusCode(500);
                }

                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult RemoveItem(Guid basketId, Guid productId)
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
                    return NotFound(productId);
                }

                var updatedBasket = _basketLogic.RemoveProducts(basketId, products);

                if (updatedBasket == null)
                {
                    return StatusCode(500);
                }

                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult IncreaseQuantity(Guid basketId, Guid productId)
        {
            try
            {
                var updatedBasket = _basketLogic.IncreaseProductQuantity(basketId, productId);

                if (updatedBasket == null)
                {
                    return StatusCode(500);
                }

                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult ReduceQuantity(Guid basketId, Guid productId)
        {
            try
            {
                var updatedBasket = _basketLogic.DecreaseProductQuantity(basketId, productId);

                if (updatedBasket == null)
                {
                    return StatusCode(500);
                }

                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
