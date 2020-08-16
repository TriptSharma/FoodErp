using FoodErp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodErp.Controllers.Api
{
    public class StoresController : ApiController
    {
        private FoodiesContext _context;
        
        public StoresController()
        {
            _context = new FoodiesContext();
        }

        //GET api/stores/
        [HttpGet]
        public IEnumerable<Store> GetStores()
        {
            return _context.Store.ToList();
        }

        //GET api/stores/
        [HttpGet]
        public Store GetStore(int id)
        {
            var store = _context.Store.SingleOrDefault(m => m.StoreId == id);
            if (store == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return store;
        }

        //POST api/Stores
        [HttpPost]
        public Store CreateStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Store.Add(store);
            _context.SaveChanges();
            return store;
        }

        //PUT api/Stores/id
        public Store UpdateStore(int id, Store store)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var storeInDb = _context.Store.SingleOrDefault(m => m.StoreId == id);
            if (storeInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
            {
                storeInDb.StoreName = store.StoreName;
                storeInDb.Location = store.Location;
                storeInDb.LocationId = store.LocationId;
                storeInDb.Revenue = store.Revenue;

                _context.SaveChanges();
            }
            return store;
        }

        //DELETE api/Stores/id
        public Store DeleteStore(Store store)
        {
            var storeInDb = _context.Store.SingleOrDefault(m => m.StoreId == store.StoreId);
            if (storeInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Store.Remove(storeInDb);
            _context.SaveChanges();
            return store;
        }

    }
}
