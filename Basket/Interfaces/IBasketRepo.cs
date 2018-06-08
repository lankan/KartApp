using System;
using System.Collections.Generic;
using System.Text;
using ShoppingBasket.Models;

namespace ShoppingBasket
{
    public interface IBasketRepo
    {
        Basket GetBasket (Guid id);
        List<Basket> GetBaskets();
        Basket CreateBasket(Basket basket);
        Basket AddProducts(Guid basketId, List<Product> products);
        Basket RemoveProducts(Guid basketId, List<Product> products);
        Basket UpdateBasket(Basket basket);
    }
}
