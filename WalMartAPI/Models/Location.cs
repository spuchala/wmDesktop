using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalMartAPI.Models
{
    public class Location
    {
        public int no { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string streetAddress { get; set; }
        public string phoneNumber { get; set; }
        public bool sundayOpen { get; set; }
    }
}