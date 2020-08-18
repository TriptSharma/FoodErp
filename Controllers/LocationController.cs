using FoodErp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace FoodErp.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {

            IEnumerable<LocationViewModel> locations = null;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/locations");

            var consumeApi = httpClient.GetAsync("locations");
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<IList<LocationViewModel>>();
                displayData.Wait();
                locations = displayData.Result;
            }
            return View(locations);
        }

        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(LocationViewModel location)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/locations");

            //HTTP POST
            var postTask = httpClient.PostAsJsonAsync<LocationViewModel>("locations", location);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(location);
        }

        public ActionResult Update(int id)
        {
            LocationViewModel locationInDb = new LocationViewModel();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/");

            var getTask = httpClient.GetAsync("locations?id=" + id.ToString());
            getTask.Wait();

            var readData = getTask.Result;
            if (readData.IsSuccessStatusCode)
            {
                var dbData = readData.Content.ReadAsAsync<LocationViewModel>();
                dbData.Wait();
                locationInDb = dbData.Result;
            }
            return View(locationInDb);
        }

        [HttpPost]
        public ActionResult Update(LocationViewModel location)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/");

            var putTask = httpClient.PutAsJsonAsync<LocationViewModel>("locations", location);
            putTask.Wait();

            var putResult = putTask.Result;
            if (putResult.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(location);
        }
        //HTTP POST

        public ActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/");

            var deleteTask = httpClient.DeleteAsync("locations/" + id.ToString());
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