using System;
using System.Collections.Generic;
using System.Text;
using ShoppingBasket.Models;

namespace ShoppingBasket
{
    public interface IKartRepo
    {
        Kart GetBasket (Guid id);
        List<Kart> GetBaskets();
        Kart CreateBasket();
        Kart AddProducts(Guid basketId, Product products);
        Kart RemoveProducts(Guid basketId, List<Product> products);
        Kart UpdateBasket(Kart basket);
    }
}
