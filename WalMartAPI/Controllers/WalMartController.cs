using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using WalMartAPI.Helpers;
using WalMartAPI.Models;
using WalMartAPI.Services;

namespace WalMartAPI.Controllers
{
    public class WalMartController : ApiController
    {
        #region members
        private WalMartHelper _walmartHelper;
        #endregion

        #region constructor
        public WalMartController()
        {
            _walmartHelper = new WalMartHelper();
        }
        #endregion

        #region endpoints

        /// <summary>
        /// handle alexa request
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Post()
        {
            try
            {
                var speechlet = new AlexaService();
                return speechlet.GetResponse(Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get nearby stores by zip code
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/walmart/GetStoreNearBy/zipCode/{zipCode}")]
        public IHttpActionResult GetStoreNearByZipCode(string zipCode)
        {
            var result = _walmartHelper.GetLocation(zipCode, Constants.LocatorType.Zip);
            return Ok(result);
        }

        /// <summary>
        /// get nearby stores by city name
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/walmart/GetStoreNearBy/city/{city}")]
        public IHttpActionResult GetStoreNearByCity(string city)
        {
            var result = _walmartHelper.GetLocation(city.Replace(" ", "+"), Constants.LocatorType.City);
            return Ok(result);
        }

        /// <summary>
        /// search for a product 
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/walmart/Search/{query}")]
        public IHttpActionResult Search(string query)
        {
            var result = _walmartHelper.GetSearchData(query.Replace(" ", "+"));
            return Ok(result);
        }

        /// <summary>
        /// get reviews for a product 
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/walmart/Reviews/{query}")]
        public IHttpActionResult GetReviews(string query)
        {
            var data = _walmartHelper.GetSearchData(query.Replace(" ", "+"));
            if (data != null && data.items.Any())
            {
                var reviews = _walmartHelper.GetReviews(data.items[0].itemId);
                return Ok(reviews);
            }
            return null;
        }

        /// <summary>
        /// get trending items from walmart
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/walmart/Trends")]
        public IHttpActionResult Trends()
        {
            var data = _walmartHelper.GetTrends();
            if (data != null && data.items.Any())
            {
                return Ok(data.items);
            }
            return null;
        }

        /// <summary>
        /// get walmart cheer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/walmart/WalMartCheer")]
        public IHttpActionResult WalMartCheer()
        {
            var data = _walmartHelper.Cheer(Constants.CheerType.WalMart);
            if (data != null)
            {
                return Ok(data);
            }
            return null;
        }

        /// <summary>
        /// get sams cheer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/walmart/SamsCheer")]
        public IHttpActionResult SamsCheer()
        {
            var data = _walmartHelper.Cheer(Constants.CheerType.WalMart);
            if (data != null)
            {
                return Ok(data);
            }
            return null;
        }

        #endregion
    }
}
