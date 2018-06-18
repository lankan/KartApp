using System;
using ShoppingBasket.Models;

namespace ShoppingBasket
{
    public interface IProduct
    {
        string Description { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        Guid ProductId { get; set; }
        int Quantity { get; set; }
        Product CreateProduct();
    }
}