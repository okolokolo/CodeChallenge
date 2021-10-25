using System;
using System.Collections.Generic;

namespace CodeChallenge.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public decimal Tip { get; set; }
        public decimal Cost { get; set; }
        public decimal Total => Cost + Tip;
        public DateTime DatePurchased { get; set; }
        public List<string> MenuItems { get; set; }
    }
}
