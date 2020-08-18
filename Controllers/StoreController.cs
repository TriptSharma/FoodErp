using FoodErp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FoodErp.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public async Task<ActionResult> Index()
        {

            IEnumerable<StoreViewModel> stores = null;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/Stores");

            var consumeApi = httpClient.GetAsync("Stores");
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<IList<StoreViewModel>>();
                displayData.Wait();
                stores = displayData.Result;
            }
            return View(stores);
        }

        public ActionResult Create(StoreViewModel store)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/stores");

            //HTTP POST
            var postTask = httpClient.PostAsJsonAsync<StoreViewModel>("stores", store);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            }

            return View(store);
        }
    }
}