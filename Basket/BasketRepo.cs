using System;
using ShoppingBasket.Models;
using System.Collections.Generic;

namespace ShoppingBasket.MockRepository
{
    public class BasketRepo : IBasketRepo
    {
        private static List<Basket> _baskets = new List<Basket>();

        public Basket AddProducts(Guid basketId, List<Product> products)
        {
            try
            {
                var index = _baskets.FindIndex(x => x.BasketId == basketId);

                if (index < 0)
                {
                    return null;
                }

                var basket = _baskets[index];

                basket.Products.AddRange(products);
                _baskets[index] = basket;

                return basket;
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
                _baskets.Add(basket);

                return basket;
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
                return _baskets.Find(x => x.BasketId == id);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<Basket> GetBaskets()
        {
            try
            {
                return _baskets;
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
                var index = _baskets.FindIndex(x => x.BasketId == basketId);

                if (index < 0)
                {
                    return null;
                }

                var basket = _baskets.Find(x => x.BasketId == basketId);

                for (int i = basket.Products.Count - 1; i >= 0; i--)
                {
                    var productId = basket.Products[i].ProductId;
                    var product = products.Find(x => x.ProductId == productId);

                    if (product != null)
                    {
                        basket.Products.RemoveAt(i);
                    }
                }

                _baskets[index] = basket;

                return basket;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Basket UpdateBasket(Basket basket)
        {
            try
            {
                var index = _baskets.FindIndex(x => x.BasketId == basket.BasketId);

                if (index < 0)
                {
                    return null;
                }

                _baskets[index].Products = basket.Products;

                return _baskets[index];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
