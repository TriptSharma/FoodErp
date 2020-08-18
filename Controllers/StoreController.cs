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
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(store);
        }

        public ActionResult Update(int id)
        {
            StoreViewModel storeInDb = new StoreViewModel();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/");

            var getTask = httpClient.GetAsync("stores?id=" + id.ToString());
            getTask.Wait();

            var readData = getTask.Result;
            if (readData.IsSuccessStatusCode)
            {
                var dbData = readData.Content.ReadAsAsync<StoreViewModel>();
                dbData.Wait();
                storeInDb = dbData.Result;
            }
            return View(storeInDb);
        }

        [HttpPost]
        public ActionResult Update(StoreViewModel store)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/");

            var putTask = httpClient.PutAsJsonAsync<StoreViewModel>("stores", store);
                putTask.Wait();

            var putResult = putTask.Result;
            if (putResult.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(store);
        }
            //HTTP POST

        public ActionResult Delete(int id) {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/");

            var deleteTask = httpClient.DeleteAsync("stores/" + id.ToString());
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
        return RedirectToAction("Index");
    }
    }
}