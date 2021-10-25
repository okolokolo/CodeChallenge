using System;
using System.Collections.Generic;

namespace DataContext.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Tip { get; set; }
        public decimal Cost { get; set; }
        public DateTime? DatePurchased { get; set; }
        public List<OrderMenuItem> MenuItems { get; set; } = new List<OrderMenuItem>();
    }
}
