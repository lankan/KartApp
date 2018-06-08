using ShoppingAPI.Models;
using ShoppingBasket;
using ShoppingBasket.Models;
using System;
using System.Web.Http; 
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;

namespace ShoppingAPI.Controllers
{
    [System.Web.Http.Route("api/basket")]
    public class BasketController : Controller
    {
        private readonly IBasketLogic _basketLogic;

        public BasketController(IBasketLogic basketLogic)
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

                return StatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Baskets()
        {
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
                return StatusCode(500);
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult CreateBasket([FromBody]List<Products> model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }

                List<Product> products = new List<Product>();

                foreach (var item in model)
                {
                    products.Add(CreateProductModelObject(item));
                }

                var basket = new Basket()
                {
                    BasketId = Guid.NewGuid(),
                    Products = products,
                    Added = DateTime.Now
                };

                var newBasket = _basketLogic.CreateBasket(basket);

                if (newBasket == null)
                {
                    return HttpStatusCodeResult ;
                }

                return Ok(newBasket.BasketId);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddItem(Guid basketId, [FromBody]Products product)
        {
            try
            {
                if (product == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }

                var products = new List<Product>
            {
                CreateProductModelObject(product)
            };

                var updatedBasket = _basketLogic.AddProducts(basketId, products);

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
