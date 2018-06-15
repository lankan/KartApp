using System;
using ShoppingBasket.Models;
using System.Collections.Generic;

namespace ShoppingBasket
{
    public interface IKart
    {
        Kart GetBasket(Guid id);
        List<Kart> GetBaskets();
        Kart CreateBasket(Kart basket);
        Kart AddProducts(Guid basketId, Product products);
        Kart RemoveProducts(Guid basketId, List<Product> products);
        Kart IncreaseProductQuantity(Guid basketId, Guid productId);
        Kart DecreaseProductQuantity(Guid basketId, Guid productId);
    }
}
