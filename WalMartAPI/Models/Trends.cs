using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalMartAPI.Models
{
    public class Trends
    {
        public Int64 time { get; set; }
        public List<Item> items { get; set; }
    }
}