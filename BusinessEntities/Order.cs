using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        public Guid ProductId { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public int? Quantity { get; set; }      
        public decimal? TotalPrice { get; set; }
         
    }
}
