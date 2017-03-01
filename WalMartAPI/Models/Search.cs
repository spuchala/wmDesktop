using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalMartAPI.Models
{
    public class Search
    {
        public string query { get; set; }
        public List<Item> items { get; set; }
    }
}