using BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace WebApi.Models.Products
{
    public class ProductModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal? Price { get; set; }
        public CategoryTypes? Category { get; set; }

        [Required(ErrorMessage = "StockQuantity is required")]
        public int? StockQuantity { get; set; }
    }
}