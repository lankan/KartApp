using System;
using ShoppingBasket.Models;
using System.Collections.Generic;

namespace ShoppingBasket.Logic
{
    public class BasketLogic : IBasketLogic
    {
        private readonly IBasketRepo _basketRepo;

        public BasketLogic(IBasketRepo basketRepo)
        {
            _basketRepo = basketRepo;
        }

        public Basket AddProducts(Guid basketId, List<Product> products)
        {
            try
            {
                var basket = _basketRepo.GetBasket(basketId);

                if (basket == null)
                {
                    return null;
                }

                basket.Updated = DateTime.Now;

                var updatedBasket = _basketRepo.AddProducts(basketId, products);

                return updatedBasket;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Basket CreateBasket(Basket basket)
        {
            try
            {
                return _basketRepo.CreateBasket(basket);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Basket GetBasket(Guid id)
        {
            try
            {
                return _basketRepo.GetBasket(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Basket> GetBaskets()
        {
            try
            {
                return _basketRepo.GetBaskets();
            }
            catch (Exception ex)
            {
                return new List<Basket>();
            }
        }

        public Basket RemoveProducts(Guid basketId, List<Product> products)
        {
            try
            {
                var basket = _basketRepo.GetBasket(basketId);

                if (basket == null)
                {
                    return null;
                }

                basket.Updated = DateTime.Now;

                var updatedBasket = _basketRepo.RemoveProducts(basketId, products);

                return updatedBasket;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Basket IncreaseProductQuantity(Guid basketId, Guid productId)
        {
            try
            {
                var basket = _basketRepo.GetBasket(basketId);

                if (basket == null)
                {
                    return null;
                }

                var index = basket.Products.FindIndex(x => x.ProductId == productId);
                basket.Products[index].Quantity++;

                basket.Updated = DateTime.Now;

                var updatedBasket = _basketRepo.UpdateBasket(basket);

                return updatedBasket;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Basket DecreaseProductQuantity(Guid basketId, Guid productId)
        {
            try
            {
                var basket = _basketRepo.GetBasket(basketId);

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

                var updatedBasket = _basketRepo.UpdateBasket(basket);

                return updatedBasket;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
