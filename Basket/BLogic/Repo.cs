using System;
using ShoppingBasket.Models;
using System.Collections.Generic;

namespace ShoppingBasket.Logic
{
    public class Repo : IKartRepo
    {
        private static List<Kart> _baskets = new List<Kart>();
        public Kart GetBasket(Guid id)
        {
            try
            {
                return _baskets.Find(x => x.BasketId == id);
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
                return _baskets;
            }
            catch (Exception ex)
            {
                return new List<Kart>();
            }
        }

        public Kart CreateBasket(Kart basket)
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

        public Kart AddProducts(Guid basketId, Product products)
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

        public Kart RemoveProducts(Guid basketId, List<Product> products)
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

        public Kart UpdateBasket(Kart basket)
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
