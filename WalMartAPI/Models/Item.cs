using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalMartAPI.Models
{
    public class Item
    {
        public int itemId { get; set; }
        public string name { get; set; }
        public double msrp { get; set; }
        public double salePrice { get; set; }
        public string shortDescription { get; set; }
        public List<Review> reviews { get; set; }
    }
}