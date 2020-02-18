using FODfinder.Models.Food;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FODfinder.Controllers
{
    public class FoodController : Controller
    {
        private String _API_key = WebConfigurationManager.AppSettings.Get("USDA_KEY");
        private const String USDA_FOOD_SEARCH = "https://api.nal.usda.gov/fdc/v1/search";
        async private Task<String> GetFoodResults(String query, String pageNumber = "1")
        {
            UriBuilder uriBuilder = new UriBuilder(USDA_FOOD_SEARCH);
            var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParams["api_key"] = _API_key;
            queryParams["generalSearchInput"] = String.Join("%20",query.Split(' '));
            queryParams["includeDataTypeList"] = "Branded";
            queryParams["pageNumber"] = pageNumber;
            uriBuilder.Query = queryParams.ToString();

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(uriBuilder.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
        // GET: Food
        async public Task<ActionResult> Index(String query)
        {
            if (query == null)
            {
                return View();
            }
            var foodSearchResults = await GetFoodResults(query);
            return View(new FoodSearchResult(foodSearchResults));
        }

        async public Task<ContentResult> Get(String query, String page)
        {
            var foodSearchResults = await GetFoodResults(query, page);
            FoodSearchResult results = new FoodSearchResult(foodSearchResults);
            return Content(JObject.FromObject(results).ToString(), "application/json");
        }
    }
}