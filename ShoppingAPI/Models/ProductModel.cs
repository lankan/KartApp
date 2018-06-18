using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ShoppingBasket;
using ShoppingBasket.Models;

namespace ShoppingAPI.Models
{
    public class ProductModel
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

  }
}