using System;
using ShoppingBasket.Models;
using System.Collections.Generic;

namespace ShoppingBasket.Logic
{
    public class KartLogic : IKart
    {
        private readonly IKart _kart;

        public KartLogic(IKart repo)
        {
            _kart = repo;
        }

        public Kart AddProducts(Guid basketId, Product products)
        {
            try
            {
                var basket = _kart.GetBasket(basketId);

                if (basket == null)
                {
                    return null;
                }

                basket.Updated = DateTime.Now;

                var updatedBasket = _kart.AddProducts(basketId, products);

                return updatedBasket;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Kart CreateBasket(Kart basket)
        {
            try
            {
                return _kart.CreateBasket(basket);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Kart GetBasket(Guid id)
        {
            try
            {
                return _kart.GetBasket(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Kart> GetBaskets()
        {
            try
            {
                return _kart.GetBaskets();
            }
            catch (Exception ex)
            {
                return new List<Kart>();
            }
        }

        public Kart RemoveProducts(Guid basketId, List<Product> products)
        {
            try
            {
                var basket = _kart.GetBasket(basketId);

                if (basket == null)
                {
                    return null;
                }

                basket.Updated = DateTime.Now;

                var updatedBasket = _kart.RemoveProducts(basketId, products);

                return updatedBasket;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Kart IncreaseProductQuantity(Guid basketId, Guid productId)
        {
            try
            {
                var basket = _kart.GetBasket(basketId);

                if (basket == null)
                {
                    return null;
                }

                var index = basket.Products.FindIndex(x => x.ProductId == productId);
                basket.Products[index].Quantity++;

                basket.Updated = DateTime.Now;

                var updatedBasket = _kart.UpdateBasket(basket);

                return updatedBasket;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Kart DecreaseProductQuantity(Guid basketId, Guid productId)
        {
            try
            {
                var basket = _kart.GetBasket(basketId);

                if (basket == null)
                {
                    return null;
                }

                var index = basket.Products.FindIndex(x => x.ProductId == productId);

                if (index < 0)
                {
                    return null;
                }

                if (basket.Products[index].Quantity > 0)
                {
                    basket.Products[index].Quantity--;
                }
                else
                {
                    var products = new List<Product>()
                {
                    basket.Products[index]
                };

                    RemoveProducts(basket.BasketId, products);
                }

                basket.Updated = DateTime.Now;

                var updatedBasket = _kart.UpdateBasket(basket);

                return updatedBasket;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
