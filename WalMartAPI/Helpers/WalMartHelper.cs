using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using WalMartAPI.Models;

namespace WalMartAPI.Helpers
{
    public class WalMartHelper
    {
        #region properties
        private string _apiKey = "4m2f84j7g6yda9jaf2jagwxz";
        private string _walmartApi = "http://api.walmartlabs.com/v1/";
        private string _search = "search?query={0}&format=json&apiKey={1}";
        private string _reviews = "reviews/{0}?format=json&apiKey={1}";
        private string _trending = "trends?format=json&apiKey={0}";
        private string _storeLocatorByZip = "stores?format=json&zip={0}&apiKey={1}";
        private string _storeLocatorByCity = "stores?format=json&city={0}&apiKey={1}";
        HttpClient _httpClient;
        #endregion

        #region Helpers
        public string Cheer(Constants.CheerType cheerType)
        {
            if (cheerType == Constants.CheerType.WalMart)
                return "Give me a W!, Give me an A!, Give me an L!, Give me a Squiggly!, Give me an M!, Give me an A!, Give me an R!, Give me a T!, What's that spell? Whose Walmart is it? Who's number one?";
            else if (cheerType == Constants.CheerType.Sams)
                return "Give me an S!, Give me an A!, Give me an M!, Give me a Huh!, Give me an S!, What's that spell? Who's number one?";
            return null;
        }

        public Trends GetTrends()
        {
            try
            {
                InitializeClient();
                Trends trends = null;
                HttpResponseMessage response = _httpClient.GetAsync(string.Format(_trending, _apiKey)).Result;
                if (response.IsSuccessStatusCode)
                {
                    trends = response.Content.ReadAsAsync<Trends>().Result;
                }
                return trends;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Review> GetReviews(int id)
        {
            try
            {
                InitializeClient();
                Item item = null;
                HttpResponseMessage response = _httpClient.GetAsync(string.Format(_reviews, id, _apiKey)).Result;
                if (response.IsSuccessStatusCode)
                {
                    item = response.Content.ReadAsAsync<Item>().Result;
                }
                if (item != null && item.reviews.Any())
                    return item.reviews;
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Search GetSearchData(string query)
        {
            try
            {
                InitializeClient();
                Search searchData = null;
                HttpResponseMessage response = _httpClient.GetAsync(string.Format(_search, query, _apiKey)).Result;
                if (response.IsSuccessStatusCode)
                {
                    searchData = response.Content.ReadAsAsync<Search>().Result;
                }
                return searchData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Location> GetLocation(string input, Constants.LocatorType type)
        {
            try
            {
                InitializeClient();
                List<Location> locations = null;
                var url = string.Empty;
                if (type == Constants.LocatorType.City) url = _storeLocatorByCity;
                else if (type == Constants.LocatorType.Zip) url = _storeLocatorByZip;
                HttpResponseMessage response = _httpClient.GetAsync(string.Format(url, input, _apiKey)).Result;
                if (response.IsSuccessStatusCode)
                {
                    locations = response.Content.ReadAsAsync<List<Location>>().Result;
                }
                return locations;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InitializeClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_walmartApi);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion
    }
}