using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalMartAPI.Models
{
    public class Constants
    {
        public const string RequestReviews = "RequestReviews";
        public const string ProductSlot = "Product";
        public const string Error = "Error";

        public enum LocatorType
        {
            City,
            Zip
        }

        public enum CheerType
        {
            WalMart,
            Sams
        }
    }
}