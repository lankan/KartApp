﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}