using System;
using ShoppingBasket.Models;
using System.Collections.Generic;

namespace ShoppingBasket
{
    public interface IBasketLogic
    {
        Basket GetBasket(Guid id);
        List<Basket> GetBaskets();
        Basket CreateBasket(Basket basket);
        Basket AddProducts(Guid basketId, List<Product> products);
        Basket RemoveProducts(Guid basketId, List<Product> products);
        Basket IncreaseProductQuantity(Guid basketId, Guid productId);
        Basket DecreaseProductQuantity(Guid basketId, Guid productId);
    }
}
