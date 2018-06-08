using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Models
{
    public class Basket
    {

        public Guid BasketId { get; set; } = Guid.NewGuid();
        public List<Product> Products { get; set; } = new List<Product>();
        public int Quantity
        {
            get => Products.Count;
        }
        public decimal Total
        {
            get
            {
                decimal total = 0;

                Products.ForEach(x =>
                {
                    total = total + (x.Price * x.Quantity);
                });

                return total;
            }
        }
        public DateTime Added { get; set; }
        public DateTime Updated { get; set; }

    }
}
