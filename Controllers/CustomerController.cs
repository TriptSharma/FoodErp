using FoodErp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace FoodErp.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<CustomerViewModel> customers = null;

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/customers");

            var consumeApi = httpClient.GetAsync("customers");
            consumeApi.Wait();

            var resultData = consumeApi.Result;
            if (resultData.IsSuccessStatusCode)
            {
                var displayData = resultData.Content.ReadAsAsync<IList<CustomerViewModel>>();
                displayData.Wait();

                customers = displayData.Result;
            }
            return View(customers);
        }

        public ActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44345/api/");

            var deleteTask = httpClient.DeleteAsync("customers/" + id.ToString());
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